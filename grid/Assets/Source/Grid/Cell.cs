using System;
using Source.Core.Utils;
using Source.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Grid
{
    [RequireComponent(typeof(Collider2D))]
    public class Cell : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Cell> { }
        
        public Vector2Int GridPosition => _gridPos;
        
        public bool IsMarked => _isMarked;
        
        private Vector2Int _gridPos;
        private IMarkerService _markerService;
        private IClustering _clustering;
        private bool _isMarked;
        private float _cellSize;
        
        public Vector2Int[] Neighbors => new[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };
        
        private SpriteRenderer _spriteRenderer;

        [Inject]
        public void Construct(IMarkerService markerService, IClustering clustering)
        {
            _markerService = markerService;
            _clustering = clustering;
    
            SLog.InjectionStatus(this,
                (nameof(_markerService), _markerService),
                (nameof(_clustering), _clustering)
            );
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
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

        private void OnMouseEnter()
        {
            _spriteRenderer.color = new Color32(185, 185, 155, 255);
        }

        private void OnMouseExit()
        {
            
            _spriteRenderer.color = Color.white;
        }

        public void SetMarked(bool marked) => _isMarked = marked;
    }
}