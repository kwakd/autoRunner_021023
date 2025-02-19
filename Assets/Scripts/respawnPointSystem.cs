using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPointSystem : MonoBehaviour
{
    private float timer;

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
        // if(timer > 0)
        // {
        //     timer -= Time.deltaTime;
        // }
        // if(timer <= 0)
        // {
        //     updatePosition();
        //     timer = 0.25f;
        // }
        updatePosition();
    }

    void updatePosition()
    {
        //HARD CODED THE Y VARIABLE FOR NOW HAVE TO LOOK FOR A BETTER WAY FOR THE DZONE TO FOLLOW PLAYER
        if(myPlayerController.isGrounded && !myPlayerController.isHurt)
        {
            gameObject.transform.position = new Vector3(thePlayer.transform.position.x-0.5f, thePlayer.transform.position.y, 0);
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
