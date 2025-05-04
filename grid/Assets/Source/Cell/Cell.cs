using UnityEngine;
using Zenject;

namespace Source.Cell
{
    public class Cell : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Cell>{}
        
        public bool IsMarked  = false;
        
        public void Mark()
        {
            if (IsMarked) return;
            // markInstance = Instantiate(crossPrefab, transform.position, Quaternion.identity, transform);
            IsMarked = true;
        }

        public void Unmark()
        {
            if (!IsMarked) return;
            IsMarked = false;
        }
    }
}