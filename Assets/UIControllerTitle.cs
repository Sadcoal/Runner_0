using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerTitle : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void Records()
    {
        SceneManager.LoadScene("Records");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Exit");
    }
}
