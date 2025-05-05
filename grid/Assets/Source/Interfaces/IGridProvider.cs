using Source.Grid;

namespace Source.Interfaces
{
    public interface IGridProvider
    {
        public int MatchCount { get; set; }
        
        Cell GetAt(int x, int y);
    }
}