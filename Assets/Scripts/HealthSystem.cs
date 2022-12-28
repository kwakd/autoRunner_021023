using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public bool isPlayerDead;

    public Animator myAnimator;

    // Start is called before the first frame update
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        currentHealth = maxHealth;
        isPlayerDead = false;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            isPlayerDead = true;
            //PLAY RIP ANIMATION
            myAnimator.SetBool("isDead", isPlayerDead);
            
            //TODO: SHOW GAMEOVER SCREEN
        }
    }

    public void GainHealth(float amount)
    {
        currentHealth += amount;
    }
}
