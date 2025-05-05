using Source.Mark;
using Cell_ = Source.Cell.Cell;

namespace Source.Cell
{
    public interface IInteractable
    {
        void Mark(Cell cell, Marker marker);
        void Unmark(Cell cell, Marker marker);
    }
}