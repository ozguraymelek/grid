using Source.Signals;
using UnityEngine;
using Zenject;

namespace Source.Infrastructure.Installers
{
    public class SignalInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            // Container.DeclareSignal<InteractedWithCellSignal>();
            Container.BindSignal<InteractedWithCellSignal>();
        }
    }
}
