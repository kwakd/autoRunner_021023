using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inviPlatform : MonoBehaviour
{
    private float timer = 0.5f;
    private testPlayerController myPlayerController;

    public Transform deathZoneCoord;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = FindObjectOfType<testPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!myPlayerController.isInvincible)
        {
            GetComponent<BoxCollider2D>().enabled = false;
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
        else
        {
            GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    void updatePosition()
    {
        gameObject.transform.position = new Vector3(deathZoneCoord.transform.position.x, deathZoneCoord.transform.position.y + 2.5f, 0);
    }
}
