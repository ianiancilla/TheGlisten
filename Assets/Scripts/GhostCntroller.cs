﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostCntroller : MonoBehaviour
{
    public float speed;
    Animator myAnim;
   private bool left;
    
    // Start is called before the first frame update
    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        left = true;
    }

    // Update is called once per frame
    void Update()
    {//Makes the ghost character go back and forth along the X Axis.
        if (left==true)
        {
            transform.position = transform.position + new Vector3(-1, 0, 0) * Time.deltaTime * speed;
        }
        if (left == false)
        {
            transform.position = transform.position + new Vector3(1, 0, 0) * Time.deltaTime * speed;
        }

        myAnim.SetBool("Left", left);

     }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left")){
            left = true;
        }

        if (collision.CompareTag("Right"))
        {
            left = false; 
        }
    }


}