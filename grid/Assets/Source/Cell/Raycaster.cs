using System;
using Source.Core.Utils;
using Source.Mark;
using UnityEngine;
using Zenject;

namespace Source.Cell
{
    public class Raycaster : MonoBehaviour, IInteractable
    {
        private Marker.Pool _pool;
        
        [Inject]
        public void Construct(Marker.Pool pool)
        {
            _pool = pool;

            SLog.InjectionStatus(this,
                (nameof(_pool), _pool));
        }
        
        private void OnMouseDown()
        {
            Debug.Log("OnMouseDown");
            _pool.Spawn(Input.mousePosition);
        }

        public void Mark()
        {
            
        }

        public void Unmark()
        {
            
        }
    }
}