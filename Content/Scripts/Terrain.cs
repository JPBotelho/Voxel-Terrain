using UnityEngine;
using System.Collections.Generic;

namespace MarchingCubesProject
{
    public enum MARCHING_MODE {  CUBES, TETRAHEDRON };

    public class Terrain : MonoBehaviour
    {

        public Texture2D tex;
        public Material m_material;

        List<GameObject> meshes = new List<GameObject>();

        int width = 20;
        int height = 10;
        int length = 20;

        private List<GameObject> meshObjects = new List<GameObject>();

        public float[] voxels;

        void Start()
        {
            voxels = new float[width * height * length];

            //Fill voxels with values. Im using perlin noise but any method to create voxels will work.
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < length; z++)
                    {
                        int idx = x + y * width + z * width * height;

                        float r = tex.GetPixel(x, z).grayscale;
                        float floored = Mathf.Floor(r*100)/100;
                        float diff = r-floored;
                        float currentY = y/(float)height;

                        if(currentY <= floored)
                        {
                            voxels[idx] = 1;
                        }
                        else if (currentY <= r && diff < 1)
                        {                               
                            voxels[idx] = Mathf.Max(.3f, diff);
                        }
                        else
                        {
                            voxels[idx] = 0;
                        }
                    }
                }
            }

            GenerateMeshes();
        }

        public void GenerateMeshes()
        {
            foreach(GameObject go in meshObjects)
            {
                Destroy(go);
            }
            meshObjects.Clear();
            
            List<Vector3> verts = new List<Vector3>();
            List<int> indices = new List<int>();
            Marching marching = new MarchingCubes();
            marching.Surface = 0.5f;

            marching.Generate(voxels, width, height, length, verts, indices);
           
            //A mesh in unity can only be made up of 65000 verts.
            //Need to split the verts between multiple meshes.

            int maxVertsPerMesh = 30000; //must be divisible by 3, ie 3 verts == 1 triangle
            int numMeshes = verts.Count / maxVertsPerMesh + 1;

            for (int i = 0; i < numMeshes; i++)
            {

                List<Vector3> splitVerts = new List<Vector3>();
                List<int> splitIndices = new List<int>();

                for (int j = 0; j < maxVertsPerMesh; j++)
                {
                    int idx = i * maxVertsPerMesh + j;

                    if (idx < verts.Count)
                    {
                        splitVerts.Add(verts[idx]);
                        splitIndices.Add(j);
                    }
                }

                if (splitVerts.Count == 0) continue;

                Mesh mesh = new Mesh();
                mesh.SetVertices(splitVerts);
                mesh.SetTriangles(splitIndices, 0);
                mesh.RecalculateBounds();
                mesh.RecalculateNormals();

                GameObject go = new GameObject("Mesh");
                meshObjects.Add(go);
                go.isStatic = true;
                go.transform.parent = transform;
                go.AddComponent<MeshFilter>();
                go.AddComponent<MeshRenderer>();
                go.GetComponent<Renderer>().material = m_material;
                go.GetComponent<MeshFilter>().mesh = mesh;
                go.AddComponent<MeshCollider>().sharedMesh = mesh;;
                go.transform.localPosition = Vector3.zero;
                meshes.Add(go);
            } 
        }

        public bool ValidVoxel(float x, float y, float z)
        {
            bool xx = x >= 0 && x <= width;
            bool yy = y >= 0 && y <= height;
            bool zz = z >= 0 && z <= length;

            return xx && yy && zz;
        }

        public void SetAt(Vector3 pos, float value)
        {
            if(!ValidVoxel(pos.x, pos.y, pos.z))
                return;
            
            pos.Round();


            float coords = pos.x + pos.y * width + pos.z * width * height;

            if(voxels[(int)coords] != value)
            {
                voxels[(int)coords] = value;
            }
        }

    }
}
