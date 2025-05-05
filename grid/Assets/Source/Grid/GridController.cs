using System;
using Source.Core.Utils;
using Source.Data;
using Source.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Grid
{
    public class GridController : MonoBehaviour, IGridBuilder, IGridProvider
    {
        private Cell.Cell.Factory _cellFactory;
        private GridConfig _config;
        private Camera _camera;
        private Cell.Cell[,] _cells;
        
        [Inject(Id = "PaddingFactor")] private readonly float _padding;
        
        public int MatchCount { get; set; }
        private int _size;
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
            _size = _config.Size;
            _cells = new Cell.Cell[_size, _size];

            var vertical = _camera.orthographicSize * 2;
            var horizontal = vertical * _camera.aspect;
            var square = Mathf.Min(vertical, horizontal);
            var cellSize = square / _size;
            var origin = Vector2.one * -square / 2;

            for (var x = 0; x < _size; x++)
            for (var y = 0; y < _size; y++)
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
            MatchCount = 0;
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

        public Cell.Cell GetAt(int x, int y)
        {
            if (x < 0 || y < 0 || x >= _size || y >= _size)
                return null;
            return _cells[x, y];
        }
    }
}