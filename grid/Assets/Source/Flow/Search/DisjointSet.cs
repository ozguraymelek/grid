using System.Collections.Generic;
using System.Linq;
using Source.Interfaces;

namespace Source.Flow.Search
{
    public class DisjointSet : IDisjointSet
    {
        private readonly Dictionary<Cell.Cell, Cell.Cell> _parent = new Dictionary<Cell.Cell, Cell.Cell>();
        private readonly Dictionary<Cell.Cell, int> _rank = new Dictionary<Cell.Cell, int>();
        
        public void Add(Cell.Cell cell)
        {
            if (_parent.ContainsKey(cell)) return;
            _parent[cell] = cell;
            _rank[cell] = 0;
            
            // union with existing marked neighbors
            foreach (var dir in cell.Neighbors)
            {
                var neighbor = cell.GetNeighbor(dir);
                if (neighbor != null && neighbor.IsMarked)
                    Union(cell, neighbor);
            }
        }
        
        void Union(Cell.Cell a, Cell.Cell b)
        {
            var ra = Find(a);
            var rb = Find(b);
            if (ra == rb) return;
            if (_rank[ra] < _rank[rb]) _parent[ra] = rb;
            else if (_rank[ra] > _rank[rb]) _parent[rb] = ra;
            else { _parent[rb] = ra; _rank[ra]++; }
        }

        Cell.Cell Find(Cell.Cell cell)
        {
            if (_parent[cell] != cell)
                _parent[cell] = Find(_parent[cell]);
            return _parent[cell];
        }

        public IEnumerable<Cell.Cell> GetCluster(Cell.Cell cell)
        {
            var root = Find(cell);
            // Snapshot keys to avoid collection modification during iteration
            var keys = _parent.Keys.ToList();
            foreach (var c in keys)
            {
                if (Find(c) == root)
                    yield return c;
            }
        }

        public void Remove(Cell.Cell cell)
        {
            _parent.Remove(cell);
            _rank.Remove(cell);
        }
    }
}