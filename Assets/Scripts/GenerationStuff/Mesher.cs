using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.GenerationStuff
{
    // test script
    public class Mesher
    {
        [Serializable]
        public class Config
        {
            public GameObject AsteroidPrefab;
            public VoxelMeshData meshData;
        }
        
        [Inject] private Generator generator;
        [Inject] private IInstantiator instantiator;
        [Inject] private Config config;
        
        private int vertexIndex = 0;
        private List<Vector3> vertices = new();
        private List<int> triangles = new();
        private List<Vector3> normals = new();
        private List<Vector2> uvs = new();

        private GameObject parentGo;
        public void GenerateMeshes()
        {
            InitParent();

            var i = 0;
            foreach (var data in generator.Data.Asteroids)
            {
                var asteroid = Spawn($"asteroid [{i}]", parentGo.transform);
                var mesh = MakeMesh(data);
                asteroid.SetMesh(mesh);
                i++;
            }
        }

        private void InitParent()
        {
            if (parentGo != null)
            {
                GameObject.Destroy(parentGo);
            }
            parentGo = new GameObject("asteroids");
        }

        private Asteroid Spawn(string name, Transform parent)
        {
            var go = instantiator.InstantiatePrefab(config.AsteroidPrefab);
            go.transform.parent = parentGo.transform;
            return go.GetComponent<Asteroid>();
        }

        private void Clear()
        {
            vertexIndex = 0;
            vertices.Clear();
            triangles.Clear();
            normals.Clear();
            uvs.Clear();
        }

        public Mesh MakeMesh(AstData data)
        {
            Clear();
            foreach (var pos in data.Bordered) 
                AddVoxel(pos, data);
            return CreateMesh();
        }

        private void AddVoxel(Vector3Int pos, AstData astData)
        {
            for (int p = 0; p < 6; p++)
            {
                int faceVertCount = 0;

                var rot = 0;
                var isNeigbourSolid = astData.Neighbours[pos][p]; // temporal
                if(!isNeigbourSolid)
                {
                    for (int i = 0; i < config.meshData.faces[p].vertData.Length; i++)
                    {
                        var vertData = config.meshData.faces[p].GetVertData(i);
                        vertices.Add(pos + vertData.GetRotatedPosition(new Vector3(0, rot, 0)));
                        normals.Add(VoxelData.FaceChecks[p]);

                        var textureID = 0; // todo: implement blockType
                        // var textureID = blockType.GetTextureID(p);

                        AddTexture(textureID, vertData.uv);
                        faceVertCount++;
                    }
                    
                    foreach (var t in config.meshData.faces[p].triangles)
                        triangles.Add(vertexIndex + t);

                    vertexIndex += faceVertCount;
                }
            }
        }
        
        public Mesh CreateMesh()
        {
            var mesh = new Mesh();
            mesh.vertices = vertices.ToArray();
            mesh.triangles = triangles.ToArray();
            // mesh.subMeshCount = 3;
            // mesh.SetTriangles(triangles.ToArray(), 0);
            // mesh.SetTriangles(triangles.ToArray(), 0);
            // mesh.SetTriangles(transparentTriangles.ToArray(), 1);
            // mesh.SetTriangles(waterTriangles.ToArray(), 2);
            mesh.uv = uvs.ToArray();
            mesh.normals = normals.ToArray();
            // mesh.colors = colors.ToArray();
            return mesh;
        }

        void AddTexture(int textureID, Vector2 uv)
        {
            float y = textureID / VoxelData.TextureAtlasSizeInBlocks;
            float x = textureID - (y * VoxelData.TextureAtlasSizeInBlocks);

            var tSize = VoxelData.NormalizedBlockTextureSize;
            x *= tSize;
            y *= tSize;

            y = 1f - y - tSize;

            x += tSize * uv.x;
            y += tSize * uv.y;

            uvs.Add(new Vector2(x, y));
        }
    }
}