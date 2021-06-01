using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] float damageDealt = 1f;

    [Tooltip ("If checked, this hazard will be destroyed if colliding with the player.")]
    [SerializeField] bool destroyedOnCollision = false;
    private SFX_Manager sfxManager;


    private void Start()
    {
        sfxManager = FindObjectOfType<SFX_Manager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_HealthGlisten playerHealth = collision.gameObject.GetComponent<Player_HealthGlisten>();
        if (playerHealth)
        {
            playerHealth.TakeDamage(damageDealt);
            if (damageDealt < 20)
            {
                sfxManager.lessDamage.Play();
            }
            if (damageDealt >= 20)
            {
                sfxManager.damage.Play();
            }
            

            //Debug.Log(this.gameObject.name + " collided with " + collision.gameObject.name);

                if (destroyedOnCollision)
            {
                
                Destroy(this.gameObject);
            }
        }

    }
}
