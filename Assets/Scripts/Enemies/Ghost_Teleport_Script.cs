using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Teleport_Script : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public Transform position3;
    [SerializeField]
    float ghostStayLength;
    private float ghostStayLengthCounter;
    Animator myAnim;
    private int teleportLocation;
    private Transform target;
   private bool isCountingDown;
    [SerializeField]
     float moveSpeed;
    [SerializeField]
    float targetRange;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        isCountingDown = true;
        ghostStayLengthCounter = ghostStayLength;
        myAnim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerController>().transform;
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {//Set a timer for how lonh the ghost will stay in one place.
        if (playerController.victory == false) { 
        
        if (isCountingDown) {
            ghostStayLengthCounter -= Time.deltaTime; }
        if (ghostStayLengthCounter <= 1)
        {
            myAnim.SetBool("Teleport", true);
            myAnim.SetBool("Idle", false);
        }
        if (ghostStayLengthCounter <= 0)
        {//When the timer hits zero change the animations and have him randomly appear in one of
            //three defined places.
            myAnim.SetBool("Teleport", false)
                ;
            myAnim.SetBool("Appear", true);
            ghostStayLengthCounter = ghostStayLength;
            teleportLocation = Random.Range(1, 4);
            if (teleportLocation == 1)
            {
                transform.position = position1.position;
            }

            if (teleportLocation == 2)
            {
                transform.position = position2.position;
            }

            if (teleportLocation == 3)
            {
                transform.position = position3.position;
            }
        }


        if (ghostStayLengthCounter <= ghostStayLength - 1)
        {//change the animations for when he has finished reappearing.
            myAnim.SetBool("Idle", true);
            myAnim.SetBool("Appear", false);
        }
        //Make the Ghost follow the player if they get too close;
        if (Vector3.Distance(target.position, transform.position) <= targetRange)
        {//The ghost will only follow if he has fully appeared
            if (ghostStayLengthCounter > 1)
            {
                if (ghostStayLengthCounter < ghostStayLength - 1)
                {
                    {
                        isCountingDown = false;


                        transform.position = Vector3.MoveTowards(transform.position,
                        target.transform.position, moveSpeed * Time.deltaTime);

                        //Changes the animation of the ghost to follow the player.
                        if ((target.position.x - transform.position.x) < -0.1)
                        {
                            myAnim.SetBool("Left", true);
                            myAnim.SetBool("Right", false);
                        }

                        if ((target.position.x - transform.position.x) > 0.01)
                        {
                            myAnim.SetBool("Right", true);
                            myAnim.SetBool("Left", false);
                        }

                    }
                }
            }
        }
            //Make the Ghost teleport away if the player escapes.
            if (Vector3.Distance(target.position, transform.position) > targetRange)
            {
                if (isCountingDown == false)
                {

                    isCountingDown = true;
                    ghostStayLengthCounter = 1;
                    transform.position = transform.position;
                    myAnim.SetBool("Right", false);
                    myAnim.SetBool("Left", false);
                }

            }
                
            
            
        }

    }
}
