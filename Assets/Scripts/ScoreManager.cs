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

    // Start is called before the first frame update
    void Start()
    {
        scoreIncreasing = true;
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
}
