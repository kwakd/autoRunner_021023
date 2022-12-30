using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool canDoubleJump;

    public LayerMask whatIsGround;

    public SpriteRenderer mySprite;
    public Collider2D myCollider;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private HealthSystem myHealth;

    private ScoreManager myScoreManager;
    
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();

        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
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
            myHealth.TakeDamage(0.5f * Time.deltaTime);
        }
        else
        {
            Debug.Log("PLAYER is now RIP");
            myRigidBody.velocity = new Vector2(0, myRigidBody.velocity.y);
            myScoreManager.scoreIncreasing = false;
        }
        
        //if player is grounded and not pressing jump button
        if(isGrounded && !Input.GetButtonDown("Jump"))
        {
            canDoubleJump = true;
        }

        //jump
        if(Input.GetButtonDown("Jump") && !myHealth.isPlayerDead){
            if(isGrounded || canDoubleJump)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                canDoubleJump = !canDoubleJump;
            }
        }

        //better jumping logic
        if(Input.GetButtonUp("Jump") && myRigidBody.velocity.y > 0f){
           myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.5f);
        }

        //sets parameter values for animator purposes
        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetFloat("Jumping", myRigidBody.velocity.y);

        myAnimator.SetBool("Grounded", isGrounded);
        myAnimator.SetBool("isHurt", isHurt);
        myAnimator.SetBool("DoubleJump", canDoubleJump);


        //TODO: DEATHZONE
        //TODO: MENU
        //TODO: RESET/GAMEOVER SCREEN
        //TODO: STAGE | have to find out how to make stage transitions
        //TODO: SLIDE? 
        //TODO: JUMP DASH?
    }

}
