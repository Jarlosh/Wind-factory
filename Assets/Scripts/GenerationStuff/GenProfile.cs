using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.GenerationStuff
{
    [Serializable]
    public class GenProfile
    {
        public float Scale;
        public float NoiseOffset;

        public float GetNoise(Vector3 pos)
        {
            return GetNoise(pos.x, pos.y, pos.z);
        }
        
        public float GetNoise(float x, float y, float z)
        {
            // Perlin.Noise is [-1, 1], so clamp it to [0, 1]
            return (Perlin.Noise(x, y, z, NoiseOffset, Scale) + 1) / 2;
        }
    }
    
    [Serializable]
    public class CheckingGenProfile : GenProfile
    {
        [Range(0, 1)] public float Threshold;

        public bool Test(Vector3 pos)
        {
            return Test(pos.x, pos.y, pos.z);
        }

        public bool Test(float x, float y, float z)
        {
            return GetNoise(x, y, z) <= Threshold;
        }
    }
}