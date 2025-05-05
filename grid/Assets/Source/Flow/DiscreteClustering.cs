using System.Collections.Generic;
using System.Linq;
using Source.Core.Utils;
using Source.Grid;
using Source.Interfaces;
using Zenject;

namespace Source.Flow
{
    public class DiscreteClustering : IClustering
    {
        private readonly Dictionary<Cell, Cell> _parent = new Dictionary<Cell, Cell>();
        private readonly Dictionary<Cell, int> _rank = new Dictionary<Cell, int>();
        
        private readonly IGridProvider _provider;
        
        [Inject]
        public DiscreteClustering(IGridProvider gridProvider)
        {
            _provider = gridProvider;

            SLog.InjectionStatus(this,
                (nameof(_provider), _provider)
            );
        }
        
        public void Add(Cell cell)
        {
            if (!_parent.TryAdd(cell, cell)) return;

            _rank[cell] = 0;
            
            var pos = cell.GridPosition;
            
            // union with existing marked neighbors
            foreach (var dir in cell.Neighbors)
            {
                var neighbor = _provider.GetAt(pos.x + dir.x, pos.y + dir.y);
                if (neighbor != null && neighbor.IsMarked)
                    Union(cell, neighbor);
            }
        }
        
        private void Union(Cell a, Cell b)
        {
            var ra = Find(a);
            var rb = Find(b);
            if (ra == rb) return;
            if (_rank[ra] < _rank[rb]) _parent[ra] = rb;
            else if (_rank[ra] > _rank[rb]) _parent[rb] = ra;
            else { _parent[rb] = ra; _rank[ra]++; }
        }

        Cell Find(Cell cell)
        {
            if (_parent[cell] != cell)
                _parent[cell] = Find(_parent[cell]);
            return _parent[cell];
        }

        public IEnumerable<Cell> GetCluster(Cell cell)
        {
            var root = Find(cell);
            
            // Snapshot keys to avoid collection modification during iteration,
            // because the system throws an InvalidOperationException if modification occurs during iteration.
            var keys = _parent.Keys.ToList();
            foreach (var c in keys.Where(c => Find(c) == root))
                yield return c;
        }

        public void Remove(Cell cell)
        {
            _parent.Remove(cell);
            _rank.Remove(cell);
        }
    }
}