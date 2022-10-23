using UnityEngine;

namespace DefaultNamespace.GenerationStuff
{
    public class Asteroid : MonoBehaviour
    {
        public MeshRenderer meshRenderer;
        public MeshFilter meshFilter;

        public void SetMesh(Mesh mesh)
        {
            meshFilter.mesh = mesh;
        }
    }
}