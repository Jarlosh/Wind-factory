using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace.GenerationStuff
{
    public static class Noise
    {
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
        
        private const float Offset = 0.1f; // didn't checked why we need it
        
        public static float GetPerlin2D(Vector2 position, float seed,  float offset, float scale)
        {
            position.x += (offset + seed + Offset);
            position.y += (offset + seed + Offset);

            return Mathf.PerlinNoise(
                position.x / VoxelData.ChunkSize * scale,
                position.y / VoxelData.ChunkSize * scale);
        }
        
        #region Oveloads
        public static bool CheckPerlin3D(Vector3 position, float offset, float scale, float threshold)
        {
            return CheckPerlin3D(position.x, position.y, position.z, offset, scale, threshold);
        }

        public static float GetPerlin3D(Vector3 position, float offset, float scale)
        {
            return GetPerlin3D(position.x, position.y, position.z, offset, scale);
        }
        #endregion
        
        public static bool CheckPerlin3D(float x, float y, float z, float offset, float scale, float threshold)
        {
            return GetPerlin3D(x, y, z, offset, scale) > threshold;
        }

        public static float GetPerlin3D(float x1, float y1, float z1, float offset, float scale)
        {
            // https://www.youtube.com/watch?v=Aga0TBJkchM Carpilot on YouTube

            // offset += seed + Offset;
            // x = (x + offset) * scale;
            // y = (y + offset) * scale;
            // z = (z + offset) * scale;

            
            float x = (x1 + offset + Seed + 0.1f) * scale;
            float y = (y1 + offset + Seed + 0.1f) * scale;
            float z = (z1 + offset + Seed + 0.1f) * scale;
            
            var AB = Mathf.PerlinNoise(x, y);
            var BC = Mathf.PerlinNoise(y, z);
            var AC = Mathf.PerlinNoise(x, z);
            var BA = Mathf.PerlinNoise(y, x);
            var CB = Mathf.PerlinNoise(z, y);
            var CA = Mathf.PerlinNoise(z, x);
            return (AB + BC + AC + BA + CB + CA) / 6f;
        }
    }
}