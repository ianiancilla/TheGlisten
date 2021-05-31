using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] float healtIncrease = -5f;
    private Player_HealthGlisten playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<Player_HealthGlisten>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_HealthGlisten playerHealth = collision.gameObject.GetComponent<Player_HealthGlisten>();
        if (playerHealth)
        {
            Debug.Log("Test");
            playerHealth.TakeDamage(healtIncrease);
            Destroy(gameObject);
        }
    }

   


}