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
    public bool canDoubleJump;

    public LayerMask whatIsGround;

    private Rigidbody2D myRigidBody;
    private Collider2D myCollider;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.IsTouchingLayers(myCollider, whatIsGround);

        //moves the player
        myRigidBody.velocity = new Vector2(moveSpeed, myRigidBody.velocity.y);

        //touches the ground
        if(isGrounded && !Input.GetButtonDown("Jump"))
        {
            canDoubleJump = true;
        }

        //jump
        if(Input.GetButtonDown("Jump")){
            if(isGrounded || canDoubleJump)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
                canDoubleJump = !canDoubleJump;
            }
            
        }
        //better jumping aka timed jumps
        if(Input.GetButtonUp("Jump") && myRigidBody.velocity.y > 0f){
           myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, myRigidBody.velocity.y * 0.5f);
        }
    }

}
