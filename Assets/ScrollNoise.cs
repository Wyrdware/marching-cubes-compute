using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollNoise : MonoBehaviour
{
    [SerializeField] 
    private NoiseValues _noiseValues;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _noiseValues.Offset += new Vector3(0, _noiseValues.ScrollRate, 0)*Time.deltaTime;
    }
}
