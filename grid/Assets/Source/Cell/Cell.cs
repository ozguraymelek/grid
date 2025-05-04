using Source.Core.Utils;
using Source.Infrastructure.Pool;
using Source.Mark;
using UnityEngine;
using Zenject;

namespace Source.Cell
{
    public class Cell : MonoBehaviour
    {
        public class Factory : PlaceholderFactory<Cell>{}
        
        public bool IsMarked  = false;

        // 4 yöne komşuyu getir
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
            ObjectPool<Marker>.Enqueue(transform.GetChild(0).GetComponent<Marker>(), "Marker");
            IsMarked = false;
        }
    }
}