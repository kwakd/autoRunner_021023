using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

// void helperSnoopy()
//     {
//             string text =
//         @"  ,-~~-.___.
//         / |  ' 	\    	 
//         (  )     	0  
//         \_/-, ,----'       	 
//             ====       	//
//         /  \-'~;	/~~~(O)
//         /  __/~|   /   	|	 
//         =(  _____| (_________|
//             )
//         ";
//    	    Debug.Log(text);
//     }

public class testPlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    public bool isGrounded;
    public bool isHurt;
    public bool isInvincible;
    public bool isFall;
    public bool isDoubleJump;

    public LayerMask whatIsGround;

    public SpriteRenderer mySprite;
    public Collider2D myCollider;
    public GameOverScript myGameOver;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private HealthSystem myHealth;

    private ScoreManager myScoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myHealth = GetComponent<HealthSystem>();

        myScoreManager = FindObjectOfType<ScoreManager>();

        Debug.Log("PLAYER moveSpeed: " + moveSpeed);
        Debug.Log("PLAYER jumpForce: " + jumpForce);
    }

    // Update is called once per frame
    void Update()
    {
        //is player touching the ground checker
        isGrounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        //if player is alive keep moving forward
        //else player is dead stop moving
        if(myHealth.isPlayerDead == false)
        {
            myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

            if(!isFall && !isInvincible)
            {
                myHealth.TakeDamage(0.5f * Time.deltaTime);
            }
        }
        else
        {
            Debug.Log("PLAYER is now RIP");
            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
            myScoreManager.scoreIncreasing = false;
            myGameOver.Setup(myScoreManager.scoreCount);

        }
        
        //if player has fallen into deathZone then freeze them in the air
        //else dont freeze
        if(isFall)
        {
            myRigidBody.isKinematic = true;
            myRigidBody.velocity = new Vector2(0, 0);
        }
        else
        {
            myRigidBody.isKinematic = false;
        }

        //if player is grounded and not pressing jump button
        if(isGrounded && !Input.GetButtonDown("Jump"))
        {
            isDoubleJump = true;
        }

        //jump
        if(Input.GetButtonDown("Jump") && !myHealth.isPlayerDead){
            if(isGrounded || isDoubleJump)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                isDoubleJump = !isDoubleJump;
            }
        }

        //for better jumping
        if(Input.GetButtonUp("Jump") && myRigidBody.velocity.y > 0f){
           myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.5f);
        }

        //sets parameter values for animator 
        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetFloat("Jumping", myRigidBody.velocity.y);

        myAnimator.SetBool("Grounded", isGrounded);
        myAnimator.SetBool("isHurt", isHurt);
        myAnimator.SetBool("DoubleJump", isDoubleJump);

        //TODO: BETTER CAMERA
        //TODO: STAGE | have to find out how to make stage transitions
        //TODO: SLIDE? 
        //TODO: JUMP DASH?
    }

}
