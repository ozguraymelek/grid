using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Source.Core.Utils;
using Source.Data;
using UnityEngine;
using Zenject;

namespace Source.Grid
{
    public class Grid : MonoBehaviour, IBuilder
    {
        private Cell.Cell.Factory _cellFactory;
        private Cell.Cell[,] _cells;
        
        [SerializeField] private GridConfig gridConfig;
        [SerializeField] private float paddingFactor;    
        private float _lastCameraSize;
        private Camera _cam;
        
        [Inject]
        public void Construct(Cell.Cell.Factory cellFactory)
        {
            _cellFactory = cellFactory;
            
            SLog.InjectionStatus(this,
                (nameof(_cellFactory), _cellFactory)
            );
        }

        private void Awake()
        {
            _cam = Camera.main;
        }

        private void Update()
        {
            if (_cam.orthographicSize <= 0f) _cam.orthographicSize = _lastCameraSize;
            if (!Mathf.Approximately(_cam.orthographicSize, _lastCameraSize))
            {
                _lastCameraSize = _cam.orthographicSize;
                Regenerate();
            }
        }

        public void Generate()
        {
            _cells = new Cell.Cell[gridConfig.Size, gridConfig.Size];
            
            Clear();
            
            var verticalSize = _cam.orthographicSize * 2f;
            var horizontalSize = verticalSize * _cam.aspect;
            var squareSize = Mathf.Min(verticalSize, horizontalSize);
            var cellSize = squareSize / gridConfig.Size;
            var origin = new Vector2(-squareSize / 2f, -squareSize / 2f);
            
            for (var l = 0; l < gridConfig.Size; l++)
            {
                for (var c = 0; c < gridConfig.Size; c++)
                {
                    var pos = origin + new Vector2(
                        l * cellSize + cellSize / 2f,
                        c * cellSize + cellSize / 2f);

                    var spawnedCell = _cellFactory.Create();
                    spawnedCell.transform.SetParent(transform, true);
                    spawnedCell.name = $"Cell {l}-{c}";
                    spawnedCell.transform.localScale = Vector3.one * (cellSize * paddingFactor);
                    spawnedCell.transform.position = pos;
                    // spawnedCell.Initialize(new Vector2Int(x, y));
                    _cells[l, c] = spawnedCell;
                }
            }
        }

        public void Regenerate()
        {
            Clear();
            Generate();
        }

        public void Clear()
        {
            if (_cells == null) return;

            foreach (var c in _cells)
            {
                if (c == null) continue;
                Destroy(c.gameObject);
            }
            
            Array.Clear(_cells, 0, _cells.Length);
        }
    }
}