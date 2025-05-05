namespace Source.Interfaces
{
    public interface IMarkerService
    {
        void AddMarker(Cell.Cell cell);
        void RemoveMarker(Cell.Cell cell);
    }
}