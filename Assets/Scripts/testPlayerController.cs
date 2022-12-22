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

        if(Input.GetKeyDown(KeyCode.Space)){
            if(isGrounded)
            {
                myRigidBody.velocity = new Vector2(myRigidBody.velocity.x, jumpForce);
            }
        }
    }

}
