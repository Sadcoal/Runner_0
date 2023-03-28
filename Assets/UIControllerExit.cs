using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerExit : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cancel()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Confirm()
    {
        Debug.Log("Confirm");
        Application.Quit();
    }
}
