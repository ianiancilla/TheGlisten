using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D myRgbdy;
    private float horizontal;
    public float jumpForce;
    private float vertical;
    private bool climbing;
    public float climbSpeed;
    public bool canMove;
    public GameOver gameOver;
    public float jumpTime;
    private float jumpTimeCounter;
    private bool isJumping;
    public Target_Start cameraController;
    // Start is called before the first frame update
    void Start()
    {
        myRgbdy = gameObject.GetComponent<Rigidbody2D>();
        canMove = false;
        gameOver = FindObjectOfType<GameOver>();
        cameraController = FindObjectOfType<Target_Start>();
        gameOver.countdown = true;
        Invoke("Move", 2f);

    }

    
    
    // Update is called once per frame
    void Update()
    {
        
        // Moves the player left or right at a certain speed.

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (canMove==true)
        {
            myRgbdy.velocity = new Vector2(horizontal* moveSpeed, myRgbdy.velocity.y);

            //Makes sure the player can only jump when they are on the ground.
            if (Input.GetButtonDown("Jump") && (Mathf.Abs(myRgbdy.velocity.y) < 0.001f))
        {
                //myRgbdy.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
                myRgbdy.velocity = new Vector2(myRgbdy.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                isJumping = true;
        }

            if (Input.GetButton("Jump") && isJumping == true){
                if (jumpTimeCounter > 0)
                {
                    myRgbdy.velocity = new Vector2(myRgbdy.velocity.x, jumpForce);
                    jumpTimeCounter -= Time.deltaTime;
                }
                else { isJumping = false; }
            }
            if (Input.GetButtonUp("Jump")){
                isJumping = false;
            }
            // Makes the player able to climb ladders
            if (climbing == true)
            {
                myRgbdy.velocity = new Vector2(myRgbdy.velocity.x, vertical * Time.deltaTime * climbSpeed);
            }

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
    {//detects when the player is next to a ladder.
        if (collision.CompareTag("Ladder"))
        {
            climbing = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {//Remove climbing and turn gravity for the player back on.
            climbing = false;
            myRgbdy.gravityScale = 1f;
        }

      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//What will happen if the player reaches the target. The countown stops, they cant move,
        //The game object they touch disappears. The next level loads.
        if (collision.CompareTag("Target"))
        {
            myRgbdy.velocity = Vector2.zero;
            gameOver.countdown = false;
            canMove = false;
            Debug.Log("Test");
            
            Invoke("NextLevel", 2f);
        }
    }

    private void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    private void Move()
    {
        canMove = true;
        gameOver.countdown = true;
    }
}


                
            




