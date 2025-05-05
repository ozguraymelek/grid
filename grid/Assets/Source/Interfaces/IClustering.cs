using System.Collections.Generic;
using Source.Grid;

namespace Source.Interfaces
{
    public interface IClustering
    {
        void Add(Cell cell);
        void Remove(Cell cell);
        IEnumerable<Cell> GetCluster(Cell cell);
    }
}