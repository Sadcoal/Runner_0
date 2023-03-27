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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        int distance = Mathf.FloorToInt(player.distance);
        int coins = Mathf.FloorToInt(player.coins);
        int score = Mathf.FloorToInt(player.distance);
        distanceText.text = "count: " + distance;
        coinsText.text = "coins: " + coins;
        scoreText.text = "SCORE: " + score;
        highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt("Highscore");

        if (PlayerPrefs.GetInt("Highscore") <= score)
        {
            PlayerPrefs.SetInt("Highscore",score);
        }

        if (player.isDead)
        {
            results.SetActive(true);
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
