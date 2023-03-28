using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    Player player;
    Text distanceText;
    Text coinsText;
    Text scoreText;
    Text highscoreText;
    GameObject results;

    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        distanceText = GameObject.Find("DistanceText").GetComponent<Text>();
        coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        highscoreText = GameObject.Find("HighScoreText").GetComponent<Text>();
        results = GameObject.Find("Results");

        results.SetActive(false);
    }

    void Start()
    {
        
    }

    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        int coins = Mathf.FloorToInt(player.coins);
        int score = Mathf.FloorToInt(player.distance);
        distanceText.text = "count: " + distance;
        coinsText.text = "coins: " + coins;
        scoreText.text = "SCORE: " + score;
        highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("highscore");

        if (player.isDead)
        {
            results.SetActive(true);

            if (PlayerPrefs.GetInt("highscore") <= score)
            {
                PlayerPrefs.SetInt("highscore", score);
            }
            if (PlayerPrefs.GetInt("highscore") > score && PlayerPrefs.GetInt("scr2") <= score)
            {
                PlayerPrefs.SetInt("scr2", score);
            }
            if (PlayerPrefs.GetInt("highscore") > score && PlayerPrefs.GetInt("scr2") > score && PlayerPrefs.GetInt("scr3") <= score)
            {
                PlayerPrefs.SetInt("scr3", score);
            }
        }
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Retry()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
