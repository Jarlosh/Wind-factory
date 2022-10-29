using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.GenerationStuff
{
    public class Generator
    {
        [Inject] private GenConfig config;
        private int ChunkSize => VoxelData.ChunkSize;
        
        private Dictionary<Vector3Int, bool> solidTests = new();
        private HashSet<Vector3Int> generatedChunks = new();
        public readonly GenData Data = new();

        private Vector3Int offset;
        private Vector3Int[] directions => VoxelData.Directions;
        
        public void GenerateChunk(Vector3Int chunkPos)
        {
            offset = chunkPos * ChunkSize;
            if(config.IsClever)
            {
                LowResScan();
            }
            else
            {
                JustAdd();
            }
            generatedChunks.Add(chunkPos);
        }

        private void JustAdd()
        {
            for (int x = 0; x < ChunkSize; x++)
            for (int y = 0; y < ChunkSize; y++)
            for (int z = 0; z < ChunkSize; z++)
            {
                var point = new Vector3Int(offset.x + x, offset.y + y, offset.z + z);
                if (IsSolid(point))
                {
                    var ast = new AstData();
                    var nei = new bool[6];
                    for (int i = 0; i < 6; i++)
                    {
                        nei[i] = (IsSolid(point + directions[i]));
                    }
                    ast.Bordered.Add(point);
                    ast.Neighbours[point] = nei;
                    Data.Asteroids.Add(ast);
                }
            }
        }

        private void LowResScan()
        {
            var step = config.lowResStep;
            var lowResCount = ChunkSize / step;
            for (int x = 0; x < lowResCount; x++)
            for (int y = 0; y < lowResCount; y++)
            for (int z = 0; z < lowResCount; z++)
            {
                var point = new Vector3Int(offset.x + x*step, offset.y + y*step, offset.z + z*step);
                if (IsSolid(point))
                {
                    point = FindBorderPoint(point);
                    if(AnyAsteroidBorderContains(point))
                        continue;
                    
                    var asteroid = new AstData();
                    Data.Asteroids.Add(asteroid);
                    CollectBorderLine(asteroid, point);
                }
            }
        }

        public void Honk()
        {
            var r = $"Result, {Data.Asteroids.Count}\n";
            var s = Data.Asteroids
                .Select(a => $"{a.Bordered.Count}");
            r += string.Join('\n', s);
            Debug.Log(r);
        }

        private void CollectBorderLine(AstData asteroid, Vector3Int start)
        {
            int directionsCount = directions.Length;
            var stack = new Stack<Vector3Int>();
            stack.Push(start);
            var tested = new HashSet<Vector3Int>();
            var neigbourSolidness = new bool[directionsCount];
            
            // yea I know we don't need to check first point, surelly its border, but i.d.c.
            while (stack.TryPop(out var point) && asteroid.Bordered.Count < 1000)
            {
                var isBorder = false;
                for (var i = 0; i < directionsCount; i++)
                {
                    var isSolid = neigbourSolidness[i] = IsSolid(point + directions[i]);
                    isBorder |= !isSolid;
                }
                
                if (isBorder || config.FillAsteroids)
                {
                    asteroid.Bordered.Add(point);

                    for (int i = 0; i < directionsCount; i++)
                    {
                        if (neigbourSolidness[i])
                        {
                            var neighbour = point + directions[i];
                            if (!tested.Contains(neighbour))
                            {
                                stack.Push(neighbour);
                            }
                        }
                    }

                    asteroid.Neighbours[point] = neigbourSolidness;
                    neigbourSolidness = new bool[6];
                }

                tested.Add(point);
            }
        }

        private bool AnyAsteroidBorderContains(Vector3Int borderPoint)
        {
            for (var index = 0; index < Data.Asteroids.Count; index++)
            {
                var a = Data.Asteroids[index];
                if (a.Bordered.Contains(borderPoint))
                    return true;
            }
            return false;
        }

        private Vector3Int FindBorderPoint(Vector3Int point)
        {
            // go right (arbitrary). Soon we must reach border
            var next = point;
            next.x++;
            var steps = 1;
            while (IsSolid(next))
            {
                point = next;
                next.x++;
                
                steps++;
                if (steps > 1000)
                {   // surelly there are a problem
                    throw new Exception();
                }
            }
            return point;
        }

        private bool IsSolid(Vector3Int point)
        {
            if (solidTests.TryGetValue(point, out var result)) 
                return result;
            return solidTests[point] = config.highResProfile.Test(point.x, point.y, point.z);
        }
    }
    
    public class GenData
    {
        public List<AstData> Asteroids = new();
    }
        
    public class AstData
    {
        public Dictionary<Vector3Int, bool[]> Neighbours = new();
        public HashSet<Vector3Int> Bordered = new();
    }
    
    [Serializable]
    public class GenConfig
    {
        public CheckingGenProfile lowResProfile;
        public CheckingGenProfile highResProfile;
        public int lowResStep = 4;
        public bool FillAsteroids = false;
        public bool IsClever = true;
    }
}