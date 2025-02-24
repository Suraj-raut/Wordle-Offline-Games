using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI highScoreText;
    
    private int score = 0;
    private int highScore = 0;


    void Start()
    {
       StartTheGame();
    }

    public void IncreseScore()
    {
        score++;
        scoreText.text = score.ToString();
        SaveTheHighScore();
    }

    public void SaveTheHighScore()
    {
        if(score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore",highScore);
            GetTheHighScore();
        }
        
    }

    public void GetTheHighScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highScoreText.text = highScore.ToString();
    }

    public void StartTheGame()
    {
        score = 0;
        scoreText.text = score.ToString();
        GetTheHighScore();
    }

}
