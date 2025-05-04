using System.Collections.Generic;
using Source.Mark;
using UnityEngine;
using Zenject;

namespace Source.Infrastructure.Pool
{
    public class ObjectPool<T> where T : Component
    {
        private static Component prefab;
        public static Dictionary<string, Queue<Component>> _pool = new Dictionary<string, Queue<Component>>();

        public static void Setup(T componentPrefab, int initialSize, string key, 
            Transform parent = null, bool worldPositionStays = true)
        {
            if (_pool.ContainsKey(key)) _pool.Remove(key);
            prefab = componentPrefab;
            _pool.Add(key, new Queue<Component>());

            for (var i = 0; i < initialSize; i++)
            {
                var instance = Object.Instantiate(componentPrefab, parent, worldPositionStays);
                instance.gameObject.SetActive(false);
                _pool[key].Enqueue(instance);
            }
        }
        
        public static void Enqueue(T component, string name)
        {
            if (!component.gameObject.activeSelf) return;

            component.transform.position = Vector2.zero;
            _pool[name].Enqueue(component);
            component.gameObject.SetActive(false);
        }

        public static T Dequeue(string key, Transform parent = null)
        {
            if (!_pool.TryGetValue(key, out var queue))
            {
                Debug.LogError($"No pool found for key: {key}");
                return null;
            }

            if (queue.Count == 0)
            {
                if (prefab != null)
                {
                    var instance = Object.Instantiate(prefab, parent);
                    instance.gameObject.SetActive(false);
                    return (T)instance;
                }

                Debug.LogWarning($"Pool is empty for key: {key}, and no prefab provided for instantiation.");
                return null;
            }

            return (T)queue.Dequeue();
        }
    }
}