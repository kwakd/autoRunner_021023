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

//TODO: REDO MENU
//TODO: SIMPLE SOUND EFFECTS
//TODO: MAKE OWN ASSETS

//POLISH: *CHANGE SCORE SYSTEM SO ITS BASED ON DISTANCE
//POLISH: CAMERA VALUES (WITH THE FASTER POWERUP CAMERA JITTERS)
//POLISH: goFasterPowerUp POLISH -> when collecting multiple it messes up the timing
//POLISH: RESPAWN SYSTEM (atm have an acceptable respawn system)

//======EXTRA TODO======
//TODO: ADD SMOKE EFFECT WHEN LANDING
//TODO: DUST PARTICLE EFFECT WHEN WALKING
//TODO: EASTEREGG STAGE THROUGH MENUSCREEN?
//TODO: DIFFERENT CHARACTER?
//TODO: CHARACTER SELECT SCREEN?
//TODO: STAGE? | have to find out how to make stage transitions
//TODO: SLIDE? 
//TODO: SOUND EFFECTS?

//======EXTRA POLISH======
//POLISH: JUMP DASH
//POLISH: CLEANUP CODE
//POLISH: TRAIL(WHEN DOING FIRST JUMP CANT CHANGE TRAIL COLOR)

//FOREVER: ALWAYS THINK ABOUT MOVEMENT

//GOAL: SOMETHING LIKE THIS LMAO (https://www.youtube.com/watch?v=C3JQld-qWls&t=44s)


public class testPlayerController : MonoBehaviour
{
    public  float moveSpeed;
    public  float jumpForce;
    public  float fastFallForce;
    public  float dashTime;
    public  float playerBaseSpeed;
    public  float playerBaseGravity;
    public  float speedMultiplier;
    public  float speedIncreaseMilestone;
    public  float checkRadius;

    private float speedMilestoneCount;
    private float coyoteTime = 0.5f;
    private float coyoteTimeCounter;
    private float playerTempSpeed;
    private float baseMileStone;

    public  bool  isHurt;
    public  bool  isInvincible;
    public  bool  isFall;
    public  bool  isGrounded;
    public  bool  isInviGrounded;
    public  bool  isFrontTouching;
    public  bool  countDownDone;

    private bool  isDoubleJump;
    private bool  canDash;
    
    public LayerMask      whatIsGround;
    public LayerMask      whatIsInviPlatform;
    public GameOverScript myGameOver;
    public SpriteRenderer mySprite;
    public Collider2D     myCollider;
    public Transform      groundCheck;
    public Transform      frontCheck;
    public TMP_Text       playerSpeedText;

    private Rigidbody2D   myRigidBody;
    private Animator      myAnimator;
    private HealthSystem  myHealth;
    private ScoreManager  myScoreManager;

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
        countDownDone = false;
        playerBaseSpeed = moveSpeed;
        playerTempSpeed = moveSpeed;
        playerBaseGravity = myRigidBody.gravityScale;
        speedMilestoneCount = speedIncreaseMilestone;
        baseMileStone = speedIncreaseMilestone;
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
        
        //make the player go faster
        if(transform.position.x > speedMilestoneCount)
        {
            speedMilestoneCount += speedIncreaseMilestone;
            speedIncreaseMilestone = speedIncreaseMilestone * speedMultiplier * 1.5f;
            moveSpeed = moveSpeed * speedMultiplier;
            playerTempSpeed = moveSpeed;
            //Debug.Log("PLAYER IS GOING FASTER");
        }

        //if player is alive keep moving forward
        //else player is dead stop moving
        if(!myHealth.isPlayerDead)
        {
            if(countDownDone)
            {
                myScoreManager.scoreIncreasing = true;
                myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

                if(!isFall && !isInvincible)
                {
                    myHealth.TakeDamage(0.5f * Time.deltaTime);
                }
            }
            else
            {
                myScoreManager.scoreIncreasing = false;
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y);
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
            moveSpeed = playerBaseSpeed;
            playerTempSpeed = playerBaseSpeed;
            speedIncreaseMilestone = baseMileStone;
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
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }

        //jump
        if(Input.GetButtonDown("Jump") && !myHealth.isPlayerDead && countDownDone){
            if(coyoteTimeCounter > 0f || isFrontTouching)
            {
                myTR.startColor = new Color (1f, 0f, 0f);
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
            }
            else if(isDoubleJump)
            {
                // myTR.startColor = new Color (0f, 1f, 0f);
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                isDoubleJump = !isDoubleJump;
            }
        }
        //for better jumping (releasing jump)
        if(Input.GetButtonUp("Jump") && myRigidBody.velocity.y > 0f){
            myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.5f);
            coyoteTimeCounter = 0f;
        }

        // if(Input.GetKeyDown(KeyCode.D) && canDash && (!isGrounded || !isInviGrounded)){
        //     StartCoroutine(Dash());
        // }
        if(Input.GetKeyDown(KeyCode.D) && canDash && !isGrounded){
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
            playerTempSpeed = playerBaseSpeed;
            speedIncreaseMilestone = baseMileStone;
        }

        //sets parameter values for animator 
        myAnimator.SetFloat("Speed", myRigidBody.velocity.x);
        myAnimator.SetFloat("Jumping", myRigidBody.velocity.y);

        myAnimator.SetBool("Grounded", isGrounded || isInviGrounded);
        myAnimator.SetBool("isHurt", isHurt);
        myAnimator.SetBool("DoubleJump", isDoubleJump);

        UpdateSpeedTextBox();

    }

    void UpdateSpeedTextBox()
    {
        playerSpeedText.text = Mathf.Round(moveSpeed).ToString();
    }

    private IEnumerator Dash()
    {
        myTR.startColor = Color.white;

        canDash = false;
        myRigidBody.gravityScale = 0f;

        if(isInvincible)
        {
            moveSpeed = playerTempSpeed + 5f;
        }
        else
        {
            moveSpeed = playerTempSpeed + 2f;
        }

        myRigidBody.velocity = new Vector2(transform.localScale.x, 0f);
        myTR.emitting = true;
        yield return new WaitForSeconds(dashTime);
        
        if(isInvincible)
        {
            moveSpeed = playerTempSpeed + 5f;
        }
        else
        {
            moveSpeed = playerTempSpeed;
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
