using Microsoft.Unity.VisualStudio.Editor;
using Source.Data;
using Source.Flow.Search;
using Source.Grid;
using Source.GUI;
using Source.Interfaces;
using Source.Mark;
using UnityEngine;
using Zenject;
using Cell_ = Source.Cell.Cell;

namespace Source.Infrastructure.Installers
{
    [ExecuteInEditMode]
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GridConfig _config;
        [SerializeField] private Cell.Cell _cellPrefab;
        [SerializeField] private Marker _markerPrefab;
        [SerializeField] private GridController _grid;
        [SerializeField] private HUD _hud;
        [SerializeField] private float paddingFactor;
        
        public override void InstallBindings()
        {
            Container.BindFactory<Cell.Cell, Cell.Cell.Factory>().FromComponentInNewPrefab(_cellPrefab);
            // Container.Bind<IGridBuilder>().To<GridController>().FromComponentOn(_grid.gameObject).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GridController>().FromComponentOn(_grid.gameObject).AsSingle().NonLazy();
            Container.Bind<Marker>().FromComponentInNewPrefab(_markerPrefab).AsTransient();
            // Container.Bind<MarkerSpawner>().FromComponentOn(_markerPrefab.gameObject).AsSingle();
            Container.Bind<MarkerSpawner>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IMarkerService>().To<MarkerService>().AsSingle();
            Container.Bind<IDisjointSet>().To<DisjointSet>().AsSingle();
            Container.Bind<HUD>().FromComponentOn(_hud.gameObject).AsSingle().NonLazy();
            Container.Bind<float>().WithId("PaddingFactor").FromInstance(paddingFactor);
        }
    }
}