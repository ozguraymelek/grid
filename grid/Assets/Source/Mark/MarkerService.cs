using System.Linq;
using Source.Core.Utils;
using Source.Grid;
using Source.Interfaces;
using UnityEngine;
using Zenject;

namespace Source.Mark
{
    public class MarkerService : IMarkerService
    {
        private readonly MarkerSpawner _spawner;
        private readonly IClustering _clustering;
        private readonly IGridProvider _provider;
        
        [Inject]
        public MarkerService(MarkerSpawner spawner, IClustering clustering, IGridProvider provider)
        {
            _spawner = spawner;
            _clustering = clustering;
            _provider = provider;

            SLog.InjectionStatus(this,
                (nameof(_spawner), _spawner),
                (nameof(_clustering), _clustering),
                (nameof(_provider), _provider)
            );
        }

        public void AddMarker(Cell cell)
        {
            var marker = _spawner.Spawn();
            marker.gameObject.SetActive(true);
            marker.transform.SetParent(cell.transform, false);
            marker.transform.localPosition = Vector3.zero;
            
            cell.SetMarked(true);
            _clustering.Add(cell);

            var cluster = _clustering.GetCluster(cell).ToList();
            if (cluster.Count < 3) return;
            
            foreach (var c in cluster)
                RemoveMarker(c);
            
            _provider.MatchCount++;
        }

        public void RemoveMarker(Cell cell)
        {
            cell.SetMarked(false);
            var marker = cell.GetComponentInChildren<Marker>();
            _spawner.Despawn(marker);
            marker.gameObject.SetActive(false);
            marker.transform.SetParent(_spawner.transform, false);
            _clustering.Remove(cell);
        }
    }
}