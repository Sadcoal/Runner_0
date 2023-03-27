using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerTitle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("SampleScene");
    }

    public void Records()
    {
        Debug.Log("Records");
        SceneManager.LoadScene("Table");
    }

    public void Quit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
