using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureGenerator : MonoBehaviour
{
    public ComputeShader GenerateTexture;
    public RenderTexture RTexture;
    // Start is called before the first frame update
    void Awake()
    {
        RTexture.Release();
        RTexture.enableRandomWrite = true;


        int kernelHandle = GenerateTexture.FindKernel("CSMain");

        int workGroupX = Mathf.CeilToInt(RTexture.width / 8.0f);
        int workGroupY = Mathf.CeilToInt(RTexture.height / 8.0f);

        GenerateTexture.SetTexture(kernelHandle, "Result", RTexture);
        GenerateTexture.Dispatch(kernelHandle, workGroupX, workGroupY, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
