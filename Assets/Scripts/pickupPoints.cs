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

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2")
        {
            myScoreManager.AddScore(scoreToGive);
            
            // look into setactive vs destory
            // also have to look into object pooling
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
