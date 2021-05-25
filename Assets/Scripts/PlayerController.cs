using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRgbdy;
    private float horizontal;
    public float jumpForce;
    private float vertical;
    private bool ladder;
    private bool climbing;
    public float climbSpeed;
    // Start is called before the first frame update
    void Start()
    {
        myRgbdy = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {// Moves the player left or right at a certain speed.

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        transform.position = transform.position + new Vector3(horizontal, 0, 0) * Time.deltaTime * moveSpeed;

        //This was added as for some reason the player kept having X velocity when bouncing off of things.
        if (horizontal == 0)
        {
            myRgbdy.velocity = new Vector2(0, myRgbdy.velocity.y);
        }

        // Makes the player jump
        if (Input.GetButtonDown("Jump") && (Mathf.Abs(myRgbdy.velocity.y) < 0.001f))
        {
            myRgbdy.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        // Makes the player able to climb ladders
        if (climbing == true)
        {
            myRgbdy.velocity = new Vector2(myRgbdy.velocity.x, vertical * Time.deltaTime * climbSpeed);

        }
    }

    private void FixedUpdate()
    {
        if (climbing == true)
        {
            //Remove Gravity so the player does not instantly fall while climbing.
            myRgbdy.gravityScale = 0f;

        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            climbing = true;


        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            climbing = false;
            myRgbdy.gravityScale = 1f;


        }


    }

   
    }


                
            




