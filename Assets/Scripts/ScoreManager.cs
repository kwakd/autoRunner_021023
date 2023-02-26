using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float scoreCount;
    public float pointsPerSec;

    public bool scoreIncreasing;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreIncreasing = true;
        updateHighScoreText();
    }

    // Update is called once per frame
    void Update()
    {
        if(scoreIncreasing)
        {
            scoreCount += pointsPerSec * Time.deltaTime;
        }
        
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }

    public void AddScore(int pointsToAdd)
    {
        scoreCount += pointsToAdd;
        checkHighScore();
    }

    void checkHighScore()
    {
        if(scoreCount > PlayerPrefs.GetInt("HighScore", 0))
        {
            int tempInt = (int)Mathf.Round(scoreCount);
            PlayerPrefs.SetInt("HighScore", tempInt);
            updateHighScoreText();
        }
    }

    void updateHighScoreText()
    {
        highScoreText.text = $"HighScore: {PlayerPrefs.GetInt("HighScore", 0)}";
    }
}
