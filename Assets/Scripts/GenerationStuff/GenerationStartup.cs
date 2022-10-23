using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;
using Random = UnityEngine.Random;

namespace DefaultNamespace.GenerationStuff
{
    public class GenerationStartup : IInitializable, ITickable
    {
        [Inject] private Generator generator;
        [Inject] private Config config;
        [Inject] private Mesher mesher;

        public void Initialize()
        {
            VoxelData.ChunkSize = config.ChunkSize;
            Noise.Seed = config.RandomSeed ? Mathf.FloorToInt(Random.value * int.MaxValue) : config.Seed;
            Generate();
        }

        private void Generate()
        {
            if (config.RandomSeed)
            {
                Noise.Seed = Mathf.FloorToInt(Random.value * int.MaxValue);
            }
            var startSize = config.StartSize;
            for (int x = 0; x < startSize.x; x++)
            for (int y = 0; y < startSize.y; y++)
            for (int z = 0; z < startSize.z; z++)
            {
                GenerateChunk(x, y, z);
            }
            generator.Honk();
            mesher.GenerateMeshes();
        }

        private void GenerateChunk(int x, int y, int z)
        {
            generator.GenerateChunk(new Vector3Int(x, y, z));            
        }

        public void Tick()
        {
            if (Input.GetKeyDown(KeyCode.G)) 
                Generate();
            if (Input.GetKeyDown(KeyCode.R))
                SceneManager.LoadScene(0);
        }

        [Serializable]
        public class Config
        {
            public bool RandomSeed = false;
            [HideIf(nameof(RandomSeed))]
            public int Seed = 0;
            public int ChunkSize = 16;
            public Vector3 StartSize = Vector3.one * 5;
        }
    }
}