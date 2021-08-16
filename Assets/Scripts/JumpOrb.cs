using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    SFX_Manager sound;
    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<SFX_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            sound.whoosh.Play();
        }
    }


}
