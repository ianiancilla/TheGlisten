using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost_Teleport_Script : MonoBehaviour
{
    public Transform position1;
    public Transform position2;
    public Transform position3;
    public float ghostStayLength;
    private float ghostStayLengthCounter;
    Animator myAnim;
    private int teleportLocation;
    // Start is called before the first frame update
    void Start()
    {
        ghostStayLengthCounter = ghostStayLength;
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {//Set a timer for how lonh the ghost will stay in one place.
        ghostStayLengthCounter -= Time.deltaTime;
        if (ghostStayLengthCounter <= 1)
        {
            myAnim.SetBool("Teleport", true);
            myAnim.SetBool("Idle", false);
        }
        if (ghostStayLengthCounter <= 0)
        {//When the timer hits zero change the animations and have him randomly appear in one of
            //three defined places.
            myAnim.SetBool("Teleport", false);
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


        if (ghostStayLengthCounter<= ghostStayLength-1)
        {//change the animations for when he has finished reappearing.
            myAnim.SetBool("Idle", true);
            myAnim.SetBool("Appear", false);
        }
    }
}
