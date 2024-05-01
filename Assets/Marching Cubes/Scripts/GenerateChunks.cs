using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateChunks : MonoBehaviour
{
    [SerializeField]
    private GameObject _chunk;
    [SerializeField]
    private int _generateSize;
    // Start is called before the first frame update
    void Start()
    {
        for(int x = 0; x < _generateSize; x++)
        {
            for(int y = 0 ; y < _generateSize; y++)
            {
                Vector3 position= new Vector3 (x, 0, y) * (GenerateMesh.Size-1);
                Instantiate(_chunk, position, Quaternion.identity);
            }
        }
    }

}
