using Source.Data;
using UnityEngine;
using Zenject;

namespace Source.Core.Infrastructure.Installers
{
    [CreateAssetMenu(fileName = "DataInstaller", menuName = "Installers/Data")]
    public class DataInstaller : ScriptableObjectInstaller<DataInstaller>
    {
        [SerializeField] private GridConfig gridConfig;
        
        public override void InstallBindings()
        {
            Container.BindInstance(gridConfig).AsSingle().NonLazy();
        }
    }
}
