using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    [SerializeField] float healtIncrease = -5f;
    private Player_HealthGlisten playerHealth;
    SFX_Manager sfxManager;
    // Start is called before the first frame update
    void Start()
    {
        playerHealth = FindObjectOfType<Player_HealthGlisten>();
        sfxManager = FindObjectOfType<SFX_Manager>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerHealth = collision.gameObject.GetComponent<Player_HealthGlisten>();
        if (playerHealth)
        {
            AudioSource audio = sfxManager.healthIncrease;
            if (audio)
            {
                audio.Play();
            }
            playerHealth.TakeDamage(healtIncrease);
            Destroy(gameObject);
        }
    }

   


}