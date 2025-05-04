using System;
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

        [Inject]
        public void Construct()
        {
            
        }
        
        private void Awake()
        {
            ObjectPool<Marker>.Setup(markerPrefab, 10, "Marker", transform, false);
        }
    }
}
