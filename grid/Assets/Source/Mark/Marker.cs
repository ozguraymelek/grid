using System;
using UnityEngine;
using Zenject;

namespace Source.Mark
{
    public class Marker : MonoBehaviour, IPoolable<Vector2, Marker.Pool>, IDisposable
    {
        public class Pool : MonoMemoryPool<Vector2, Marker>
        {
            
        }
        
        private Pool _pool;
        
        public void OnDespawned()
        {
            gameObject.SetActive(false);
        }

        public void OnSpawned(Vector2 pos, Pool pool)
        {
            _pool = pool;
            // gameObject.SetActive(true);
            transform.localPosition = pos;
            Debug.Log(transform.localPosition);
        }

        public void Dispose()
        {
            _pool.Despawn(this);
        }
    }
}
