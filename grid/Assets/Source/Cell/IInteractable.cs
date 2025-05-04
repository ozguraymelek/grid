using Source.Mark;
using UnityEngine;

namespace Source.Cell
{
    public interface IInteractable
    {
        void Mark(Cell cell, Marker marker);
        void Unmark(Cell cell, Marker marker);
    }
}