using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{ public string tutorialText;
    public TextMeshProUGUI text;
    public GameObject textBox;
    private bool displayText;
    public PlayerController player;
    [SerializeField] Player_HealthGlisten health;
    public TutorialDamage damagePlayerTutorial;
    public bool damageTutorial;
    public GameObject continueText;
    public bool ghostTutorial;
    public bool demonTutorial;
        [SerializeField] Ghost_Teleport_Script ghost;
    public GameObject[] ghostList;
    [SerializeField] DemonController demon;
   

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        health = FindObjectOfType<Player_HealthGlisten>();
        
        displayText = false;


    }

    // Update is called once per frame
    void Update()
    {
        //Create a list to store the ghosts in the level
        ghostList = GameObject.FindGameObjectsWithTag("Enemy");
        if (damageTutorial == true)
        {
            if (damagePlayerTutorial.damage == true)
            {
                
                TutorialTextAppear();
                Invoke("Continue", 1f);

                //Stops the demon from moving when tutorial loads.
                if (demonTutorial == true)
                {
                    demon.GetComponent<BoxCollider2D>().enabled = false;
                    demon.canMove = false;
                    player.GetComponent<CapsuleCollider2D>().enabled = false;
                }
            }
        }
        //This will activate when the ghost is in range and starts following the Player.
        if (ghostTutorial == true) {
            if (ghost.isCountingDown == false)
            {
                TutorialTextAppear();
                Invoke("Continue", 1f);
                ghost.moveSpeed = 0;

            }

                }

        //This code is for when the tutorial appears on the screen.
        if (displayText)
        {
            continueText.SetActive(true);
           //This closes the tutorial
            if (Input.GetButtonDown("Jump"))
            {
                if (ghostTutorial == true)
                {
                    ghost.moveSpeed = 2;
                }
                //This allows the demon to startt moving when the tutorial rext is closed.
                if (demonTutorial == true)
                {
                    demon.GetComponent<BoxCollider2D>().enabled = true;
                    demon.canMove = true;
                    player.GetComponent<CapsuleCollider2D>().enabled = true;
                }

                textBox.SetActive(false);
                //Destroys the tutorial object so it wont load again.
                Destroy(gameObject);
                //Allows the player to move again.
                player.canMove = true;
                health.takesDamagePerSecond = true;
                continueText.SetActive(false);
                displayText = false;
                player.GetComponent<Rigidbody2D>().gravityScale = 1;
               
                foreach (GameObject cc in ghostList)
                {
                    cc.GetComponent<Ghost_Teleport_Script>().moveSpeed = 2;
                }
            }
        }
    }
        
    public void Continue()
    {//Didplay the option to continue the game,
        displayText = true;
        
    }
       

    private void OnTriggerEnter2D(Collider2D collision)
    {//For when the player comes into contact with the tutorial box trigger.
        if (collision.CompareTag("Player")){

            TutorialTextAppear();
            Invoke("Continue", 2f);

            if (demonTutorial == true)
            {
                demon.GetComponent<BoxCollider2D>().enabled = false;
                demon.canMove = false;
                player.GetComponent<CapsuleCollider2D>().enabled = false;
            }


        }
        }

     void TutorialTextAppear()
    {//Loads the tutorial text.
        textBox.SetActive(true);
        //Stops the player from moving.
        player.canMove = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        player.GetComponent<Rigidbody2D>().gravityScale = 0;
        //Stops the oxygen gauge decreasing
        health.takesDamagePerSecond = false;
        text = FindObjectOfType<TextMeshProUGUI>();
        text.text = tutorialText;
        
        //Stop the ghosts from moving if a tutorial is on the screen.
       foreach(GameObject cc in ghostList)
        {
            cc.GetComponent<Ghost_Teleport_Script>().moveSpeed = 0;
        }
        

        
        
    }
            
            
    }


