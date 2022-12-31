using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnPointSystem : MonoBehaviour
{
    private Animator respawnAnimator;

    // Start is called before the first frame update
    void Start()
    {
        respawnAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2")
        {
            respawnAnimator.SetBool("playerPassed", true);
        }
    }
}
