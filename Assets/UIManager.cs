using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    TMP_Text text;
    private void Start()
    {
        text.text = SceneManager.GetActiveScene().name;
    }
    public void NextScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index = (index + 1 )% 6;
        SceneManager.LoadScene( index );
    }
    public void PreviousScene()
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        index = (index - 1) % 6;
        SceneManager.LoadScene(index);
    }
}
