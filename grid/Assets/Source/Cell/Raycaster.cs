using System;
using Source.Core.Utils;
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
        private MarkerSpawner _markerSpawner;
        
        public Marker ActiveMarker;
        
        [Inject]
        public void Construct(MarkerSpawner markerSpawner)
        {
            _markerSpawner = markerSpawner;

            SLog.InjectionStatus(this,
                (nameof(_markerSpawner), _markerSpawner)
            );
        }
        
        private void OnMouseDown()
        {
            if (GetComponent<Cell>().IsMarked == false)
            {
                ActiveMarker = ObjectPool<Marker>.Dequeue("Marker");
                ActiveMarker.Mark(GetComponent<Cell>(), ActiveMarker);
                Settings(Stage.Dequeue);
                return;
            }
            
            ActiveMarker.Unmark(GetComponent<Cell>(), ActiveMarker);
            Settings(Stage.Enqueue);
        }

        private void Settings(Stage stage = Stage.Dequeue)
        {
            ActiveMarker.transform.SetParent(stage == Stage.Dequeue ? transform : _markerSpawner.transform
                , false);
            ActiveMarker.gameObject.SetActive(stage == Stage.Dequeue);
            ActiveMarker.transform.localPosition = Vector3.zero;
        }
    }
}