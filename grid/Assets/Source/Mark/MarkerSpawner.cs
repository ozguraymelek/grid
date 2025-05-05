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
        [SerializeField] private int poolInitialSize;

        private void Awake()
        {
            ObjectPool<Marker>.Setup(markerPrefab, poolInitialSize, "Marker", transform, false);
        }

        public Marker Spawn() => ObjectPool<Marker>.Dequeue("Marker", transform);
        public void Despawn(Marker item) => ObjectPool<Marker>.Enqueue(item, "Marker");
    }
}