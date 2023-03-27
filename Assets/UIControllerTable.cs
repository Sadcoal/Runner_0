using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerTable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Records");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Menu()
    {
        Debug.Log("Menu");
        SceneManager.LoadScene("Menu");
    }
}
