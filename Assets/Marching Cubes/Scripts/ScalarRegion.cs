using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using Unity.Mathematics;
using UnityEngine;

public class ScalarRegion : MonoBehaviour , IScalarRegion
{
    [SerializeField]
    private NoiseValues _noiseValues;



    [SerializeField]
    private int _size = 16;//Must always be a multiple of 8
    [SerializeField]
    private int _threads = 8;



    [SerializeField]
    private ComputeShader NoiseShader;

    private ComputeBuffer _weightsBuffer;
    private ComputeBuffer _modBuffer;


    private Vector4[] _points;
    private UpdateScalarRegion _update;

    private void Awake()
    {
        _weightsBuffer = new ComputeBuffer(_size * _size * _size, sizeof(float));
        _modBuffer = new ComputeBuffer(_size * _size * _size, sizeof(float)*4);
    }
    private void OnDestroy()
    {
        _weightsBuffer.Release();
    }

    private void Update()
    {
        UpdateRegion();
    }
    public float[] GetValues()
    {
        float[] noiseValue = new float[_size * _size * _size];

        NoiseShader.SetBuffer(0, "_Weights", _weightsBuffer);
        NoiseShader.SetBuffer(0, "_Mod", _modBuffer);

        NoiseShader.SetInt("_ChunkSize", _size);
        NoiseShader.SetFloat("_Amplitude", _noiseValues.Amplitude);
        NoiseShader.SetFloat("_Frequency", _noiseValues.Frequency);
        NoiseShader.SetInt("_Octaves", _noiseValues.Octaves);
        NoiseShader.SetFloat("_Ground", _noiseValues.GroundPercent);
        NoiseShader.SetVector("_Offset", transform.position+ _noiseValues.Offset);

        //_modBuffer.SetData(_points); Terrain Modification

        NoiseShader.Dispatch(0, _size/_threads, _size/_threads, _size/_threads);


        _weightsBuffer.GetData(noiseValue);


        return noiseValue;
    }
    public int GetSize()
    {
        return _size;
    }

    public void SetPosition()
    {
        throw new System.NotImplementedException();
    }

    public void AddOnUpdate(UpdateScalarRegion update)
    {
        _update += update;
    }
    public void UpdateRegion()
    {
        _update?.Invoke(GetValues());
    }
}
