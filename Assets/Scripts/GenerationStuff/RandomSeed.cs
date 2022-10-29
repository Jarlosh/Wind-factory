using UnityEngine;

namespace DefaultNamespace.GenerationStuff
{
    public static class RandomSeed
    {
        private const int randomSeedMax = 100000;
        private static int seed;

        public static int Seed
        {
            get => seed;
            set
            {
                if (seed == value)
                    return;
                seed = value;
                Random.InitState(value);
            }
        }

        public static int GenerateRandom()
        {
            return GenerateRandom(randomSeedMax);
        }

        public static int GenerateRandom(float max)
        {
            return Mathf.FloorToInt(Random.value * max);
        }
    }
}