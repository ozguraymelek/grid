using System;
using Source.Data;
using Source.Flow;
using Source.Grid;
using Source.GUI;
using Source.Interfaces;
using Source.Mark;
using UnityEngine;
using Zenject;

namespace Source.Core.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private Cell cellPrefab;
        [SerializeField] private Marker markerPrefab;
        [SerializeField] private GridController grid;
        [SerializeField] private HUD hud;
        [SerializeField] private float paddingFactor;

        public override void InstallBindings()
        {
            Container.BindFactory<Cell, Cell.Factory>().FromComponentInNewPrefab(cellPrefab);
            Container.BindInterfacesAndSelfTo<GridController>().FromComponentOn(grid.gameObject).AsSingle().NonLazy();
            Container.Bind<Marker>().FromComponentInNewPrefab(markerPrefab).AsTransient();
            Container.Bind<MarkerSpawner>().FromComponentInHierarchy().AsSingle();
            Container.Bind<IMarkerService>().To<MarkerService>().AsSingle();
            Container.Bind<IClustering>().To<DiscreteClustering>().AsSingle();
            Container.Bind<HUD>().FromComponentOn(hud.gameObject).AsSingle().NonLazy();
            Container.Bind<float>().WithId("PaddingFactor").FromInstance(paddingFactor);
        }
    }
}