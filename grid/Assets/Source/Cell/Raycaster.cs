using System;
using Source.Core.Utils;
using Source.Flow.Search;
using Source.Infrastructure.Pool;
using Source.Mark;
using UnityEngine;
using Zenject;

namespace Source.Cell
{
    public enum Stage
    {
        Dequeue,
        Enqueue
    }
    public class Raycaster : MonoBehaviour
    {
        private DisjointGridManager _disjointGridManager;
        private MarkerSpawner _markerSpawner;
        
        
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
            var cell = GetComponent<Cell>();
            if (cell.IsMarked == false)
            {
                _markerSpawner.ActiveMarker = ObjectPool<Marker>.Dequeue("Marker");
                _markerSpawner.ActiveMarker.Mark(GetComponent<Cell>(), 
                    _markerSpawner.ActiveMarker);
                Settings(Stage.Dequeue);
                _disjointGridManager.Add(cell);
                return;
            }
            
            _markerSpawner.ActiveMarker.Unmark(GetComponent<Cell>(),
                _markerSpawner.ActiveMarker);
            Settings(Stage.Enqueue);
        }

        private void Settings(Stage stage = Stage.Dequeue)
        {
            _markerSpawner.ActiveMarker.transform.SetParent(stage == Stage.Dequeue ? transform : _markerSpawner.transform
                , false);
            _markerSpawner.ActiveMarker.gameObject.SetActive(stage == Stage.Dequeue);
            _markerSpawner.ActiveMarker.transform.localPosition = Vector3.zero;
        }
    }
}