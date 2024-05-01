using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViaualScalarRegion : MonoBehaviour
{
    [SerializeField]
    ScalarRegion renderTarget;

    public GameObject VisPrefab;
    float[] _weights;
    GameObject[] _vis;
    // Start is called before the first frame update
    void Start()
    {
        _weights = renderTarget.GetValues();
        _vis = new GameObject[_weights.Length];
        for (int x = 0; x < renderTarget.GetSize(); x++)
        {
            for (int y = 0; y < renderTarget.GetSize(); y++)
            {
                for (int z = 0; z < renderTarget.GetSize(); z++)
                {
                    int index = x + y * renderTarget.GetSize() + z * renderTarget.GetSize() * renderTarget.GetSize();
                    float weight = _weights[index];
                    _vis[index] = Instantiate(VisPrefab, new Vector3(x, y, z), Quaternion.identity);
                    _vis[index].GetComponent<Renderer>().material.color = new Color(weight, weight, weight);

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        _weights = renderTarget.GetValues();
        if (_weights == null || _weights.Length == 0) return;
        for (int x = 0; x < renderTarget.GetSize(); x++)
        {
            for (int y = 0; y < renderTarget.GetSize(); y++)
            {
                for (int z = 0; z < renderTarget.GetSize(); z++)
                {
                    int index = x + y * renderTarget.GetSize() + z * renderTarget.GetSize() * renderTarget.GetSize();
                    float weight = Mathf.Clamp01(_weights[index]);
                    _vis[index].GetComponent<Renderer>().material.color = new Color(weight, weight, weight);

                }
            }
        }
    }
    private void OnDrawGizmos()
    {

        
    }


}
