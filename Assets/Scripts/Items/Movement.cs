using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed;
   
    private bool left;
    private PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        
        left = true;
        playerController = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {//Makes the Block character go back and forth along the X Axis.
        //Only starts the movment after a set period of time.
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

           

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {// The distance he travels right and left is changed by adding box colliders with tags.
        if (collision.CompareTag("Left"))
        {
            left = true;
        }

        if (collision.CompareTag("Right"))
        {
            left = false;
        }

        if (collision.CompareTag("Player"))
        {//This moves the player along with the block when standing on top of it.
            collision.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        { //This stops the player auto moving with the block when they are no longer touching
          //the Block.
            collision.transform.SetParent(null);
        }
    }


}