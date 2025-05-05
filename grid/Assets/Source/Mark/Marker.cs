using Source.Cell;
using Source.Infrastructure.Pool;
using UnityEngine;
using Cell_ = Source.Cell.Cell;

namespace Source.Mark
{
    public class Marker : MonoBehaviour, IInteractable
    {
        public void Mark(Cell_ cell, Marker marker)
        {
            if (cell.IsMarked)
            {
                return;
            }
            
            cell.IsMarked = true;
        }

        public void Unmark(Cell_ cell, Marker marker)
        {
            if (!cell.IsMarked) return;
            ObjectPool<Marker>.Enqueue(marker, "Marker");
            cell.IsMarked = false;
        }
    }
}