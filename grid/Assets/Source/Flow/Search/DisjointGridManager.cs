using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Cell_ = Source.Cell.Cell;
namespace Source.Flow.Search
{
    public class DisjointGridManager
    {
        private Dictionary<Cell_, Cell_> parent = new Dictionary<Cell_, Cell_>();
        private Dictionary<Cell_, int> size = new Dictionary<Cell_, int>();

        private Vector2Int[] directions = new Vector2Int[]
        {
            Vector2Int.up,
            Vector2Int.down,
            Vector2Int.left,
            Vector2Int.right
        };

        public void Add(Cell_ cell)
        {
            if (!parent.ContainsKey(cell))
            {
                parent[cell] = cell;
                size[cell] = 1;

                // Komşularla birleş
                foreach (var dir in directions)
                {
                    Cell_ neighbor = cell.GetNeighbor(dir);
                    if (neighbor != null && neighbor.IsMarked)
                    {
                        Union(cell, neighbor);
                    }
                }

                // Silinecekleri topla
                if (GetSize(cell) >= 3)
                {
                    var toRemove = new List<Cell_>();
                    foreach (var c in GetAllInSameSet(cell))
                    {
                        toRemove.Add(c);
                    }

                    // Liste üzerinden sil → Artık Collection Modified hatası OLMAZ
                    foreach (var c in toRemove)
                    {
                        c.RemoveMarker();
                        Remove(c);
                    }
                }
            }
        }

        public void Remove(Cell_ cell)
        {
            parent.Remove(cell);
            size.Remove(cell);
        }

        public Cell_ Find(Cell_ cell)
        {
            if (!parent.ContainsKey(cell))
                return null;

            if (parent[cell] != cell)
                parent[cell] = Find(parent[cell]);
            
            return parent[cell];
        }

        public void Union(Cell_ a, Cell_ b)
        {
            var rootA = Find(a);
            var rootB = Find(b);

            if (rootA == rootB) return;

            if (size[rootA] < size[rootB])
            {
                parent[rootA] = rootB;
                size[rootB] += size[rootA];
            }
            else
            {
                parent[rootB] = rootA;
                size[rootA] += size[rootB];
            }
        }

        public int GetSize(Cell_ cell)
        {
            var root = Find(cell);
            return root != null ? size[root] : 0;
        }

        public IEnumerable<Cell_> GetAllInSameSet(Cell_ cell)
        {
            var root = Find(cell);
            var keys = parent.Keys.ToList();

            foreach (var key in keys)
            {
                if (Find(key) == root)
                {
                    yield return key;
                }
            }
        }
    }
}