using System;
using System.Collections.Generic;
using Source.Core.Utils;
using Source.Infrastructure.Pool;
using UnityEngine;
using Zenject;

namespace Source.Mark
{
    public class MarkerSpawner : MonoBehaviour
    {
        [SerializeField] private Marker markerPrefab;
        public Marker ActiveMarker;
        public HashSet<Marker> MarkedObjects = new HashSet<Marker>();

        [Inject]
        public void Construct()
        {
            
        }
        
        private void Awake()
        {
            ObjectPool<Marker>.Setup(markerPrefab, 10, "Marker", transform, false);
        }

        public void SetMarkedObjects(bool condition)
        {
            if (condition)
            {
                MarkedObjects.Add(ActiveMarker);
                Debug.Log(MarkedObjects.Count);
            }
                
            else
                MarkedObjects.Remove(ActiveMarker);
        }
    }
}
