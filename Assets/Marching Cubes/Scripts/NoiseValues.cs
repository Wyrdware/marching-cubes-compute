using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "NoiseValues", menuName = "ScriptableObjects/NoiseValues", order = 1)]
public class NoiseValues : ScriptableObject
{
    public float Amplitude = 5f;
    public float Frequency = 0.005f;
    public int Octaves = 8;
    [Range(0f, 1f)]
    public float GroundPercent = 0.2f;
    public float ScrollRate= 1f;
    public Vector3 Offset= new Vector3(1000,1000,1000);

}
