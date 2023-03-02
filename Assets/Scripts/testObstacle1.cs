using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testObstacle1 : MonoBehaviour
{
    private testPlayerController myPlayerController;
    private HealthSystem myHealth;

    [SerializeField] private AudioSource   gameOverSoundEffect;

    // Start is called before the first frame update
    void Start()
    {
        myPlayerController = FindObjectOfType<testPlayerController>();
        myHealth = FindObjectOfType<HealthSystem>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "testPlayer2" && myPlayerController.isInvincible == false)
        {
            myPlayerController.moveSpeed = myPlayerController.moveSpeed/2;
            myPlayerController.isHurt = true;
            myPlayerController.mySprite.color = Color.red;
            gameOverSoundEffect.Play();
            myHealth.TakeDamage(5f);

            StartCoroutine(ResetIFrame());
        }
    }

    private IEnumerator ResetIFrame()
    {
        yield return new WaitForSeconds(0.5f);
        myPlayerController.moveSpeed = myPlayerController.playerBaseSpeed;
        myPlayerController.isHurt = false;
        myPlayerController.mySprite.color = Color.white;
        Debug.Log("PLAYER moveSpeed: " + myPlayerController.moveSpeed);
    }

}
