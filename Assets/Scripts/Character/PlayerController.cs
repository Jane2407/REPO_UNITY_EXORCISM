using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool drawDebugRaycasts = true;	    //Should the environment checks be visualized

    Rigidbody2D rb;                             //The rigidbody component                 

    [Header("Movement Properties")]
    private int maxVel = 5;                     //Player x max velocity
    private int minVel = -5;                    //Player x min velocity

    [Header("Jump Properties")]
    public int jumpForce = 9;                   //Initial force of jump

    [Header("Status Flags")]
    public bool isOnGround;                     //Is the player on the ground?
    public bool isJumping;                      //Is player jumping?
    bool isFacingRight = true;				    //Direction player is facing
    public bool isHaging;                       //Is player haging on wall?

    [Header("Environment Check Properties")]
    public float footOffsetX = .03f;            //X Offset of feet raycast
    public float footOffsetY = -.67f;           //Y Offset of feet raycast
    public float groundDistance = -.2f;         //Distance player is considered to be on the ground
    public LayerMask groundLayer;               //Layer of the ground
    public float eyeHeight = -.1f;              //Height of wall checks
    public float grabDistance = .4f;            //The reach distance for wall grabs

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Check the environment to determine status
        PhysicsCheck();

        //Process ground and air movements
        GroundMovement();
        MidAirMovement();


        //Limit x velocity from -5 to 5
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, minVel, maxVel), rb.velocity.y);

        if (rb.velocity.x > 0 && !isFacingRight)
            FlipCharacter();
        else if (rb.velocity.x < 0 && isFacingRight)
            FlipCharacter();
    }

    void FlipCharacter()
    {
        isFacingRight = !isFacingRight;
        //Flipping the character by rotation y
        transform.Rotate(0f, 180f, 0f);
    }


    void PhysicsCheck()
    {
        //Cast ray for the foot
        RaycastHit2D groundCheck = Raycast(new Vector2(footOffsetX, footOffsetY), Vector2.down, groundDistance);

        //Changing when player on ground or not
        if (groundCheck)
        {
            isOnGround = true;
            isJumping = false;
        }
        else
            isOnGround = false;

        //Determine the direction of the wall grab attempt
        Vector2 grabDir = new Vector2(1f, 0f);
        //Cast rays to look for a wall grab
        RaycastHit2D rightWallCheck = Raycast(new Vector2(footOffsetX, eyeHeight), grabDir, grabDistance);
        RaycastHit2D leftWallCheck = Raycast(new Vector2(footOffsetX, eyeHeight), -grabDir, grabDistance);

        if (rightWallCheck || leftWallCheck)
            isHaging=true;
        else
            isHaging = false;
    }

    void GroundMovement()
    {
        //Chacking is player able to move on ground
        if (isHaging&&!isOnGround)
            return;

        //Add force for horizontal movement
        rb.AddForce(Vector2.right * Input.GetAxis("Horizontal"+ GetComponent<PlayerData>().playerIndex), ForceMode2D.Impulse);
    }

    void MidAirMovement()
    {
        if (GetComponent<PlayerData>().playerIndex == 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (isOnGround)
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                else if (!isOnGround && !isJumping)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    isJumping = true;
                }
            }
        }
        else if(GetComponent<PlayerData>().playerIndex == 1)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (isOnGround)
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                else if (!isOnGround && !isJumping)
                {
                    rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
                    isJumping = true;
                }
            }
        }
        
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length)
    {
        //Call the overloaded Raycast() method using the ground layermask and return the results
        return Raycast(offset, rayDirection, length, groundLayer);
    }

    RaycastHit2D Raycast(Vector2 offset, Vector2 rayDirection, float length, LayerMask mask)
    {
        //Record the player's position
        Vector2 pos = transform.position;

        //Send out the desired raycasr and record the result
        RaycastHit2D hit = Physics2D.Raycast(pos + offset, rayDirection, length, mask);

        //Show raycast in scene
        if (drawDebugRaycasts)
        {
            //...determine the color based on if the raycast hit...
            Color color = hit ? Color.red : Color.green;
            //...and draw the ray in the scene view
            Debug.DrawRay(pos + offset, rayDirection * length, color);
        }
        //Return the results of the raycast
        return hit;
    }

    public void Die()
    {
        //Adding velocity for player fliyng up
        rb.velocity = new Vector3(0, 3, 0);
        rb.gravityScale = 0;
    }
}
