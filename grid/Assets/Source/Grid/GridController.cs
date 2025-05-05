using System;
using Source.Core.Utils;
using Source.Data;
using Source.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Grid
{
    public class GridController : MonoBehaviour, IGridBuilder
    {
        private Cell.Cell.Factory _cellFactory;
        private GridConfig _config;
        private Camera _camera;
        private Cell.Cell[,] _cells;

        [Inject(Id = "PaddingFactor")] private readonly float _padding;
        private float _lastCameraOrthoSize;
        
        [Inject]
        public void Construct(Cell.Cell.Factory cellFactory, GridConfig config)
        {
            _cellFactory = cellFactory;
            _config = config;
            _camera = Camera.main;

            SLog.InjectionStatus(this,
                (nameof(_cellFactory), cellFactory),
                (nameof(_config), _config),
                (nameof(_camera), _camera)
            );
        }
        
        public void Generate()
        {
            Clear();
            var size = _config.Size;
            _cells = new Cell.Cell[size, size];

            var vertical = _camera.orthographicSize * 2;
            var horizontal = vertical * _camera.aspect;
            var square = Mathf.Min(vertical, horizontal);
            var cellSize = square / size;
            var origin = Vector2.one * -square / 2;

            for (var x = 0; x < size; x++)
            for (var y = 0; y < size; y++)
            {
                var worldPos = origin + new Vector2(x + 0.5f, y + 0.5f) * cellSize;
                var cell = _cellFactory.Create();
                cell.Initialize(new Vector2Int(x, y), worldPos, cellSize * _padding);
                cell.transform.SetParent(transform, worldPositionStays: false);
                _cells[x, y] = cell;
            }
        }

        public void Regenerate() => Generate();

        public void Clear()
        {
            if (_cells == null) return;
            foreach (var cell in _cells)
                if (cell) Destroy(cell.gameObject);
            _cells = null;
        }

        private void Update()
        {
            if (!Mathf.Approximately(_camera.orthographicSize, _lastCameraOrthoSize))
            {
                _lastCameraOrthoSize = _camera.orthographicSize;
                Regenerate();
            }
        }
    }
}