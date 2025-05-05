using System.Collections.Generic;
using System.Linq;
using Source.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Mark
{
    public class MarkerService : IMarkerService
    {
        private readonly MarkerSpawner _spawner;
        private readonly IDisjointSet _disjoint;
        private readonly HashSet<Marker> _marked = new HashSet<Marker>();
        
        [Inject]
        public MarkerService(MarkerSpawner spawner, IDisjointSet disjoint)
        {
            _spawner = spawner;
            _disjoint = disjoint;
        }

        public void AddMarker(Cell.Cell cell)
        {
            var marker = _spawner.Spawn();
            marker.gameObject.SetActive(true);
            marker.transform.SetParent(cell.transform, false);
            marker.transform.localPosition = Vector3.zero;
            
            cell.SetMarked(true);
            _marked.Add(marker);
            _disjoint.Add(cell);

            // if cluster >= 3, remove
            var cluster = _disjoint.GetCluster(cell).ToList();
            if (cluster.Count >= 3)
                foreach (var c in cluster)
                    RemoveMarker(c);
        }

        public void RemoveMarker(Cell.Cell cell)
        {
            cell.SetMarked(false);
            var marker = cell.GetComponentInChildren<Marker>();
            _spawner.Despawn(marker);
            marker.gameObject.SetActive(false);
            marker.transform.SetParent(_spawner.transform, false);
            _disjoint.Remove(cell);
            _marked.Remove(cell.GetComponentInChildren<Marker>());
        }
    }
}
