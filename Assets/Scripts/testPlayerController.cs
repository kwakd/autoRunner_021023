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

//TODO: LONGER PLAYER GOES FASTER THEY GO BIT BY BIT
//TODO: SPEEDOMTER
//TODO: COUNTDOWN AT START OR LOADING SCREEN
//TODO: LOOK INTO JUMPING A LITTLE FEELS A BIT CLUNKY
//TODO: COYOTE JUMP
//TODO: LOOK INTO MAKING PLATFORMS A BIT LONGER?
//TODO: MAKE OWN ASSETS
//TODO: *CHANGE SCORE SYSTEM SO ITS BASED ON DISTANCE
//TODO: ADD SMOKE EFFECT WHEN LANDING
//TODO: DUST PARTICLE EFFECT WHEN WALKING
//TODO: *LOOK INTO GROUND LOGIC AGAIN A LITTLE -> because player can touch the paltform with their head and regain a dash or smth
//TODO: EASTEREGG STAGE THROUGH MENUSCREEN?
//TODO: DIFFERENT CHARACTER?
//TODO: CHARACTER SELECT SCREEN?
//TODO: STAGE? | have to find out how to make stage transitions
//TODO: SLIDE? 
    
//POLISH: CAMERA VALUES (WITH THE FASTER POWERUP CAMERA JITTERS)
//POLISH: ***RESPAWN SYSTEM***
//POLISH: JUMP DASH
//POLISH: goFasterPowerUp POLISH
//POLISH: MENU
//POLISH: CLEANUP CODE(make into functions)
//POLISH: GROUND LOGIC
//POLISH: TRAIL(WHEN DOING FIRST JUMP CANT CHANGE TRAIL COLOR)


//FOREVER: ALWAYS THINK ABOUT MOVEMENT

//GOAL: SOMETHING LIKE THIS LMAO (https://www.youtube.com/watch?v=C3JQld-qWls&t=44s)



public class testPlayerController : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;
    public float fastFallForce;
    public float dashTime;
    public float playerBaseSpeed;
    public float playerBaseGravity;
    public float speedMultiplier;
    public float speedIncreaseMilestone;
    public float checkRadius;

    public bool isHurt;
    public bool isInvincible;
    public bool isFall;
    public bool isGrounded;
    public bool isInviGrounded;
    public bool isFrontTouching;
    
    private float speedMilestoneCount;

    private bool isDoubleJump;
    private bool canDash;
 
    public LayerMask whatIsGround;
    public LayerMask whatIsInviPlatform;

    public GameOverScript myGameOver;

    public SpriteRenderer mySprite;
    public Collider2D myCollider;
    public Transform groundCheck;
    public Transform frontCheck;

    private Rigidbody2D myRigidBody;
    private Animator myAnimator;
    private HealthSystem myHealth;

    private ScoreManager myScoreManager;

    [SerializeField] private TrailRenderer myTR;
     
    // Start is called before the first frame update
    void Start()
    {
        mySprite = GetComponent<SpriteRenderer>();
        myCollider = GetComponent<Collider2D>();

        myRigidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myHealth = GetComponent<HealthSystem>();

        myScoreManager = FindObjectOfType<ScoreManager>();

        myTR.emitting = true;
        playerBaseSpeed = moveSpeed;
        playerBaseGravity = myRigidBody.gravityScale;
        speedMilestoneCount = speedIncreaseMilestone;
    }

    // Update is called once per frame
    void Update()
    {
        //is player touching the ground checker
        // isGrounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);
        // isInviGrounded = Physics2D.IsTouchingLayers(myCollider, whatIsInviPlatform);

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
        isInviGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsInviPlatform);
        isFrontTouching = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier;
            moveSpeed = moveSpeed * speedMultiplier;
            Debug.Log("PLAYER IS GOING FASTER");
        }

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
        if((isGrounded || isInviGrounded) && !Input.GetButtonDown("Jump"))
        {
            //myTR.emitting = false;
            myTR.startColor = new Color (0f, 1f, 0f);
            isDoubleJump = true;
            canDash = true;
        }

        //jump
        if(Input.GetButtonDown("Jump") && !myHealth.isPlayerDead){
            if(isGrounded || isDoubleJump || isFrontTouching)
            {
                myTR.startColor = new Color (1f, 0f, 0f);
                //myTR.emitting = true;
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                isDoubleJump = !isDoubleJump;
            }
        }
        //for better jumping
        if(Input.GetButtonUp("Jump") && myRigidBody.velocity.y > 0f){
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.5f);
        }

        if(Input.GetKeyDown(KeyCode.D) && canDash && (!isGrounded || !isInviGrounded)){
            StartCoroutine(Dash());
        }

        //fast falling
        if(Input.GetKeyDown(KeyCode.S) && (!isGrounded || !isInviGrounded)){
            myTR.startColor = new Color (0.35f, 1f, 0.75f);
            myTR.emitting = true;

            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y-fastFallForce);
        }

        //playerHurt
        if(isHurt)
        {
            myTR.startColor = new Color (0.35f, 0.35f, 0.35f);
        }

        //sets parameter values for animator 
        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetFloat("Jumping", myRigidBody.velocity.y);

        myAnimator.SetBool("Grounded", isGrounded || isInviGrounded);
        myAnimator.SetBool("isHurt", isHurt);
        myAnimator.SetBool("DoubleJump", isDoubleJump);

    }

    private IEnumerator Dash()
    {
        myTR.startColor = Color.white;

        canDash = false;
        myRigidBody.gravityScale = 0f;

        if(isInvincible)
        {
            moveSpeed = playerBaseSpeed + 5f;
        }
        else
        {
            moveSpeed = playerBaseSpeed + 2f;
        }

        myRigidBody.velocity = new Vector2(transform.localScale.x, 0f);
        myTR.emitting = true;
        yield return new WaitForSeconds(dashTime);
        
        if(isInvincible)
        {
            moveSpeed = playerBaseSpeed + 5f;
        }
        else
        {
            moveSpeed = playerBaseSpeed;
        }
        
        //myTR.emitting = false;
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);
        myRigidBody.gravityScale = playerBaseGravity;
    }

    
    private void OnDrawGizmosSelected()
    {   
        //GROUNDCHECK
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheck.position, checkRadius);

        //FRONTCHECK
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(frontCheck.position, checkRadius);
    }

}
