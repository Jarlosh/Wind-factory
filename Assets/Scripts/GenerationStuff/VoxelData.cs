using System;
using UnityEngine;

namespace DefaultNamespace.GenerationStuff
{
    public static class VoxelData
    {
        public static int ChunkSize = 16;

        // public const int WorldSizeInChunks = 100;
        // public const int WorldSizeInVoxels = WorldSizeInChunks * ChunkSize;
        // public const int WorldCentre = (WorldSizeInChunks * ChunkSize) / 2;

        public const int TextureAtlasSizeInBlocks = 16;
        public const float NormalizedBlockTextureSize = 1f / (float)TextureAtlasSizeInBlocks;

        public static readonly Vector3[] VoxelVerts = new Vector3[8]
        {
            Vector3.zero,
            Vector3.right, 
            Vector3.left + Vector3.up,
            Vector3.up,
            Vector3.forward,
            Vector3.right + Vector3.forward,
            Vector3.one, 
            Vector3.up + Vector3.forward,
        };

        public static Vector3Int[] FaceChecks => Directions;
        
        public static readonly Vector3Int[] Directions = new Vector3Int[6]
        {
            Vector3Int.back,
            Vector3Int.forward,
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };
    }
}