using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EasyGames.Sources.Utils;
using UnityEngine;
using Zenject;

namespace EasyGames.Utils.Zen
{
    public class CompositeMonoInstaller : MonoInstaller
    {
        [SerializeField] private List<MonoInstaller> monoInstallers;
        [SerializeField] private List<ScriptableObjectInstaller> soInstallers;
        [Inject] private DiContainer diContainer;

        public override void InstallBindings()
        {
            GetInstallers().ForEach(Install);
        }

        private IEnumerable<IInstaller> GetInstallers()
        {
            return soInstallers
                .Cast<IInstaller>()
                .Concat(monoInstallers);
        }

        private void Install(IInstaller installer)
        {
            diContainer.Inject(installer);
            installer.InstallBindings();
        }
    }
}