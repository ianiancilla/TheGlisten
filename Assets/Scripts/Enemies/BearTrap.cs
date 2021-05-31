using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearTrap : MonoBehaviour
{
    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        
        myAnim = GetComponent<Animator>();
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {//Set the trap off when the player steps in it.
        if (collision.CompareTag("Player")){
            myAnim.SetBool("Trap", true);
            Invoke("TrapIdle", 1f);
        }
    }

    void TrapIdle()
    {
        myAnim.SetBool("Trap", false);
    }
    
}
