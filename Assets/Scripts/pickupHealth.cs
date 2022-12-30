using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupHealth : MonoBehaviour
{
    private HealthSystem myHealth;

    // Start is called before the first frame update
    void Start()
    {
        myHealth = FindObjectOfType<HealthSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2")
        {
            myHealth.GainHealth(1f);
            
            // look into setactive vs destory
            // also have to look into object pooling
            gameObject.SetActive(false);
            //Destroy(this.gameObject);
        }
    }
}
