using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZoneArea : MonoBehaviour
{
    private float timer = 0.5f;

    private testPlayerController myPlayerController;
    private HealthSystem myHealth;
    
    public GameObject thePlayer;
    public Transform respawnPoint;
    public Transform spawnPlatform;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = FindObjectOfType<testPlayerController>();
        myHealth = FindObjectOfType<HealthSystem>();
    }

    void Update()
    {
        if(!myPlayerController.isFall)
        {
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            if(timer <= 0)
            {
                updatePosition();
                timer = 0.5f;
            }
        }
    }

    void updatePosition()
    {
        //HARD CODED THE Y VARIABLE FOR NOW HAVE TO LOOK FOR A BETTER WAY FOR THE DZONE TO FOLLOW PLAYER
        gameObject.transform.position = new Vector3(thePlayer.transform.position.x, spawnPlatform.transform.position.y-5f, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2")
        {
            myHealth.TakeDamage(1f);
            myPlayerController.isFall = true;

            StartCoroutine(RespawnPlayer());
        }
    }

    private IEnumerator RespawnPlayer()
    {
        yield return new WaitForSeconds(1f);
        thePlayer.transform.position = respawnPoint.position;
        myPlayerController.isFall = false;
    }
}
