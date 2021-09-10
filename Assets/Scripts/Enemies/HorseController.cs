using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorseController : MonoBehaviour
{
    public float speed;
    Animator myAnim;
   private bool left;
    private PlayerController playerController;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        left = true;
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {//Makes the horse character go back and forth along the X Axis.

        if (playerController.victory == false)
        {
            if (left == true)
            {
                transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * speed;
            }
            if (left == false)
            {
                transform.position = transform.position + new Vector3(1, 0, 0) * Time.deltaTime * speed;
            }

            myAnim.SetBool("Left", left);

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {// The distance he travels right and left is changed by adding box colliders with tags.
        if (collision.CompareTag("Left")){
            left = true;
        }

        if (collision.CompareTag("Right"))
        {
            left = false; 
        }

        
        }
    }


