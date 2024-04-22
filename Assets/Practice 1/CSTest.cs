using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CSTest : MonoBehaviour
{
    public ComputeShader computeShader;
    public RenderTexture renderTexture;
    int kernelHandle =0;
    // Start is called before the first frame update
    void Awake()
    {
        //renderTexture = new RenderTexture(256, 256, 24);
        renderTexture.enableRandomWrite = true;
        renderTexture.Create();

        computeShader.SetFloat("Resolution", renderTexture.width);
        computeShader.SetTexture(kernelHandle, "Result", renderTexture);
        computeShader.Dispatch(kernelHandle, renderTexture.width / 8, renderTexture.width / 8, 1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (renderTexture == null)
        {
    
            //renderTexture = new RenderTexture(256, 256, 24);
            renderTexture.enableRandomWrite = true;
            renderTexture.Create();
        }

        
        computeShader.SetTexture(kernelHandle, "Result", renderTexture);
        computeShader.Dispatch(kernelHandle, renderTexture.width / 8, renderTexture.width / 8, 1);
        Graphics.Blit(renderTexture, destination);
    }*/
}
