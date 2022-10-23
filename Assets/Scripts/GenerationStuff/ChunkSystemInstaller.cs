using UnityEngine;
using Zenject;

namespace DefaultNamespace.GenerationStuff
{
    [CreateAssetMenu(menuName = "SO/Installers/ChunkSystemInstaller", fileName = "ChunkSystemInstaller", order = 0)]
    public class ChunkSystemInstaller : ScriptableObjectInstaller<ChunkSystemInstaller>
    {
        [SerializeField] GenerationStartup.Config startupConfig;
        [SerializeField] Mesher.Config mesherConfig;
        [SerializeField] GenConfig generatorConfig;

        public override void InstallBindings()
        {
            InstallConfigs();
            Container.BindInterfacesAndSelfTo<GenerationStartup>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<Mesher>().AsSingle();
            Container.Bind<Generator>().AsSingle();
        }

        private void InstallConfigs()
        {
            Container.BindInstances(
                startupConfig, 
                generatorConfig, 
                mesherConfig
            );
        }
    }
}