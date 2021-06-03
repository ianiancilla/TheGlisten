using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceImpact : MonoBehaviour
{
    SFX_Manager sfxManager;
    Animator myAnim;
   
    // Start is called before the first frame update
    void Start()
    {
        myAnim = gameObject.GetComponent<Animator>();
        sfxManager = FindObjectOfType<SFX_Manager>();
      
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") || collision.CompareTag("Floor"))
        {
            myAnim.SetBool("Impact", true);
            sfxManager.iceBreak.Play();

        }
    }


}
