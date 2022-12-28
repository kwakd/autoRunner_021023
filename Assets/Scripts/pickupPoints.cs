using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPoints : MonoBehaviour
{

    public int scoreToGive;

    private ScoreManager myScoreManager;


    // Start is called before the first frame update
    void Start()
    {
        myScoreManager = FindObjectOfType<ScoreManager>();
    }

    // // Update is called once per frame
    // void Update()
    // {
        
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2")
        {
            myScoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);
        }
    }
}
