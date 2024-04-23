using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Source: https://polycoding.net/compute-shaders-unity
public class CubeGrid : MonoBehaviour
{
    public GameObject CubePrefab;

    public Transform Cubetransform;
    public ComputeShader CubeShader;

    private ComputeBuffer _cubesPositionBuffer;

    public int CubesPerAxis = 80;
    public int Repetitions = 1000;

    private Transform[] _cubes;

    private float[] _cubesPositions;

    public bool UseGPU;
    private void Awake()
    {
        _cubesPositionBuffer = new ComputeBuffer(CubesPerAxis * CubesPerAxis, sizeof(float));
    }
    private void OnDestroy()
    {
        _cubesPositionBuffer.Release();
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateGrid();
    }
    
    void CreateGrid()
    {
        _cubes = new Transform[CubesPerAxis*CubesPerAxis];
        _cubesPositions = new float[CubesPerAxis * CubesPerAxis];

        for(int x =0, i = 0; x< CubesPerAxis; x++)
        {
            for(int z=0; z< CubesPerAxis; z++, i++) 
            {
                _cubes[i] = Instantiate(CubePrefab, transform).transform;
                _cubes[i].position = new Vector3(x, 0, z);
            }
        }
        StartCoroutine(UpdateCubeGrid());
    }
    IEnumerator UpdateCubeGrid()
    {
        while (true)
        {


            if (UseGPU)
            {
                UpdateGPU();
            }
            else
            {
                UpdateCPU();
            }
            for (int i = 0; i < _cubes.Length; i++)
            {
                _cubes[i].localPosition = new Vector3(_cubes[i].localPosition.x, _cubesPositions[i], _cubes[i].localPosition.z);
            }
            yield return null;
        }
    }

    void UpdateGPU()
    {
        CubeShader.SetBuffer(0, "_Positions", _cubesPositionBuffer);

        CubeShader.SetInt("_CubesPerAxis", CubesPerAxis);
        CubeShader.SetInt("_Repetitions", Repetitions);
        CubeShader.SetFloat("_Time", Time.deltaTime);

        int workgroups = Mathf.CeilToInt(CubesPerAxis / 8.0f);
        CubeShader.Dispatch(0, workgroups, workgroups, 1);

        _cubesPositionBuffer.GetData(_cubesPositions);
    }
    void UpdateCPU()
    {
        for (int i = 0; i < _cubes.Length; i++)
        {
            for (int j = 0; j < Repetitions; j++)
            {
                _cubesPositions[i] = Random.Range(-1f, 1f);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
