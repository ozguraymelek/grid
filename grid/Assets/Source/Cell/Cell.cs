using Source.Core.Utils;
using Source.Flow.Search;
using Source.Infrastructure.Pool;
using Source.Mark;
using UnityEngine;
using Zenject;
using Cell_ = Source.Cell.Cell;

namespace Source.Cell
{
    public enum InteractionStage
    {
        Dequeue,
        Enqueue
    }
    public class Cell : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Cell>{}
        
        private DisjointGridManager _disjointGridManager;
        private MarkerSpawner _markerSpawner;
        
        public bool IsMarked  = false;
        
        [Inject]
        public void Construct(MarkerSpawner markerSpawner, DisjointGridManager disjointGridManager)
        {
            _markerSpawner = markerSpawner;
            _disjointGridManager = disjointGridManager;

            SLog.InjectionStatus(this,
                (nameof(_markerSpawner), _markerSpawner),
                (nameof(_disjointGridManager), _disjointGridManager)
            );
        }
        
        private void OnMouseDown()
        {
            if (IsMarked == false)
            {
                _markerSpawner.ActiveMarker = ObjectPool<Marker>.Dequeue("Marker");
                _markerSpawner.SetMarkedObjects(true);
                _markerSpawner.ActiveMarker.Mark(this, 
                    _markerSpawner.ActiveMarker);
                Settings(InteractionStage.Dequeue);
                _disjointGridManager.Add(this);
                return;
            }
            
            _markerSpawner.ActiveMarker.Unmark(this,
                _markerSpawner.ActiveMarker);
            _markerSpawner.SetMarkedObjects(false);
            Settings(InteractionStage.Enqueue);
        }

        private void Settings(InteractionStage interactionStage = InteractionStage.Dequeue)
        {
            _markerSpawner.ActiveMarker.transform.SetParent(interactionStage == InteractionStage.Dequeue ? transform : _markerSpawner.transform
                , false);
            _markerSpawner.ActiveMarker.gameObject.SetActive(interactionStage == InteractionStage.Dequeue);
            _markerSpawner.ActiveMarker.transform.localPosition = Vector3.zero;
        }
        
        public Cell GetNeighbor(Vector2Int dir)
        {
            var neighborPos = transform.position + new Vector3(dir.x, dir.y, 0);
            var hit = Physics2D.OverlapPoint(neighborPos);
            if (hit != null)
            {
                return hit.GetComponent<Cell>();
            }
            return null;
        }
        
        public void RemoveMarker()
        {
            if (!IsMarked) return;
            
            ObjectPool<Marker>.Enqueue(transform.GetChild(0).GetComponent<Marker>(), "Marker");
            transform.GetChild(0).GetComponent<Marker>().transform.SetParent(_markerSpawner.transform, false);
            IsMarked = false;
        }
    }
}