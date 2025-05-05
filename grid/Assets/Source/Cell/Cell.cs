using Source.Core.Utils;
using Source.Flow.Search;
using Source.Infrastructure.Pool;
using Source.Interfaces;
using Source.Mark;
using UnityEngine;
using Zenject;
using Cell_ = Source.Cell.Cell;

namespace Source.Cell
{
    [RequireComponent(typeof(Collider2D))]
    public class Cell : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Cell> { }
        
        public Vector2Int[] Neighbors => new[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
        
        private Vector2Int _gridPos;
        private IMarkerService _markerService;
        private IDisjointSet _disjoint;
        private bool _isMarked;
        private float _cellSize;

        [Inject]
        public void Construct(IMarkerService markerService, IDisjointSet disjointSet)
        {
            _markerService = markerService;
            _disjoint = disjointSet;

            SLog.InjectionStatus(this,
                (nameof(_markerService), _markerService),
                (nameof(_disjoint), _disjoint)
            );
        }

        public void Initialize(Vector2Int gridPos, Vector2 worldPos, float scale)
        {
            _gridPos = gridPos;
            transform.localPosition = worldPos;
            transform.localScale = Vector3.one * scale;
            name = $"Cell {_gridPos.x}-{_gridPos.y}";
        }

        private void OnMouseDown()
        {
            if (!_isMarked)
                _markerService.AddMarker(this);
            else
                _markerService.RemoveMarker(this);
        }
        
        public Vector2Int GridPosition => _gridPos;
        
        public bool IsMarked => _isMarked;
        
        public void SetMarked(bool marked) => _isMarked = marked;
    }
}