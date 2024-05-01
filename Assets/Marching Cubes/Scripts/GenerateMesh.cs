using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(MeshFilter)), RequireComponent(typeof(MeshRenderer))]
public class GenerateMesh : MonoBehaviour
{
    public const int Size = 16;//Must always be a multiple of 8
    public const int Threads = 8;

    [SerializeField]
    private ComputeShader _mcShader;
    [SerializeField]
    private ScalarRegion scalarRegion;

    ComputeBuffer _trianglesBuffer;
    ComputeBuffer _trianglesCountBuffer;
    ComputeBuffer _weightsBuffer;

    public float IsoLevel = 0.5f;

    private Mesh _mesh;

    struct Triangle
    {
        public Vector3 a;
        public Vector3 b;
        public Vector3 c;

        public static int SizeOf => sizeof(float) * 3 * 3;
    }

    public void SetScalarRegion(ScalarRegion region)
    {
        scalarRegion = region;
        scalarRegion.AddOnUpdate(Generate);
        scalarRegion.UpdateRegion();
    }

    private void Awake()
    {
        _trianglesBuffer = new ComputeBuffer(5 * Size * Size * Size, Triangle.SizeOf, ComputeBufferType.Append);
        _trianglesCountBuffer = new ComputeBuffer(1, sizeof(int), ComputeBufferType.Raw);
        _weightsBuffer = new ComputeBuffer(Size * Size * Size, sizeof(float));
    }
    private void Start()
    {
        _mesh = GetComponent<MeshFilter>().mesh;
        SetScalarRegion(scalarRegion);
    }

    private void OnDestroy()
    {
        _trianglesBuffer.Release();
        _trianglesCountBuffer.Release();
        _weightsBuffer.Release();
    }
    
    private void Generate(float[] weights)
    {
        //weights = scalarRegion.GetValues();
        _mesh.Clear();


        //Get triangles from compute shader
        _mcShader.SetBuffer(0, "_Triangles", _trianglesBuffer);
        _mcShader.SetBuffer(0, "_Weights", _weightsBuffer);
        _mcShader.SetInt("_ChunkSize", Size);
        _mcShader.SetFloat("_IsoLevel", IsoLevel);
        

        _weightsBuffer.SetData(weights);
        _trianglesBuffer.SetCounterValue(0);

        _mcShader.Dispatch(0,Size/Threads,Size/Threads,Size/Threads);
        int triCount = ReadTriangleCount();
        Triangle[] triangles = new Triangle[triCount];
        _trianglesBuffer.GetData(triangles);

        // convert triangle list to mesh
        Vector3[] vertices = new Vector3[triCount*3];
        int[] trianglesV = new int[triCount*3];

        for(int i = 0; i < triangles.Length; i++)
        {
            //create vertice list
            vertices[i * 3 + 0] = triangles[i].a;
            vertices[i * 3 + 1] = triangles[i].b;
            vertices[i * 3 + 2] = triangles[i].c;

            //Create triangles list from vertice
            trianglesV[i * 3 + 0] = i * 3 + 0;
            trianglesV[i * 3 + 1] = i * 3 + 1;
            trianglesV[i * 3 + 2] = i * 3 + 2;
        }
        _mesh.vertices = vertices;
        _mesh.triangles = trianglesV;
        
        _mesh.RecalculateNormals();
        return;
    }

    private int ReadTriangleCount()
    {
        int[] triCount = { 0 };
        ComputeBuffer.CopyCount(_trianglesBuffer, _trianglesCountBuffer, 0);
        _trianglesCountBuffer.GetData(triCount);
        return triCount[0];
    }

}
