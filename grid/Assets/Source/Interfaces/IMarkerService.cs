using Source.Grid;

namespace Source.Interfaces
{
    public interface IMarkerService
    {
        void AddMarker(Cell cell);
        void RemoveMarker(Cell cell);
    }
}