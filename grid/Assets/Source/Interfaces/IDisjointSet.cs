using System.Collections;
using System.Collections.Generic;

namespace Source.Interfaces
{
    public interface IDisjointSet
    {
        void Add(Cell.Cell cell);
        void Remove(Cell.Cell cell);
        IEnumerable<Cell.Cell> GetCluster(Cell.Cell cell);
    }
}