using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupPowerup : MonoBehaviour
{
    public GameObject thePlayer;

    private testPlayerController myPlayerController;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = FindObjectOfType<testPlayerController>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2")
        {
            myPlayerController.moveSpeed = myPlayerController.playerBaseSpeed + 5f;
            //Debug.Log("PLAYER POWERUP moveSpeed: " + myPlayerController.moveSpeed);

            //make object dissapear but still active for coroutine
            gameObject.transform.localScale = new Vector3(0,0,0);
            gameObject.transform.position = new Vector3(gameObject.transform.position.x+100f, gameObject.transform.position.y, 0);

            myPlayerController.isInvincible = true;
            myPlayerController.mySprite.color = Color.yellow;
            StartCoroutine("ResetPower");

        }
    }

    private IEnumerator ResetPower()
    {
        yield return new WaitForSeconds(5f);
        myPlayerController.moveSpeed = myPlayerController.playerBaseSpeed;
        myPlayerController.mySprite.color = Color.white;
        myPlayerController.isInvincible = false;
        gameObject.SetActive(false);
        Debug.Log("RESET POWER DONE");
    }
}
