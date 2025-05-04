using Source.Grid;
using Source.GUI;
using Source.Infrastructure.Pool;
using Source.Mark;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Source.Infrastructure.Installers
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private GameObject cellPrefab;
        [SerializeField] private Grid.Grid gridPrefab;
        [SerializeField] private Marker markerPrefab;
        
        public override void InstallBindings()
        {
            Container
                .BindFactory<Cell.Cell, Cell.Cell.Factory>()
                .FromNewComponentOnNewPrefab(cellPrefab)
                .AsTransient();
            
            Container
                .Bind<IBuilder>()
                .To<Grid.Grid>()
                .FromComponentOn(gridPrefab.gameObject)
                .AsSingle();
            
            Container
                .Bind<HUD>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
            
            Container
                .Bind<MarkerSpawner>()
                .FromComponentInHierarchy()
                .AsSingle()
                .NonLazy();
        }
    }
}