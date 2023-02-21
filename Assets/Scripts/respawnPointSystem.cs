using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPointSystem : MonoBehaviour
{
    //private float timer = 5f;

    private Animator respawnAnimator;
    private testPlayerController myPlayerController;

    public GameObject thePlayer;
    public GameObject respawnCheckerPoint;

    

    // Start is called before the first frame update
    void Start()
    {
        respawnAnimator = GetComponent<Animator>();
        myPlayerController = FindObjectOfType<testPlayerController>();
    }

    void Update()
    {
        updatePosition();
        // if(timer > 0)
        // {
        //     timer -= Time.deltaTime;
        // }
        // if(timer <= 0)
        // {
        //     updatePosition();
        //     timer = 0.1f;
        // }
    }

    void updatePosition()
    {
        //HARD CODED THE Y VARIABLE FOR NOW HAVE TO LOOK FOR A BETTER WAY FOR THE DZONE TO FOLLOW PLAYER
        if(myPlayerController.isGrounded)
        {
            gameObject.transform.position = new Vector3(thePlayer.transform.position.x, thePlayer.transform.position.y, 0);
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2")
        {
            respawnAnimator.SetBool("playerPassed", true);
        }
    }
}
