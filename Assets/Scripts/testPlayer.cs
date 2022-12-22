using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public float gravity;
    public float groundHeight = 1;
    public float jumpVelocity = 5;

    public bool isGrounded = false;

    public Vector2 velocity;

    // Start is called before the first frame update
    // void Start()
    // {
        
    // }

    // Update is called once per frame
    void Update()
    {
        if(isGrounded){
            if(Input.GetKeyDown(KeyCode.Space))
            {
                isGrounded = false;
                velocity.y = jumpVelocity;
            }
        }
    }

    private void FixedUpdate(){
        Vector2 pos = transform.position;

        if(!isGrounded){
            pos.y += velocity.y * Time.fixedDeltaTime;
            velocity.y += gravity * Time.fixedDeltaTime;

            if(pos.y <= groundHeight){
                pos.y = groundHeight;
                isGrounded = true;
            }
        }

        transform.position = pos;
    }
}
