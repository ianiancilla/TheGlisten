using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
public class DemonController : MonoBehaviour
{
    private Transform target;
    public float attackRange;
    private Animator myAnim;
    public bool canMove;
    public float moveRange;
    public Transform homePosition;
    public float moveSpeed;
    private BoxCollider2D myBoxC;
    private SFX_Manager sfx;
    public bool following;

    PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        myAnim = GetComponent<Animator>();
        canMove = true;
        myBoxC = GetComponent<BoxCollider2D>();
        playerController = FindObjectOfType<PlayerController>();
        sfx = FindObjectOfType<SFX_Manager>();

    }

    // Update is called once per frame
    void Update()
    {//The Enemy will chase the player when they are in a certain range.
        if (playerController.victory == false)
        {
            if (Vector3.Distance(target.position, transform.position) <= moveRange)
            {
                if (canMove)
                {
                    FollowPlayer();

                }
            }

            //When the Player lleaves that range they will go back to a Home Position.
            if (Vector3.Distance(target.position, transform.position) >= moveRange)
            {
                if (canMove)
                {
                    GoHome();
                }
            }



            //If the enemy gets real close to the player, they will do the flame attck.
            if (Vector3.Distance(target.position, transform.position) < attackRange)

            {

                if ((target.position.x - transform.position.x) < -0.1)
                {
                    myAnim.SetBool("AttackLeft", true);
                    canMove = false;
                    Invoke("StopAttackLeft", 2f);
                    

                }

                if ((target.position.x - transform.position.x) > 0.01)
                {

                    myAnim.SetBool("AttackRight", true);
                    canMove = false;
                    Invoke("StopAttckRight", 2f);
                   
                }
            }
        }
    }


    void StopAttckRight()
    {//This gets invoked after the attack animation has finished.
        myAnim.SetBool("AttackRight", false);
        myAnim.SetBool("Moving", true);
        canMove = true;
    }

    void StopAttackLeft()
    {//This gets invoked after the attack animation has finished.
        myAnim.SetBool("AttackLeft", false);
        myAnim.SetBool("Moving", true);
        canMove = true;
    
    }
    public void FollowPlayer()
    {
        following = true;
        myAnim.SetFloat("MoveX", target.position.x - transform.position.x);
        myAnim.SetFloat("MoveY", target.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position,
            target.transform.position, moveSpeed * Time.deltaTime);
    }
    public void GoHome()
    {
        following = false;
        myAnim.SetFloat("MoveX", homePosition.position.x - transform.position.x);
        myAnim.SetFloat("MoveY", homePosition.position.y - transform.position.y);
        transform.position = Vector3.MoveTowards(transform.position, homePosition.transform.position, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            myBoxC.enabled = !myBoxC.enabled;
            Invoke("boxCollider2dAtive", 1f);
        }
    }
    void boxCollider2dAtive()
    {
        myBoxC.enabled = true;
    }
}
