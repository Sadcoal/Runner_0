using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIControllerTable : MonoBehaviour
{
    Player player;
    Text text1;
    Text text2;
    Text text3;

    private void Awake()
    {
        //player = GameObject.Find("Player").GetComponent<Player>();
        text1 = GameObject.Find("Text1").GetComponent<Text>();
        text2 = GameObject.Find("Text2").GetComponent<Text>();
        text3 = GameObject.Find("Text3").GetComponent<Text>();
    }

    void Start()
    {

    }

    void Update()
    {
        text1.text = "1: " + PlayerPrefs.GetInt("highscore");
        text2.text = "2: " + PlayerPrefs.GetInt("scr2");
        text3.text = "3: " + PlayerPrefs.GetInt("scr3");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }
}
