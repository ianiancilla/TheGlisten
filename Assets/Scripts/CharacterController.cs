using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRgbdy;
    private float horizontal;
    public float jumpForce;
    // Start is called before the first frame update
    void Start()
    {
        myRgbdy = gameObject.GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {// Moves the player left or right at a certain speed.

        horizontal = Input.GetAxis("Horizontal");
        transform.position= transform.position+ new Vector3(horizontal,0,0)*Time.deltaTime*moveSpeed;

        //This was added as for some reason the plaer kept having X velocity when bouncing off of things.
        if (horizontal == 0)
        {
            myRgbdy.velocity = new Vector2(0, myRgbdy.velocity.y);
        }

            // Makes the player jump
            if (Input.GetButtonDown("Jump") && (Mathf.Abs(myRgbdy.velocity.y) < 0.001f))
            {
                myRgbdy.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }


        }
    }

