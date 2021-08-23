using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Yellow_Jump_Orb : MonoBehaviour
{[SerializeField] int yVelocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<Rigidbody2D>().velocity = new Vector2(
                collision.GetComponent<Rigidbody2D>().velocity.x, yVelocity);
        }
    }
}
