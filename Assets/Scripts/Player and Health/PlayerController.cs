using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    private Rigidbody2D myRgbdy;
    private float horizontal;
    [SerializeField]
    private float jumpForce;
    private float vertical;
    private bool climbing;
    [SerializeField]
    private float climbSpeed;
    public bool canMove;
    public GameOver gameOver;
    [SerializeField]
    private float jumpTime;
    private float jumpTimeCounter;
  private bool isJumping;
    public Target_Start cameraController;
    private SFX_Manager sfxManager;
    public bool victory;
    public GameObject flame;
    [SerializeField] LayerMask platformLayerMask;
    [SerializeField] Player_HealthGlisten health;

    // Start is called before the first frame update
    void Start()
    {
        myRgbdy = gameObject.GetComponent<Rigidbody2D>();
        canMove = false;
        gameOver = FindObjectOfType<GameOver>();
        cameraController = FindObjectOfType<Target_Start>();
        gameOver.alarmWhenLowHealth = true;
        Invoke("Move", 2f);
        sfxManager = FindObjectOfType<SFX_Manager>();
        victory = true;
        health = FindObjectOfType<Player_HealthGlisten>();

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
            if (Input.GetButtonDown("Jump") && isGrounded())
        {
                //myRgbdy.AddForce(new Vector2(0, jumpForce), ForceMode2D.Force);
                myRgbdy.velocity = new Vector2(myRgbdy.velocity.x, jumpForce);
                jumpTimeCounter = jumpTime;
                isJumping = true;
                //Plays the jump sound effect.
                sfxManager.jump.Play();     
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
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {//What will happen if the player reaches the target. The countown stops, they cant move,
        //The game object they touch disappears. The next level loads.
        if (collision.CompareTag("Target"))
        {//Turns off a lot of settings when the player reaches the goal and loads the next level.
            myRgbdy.velocity = Vector2.zero;
            gameOver.alarmWhenLowHealth = false;
            canMove = false;
            sfxManager.countdown.Stop();
            sfxManager.victory.Play();
            victory = true;
            Invoke("NextLevel", 2f);
            flame.GetComponent<SpriteRenderer>().flipX = true;
            health.takesDamagePerSecond = false;
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }
    }

    private void NextLevel()
    {//Loads the next lecel of the game.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    private void Move()
    {//Allows the player to move after the camera has finished focusing on the goal.
       
        gameOver.alarmWhenLowHealth = true;
        victory = false;
    }

   private bool isGrounded()
    {//Calculates when the player is standing on top of a platform.
        
        CapsuleCollider2D circleCollider2D = GetComponent<CapsuleCollider2D>();
        
        RaycastHit2D raycastHit = Physics2D.CapsuleCast(circleCollider2D.bounds.center, circleCollider2D.bounds.size, CapsuleDirection2D.Vertical,
           0f, Vector2.down, circleCollider2D.bounds.extents.y-0.1f, platformLayerMask);
       

        return raycastHit.collider != null;
        

           
    }
}


                
            




