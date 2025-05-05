using UnityEngine;

namespace Source.Interfaces
{
    public interface IGridProvider
    {
        public int MatchCount { get; set; }
        
        Cell.Cell GetAt(int x, int y);
    }
}