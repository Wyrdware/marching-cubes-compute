using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void UpdateScalarRegion(float[] values);
public interface IScalarRegion 
{
    public void AddOnUpdate(UpdateScalarRegion update);
    public void UpdateRegion();
    public void SetPosition();
    public float[] GetValues();
}
