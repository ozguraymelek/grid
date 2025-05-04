using System;
using Source.Cell;
using Source.Core.Utils;
using Source.Infrastructure.Pool;
using UnityEngine;
using Zenject;

namespace Source.Mark
{
    public class Marker : MonoBehaviour, IInteractable
    {
        public void Mark(Cell.Cell cell, Marker marker)
        {
            if (cell.IsMarked)
            {
                return;
            }
            
            cell.IsMarked = true;
        }

        public void Unmark(Cell.Cell cell, Marker marker)
        {
            if (!cell.IsMarked) return;
            ObjectPool<Marker>.Enqueue(marker, "Marker");
            cell.IsMarked = false;
        }
    }
}