using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOrb : MonoBehaviour
{
    [SerializeField] int yVelocity;
    SFX_Manager sound;
    public int amount;
   
    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<SFX_Manager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Player_HealthGlisten>().SpawnHealthDrops(amount);


            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(
              collision.GetComponent<Rigidbody2D>().velocity.x, yVelocity);
            sound.lessDamage.Play();

            gameObject.SetActive(false);
        }
        }
    }



