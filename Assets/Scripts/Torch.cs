using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torch : MonoBehaviour
{
    Animator myAnim;
    public GameObject fire;
    SFX_Manager sfxManager;
    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        sfxManager = FindObjectOfType<SFX_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            myAnim.SetBool("Lit",true);
            fire.SetActive(true);
            sfxManager.litTorch.Play();



        }
    }
}
