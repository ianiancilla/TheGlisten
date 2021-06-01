using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTrap : MonoBehaviour
{
    public Animator myAnim;
    Rigidbody2D myRgbdy;
    public BoxCollider2D iceBox;
    // Start is called before the first frame update
    void Start()
    {
        
        myRgbdy=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (myAnim.GetBool("Impact") == true)
        {//Stops the ice from falling when it hits a platform / player. Destroys ice.
            myRgbdy.gravityScale = 0;
            myRgbdy.velocity = Vector2.zero;
            Invoke("DestroyIce", 0.5f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {//Drops the ice when the player walks under it.
            myRgbdy.gravityScale = 1;
        }
                
    }
    void DestroyIce()
    {
        Destroy(gameObject);
    }
}
