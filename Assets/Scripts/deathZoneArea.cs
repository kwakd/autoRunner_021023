using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathZoneArea : MonoBehaviour
{
    private testPlayerController myPlayerController;
    private HealthSystem myHealth;

    public GameObject thePlayer;
    public Transform respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = FindObjectOfType<testPlayerController>();
        myHealth = FindObjectOfType<HealthSystem>();
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
