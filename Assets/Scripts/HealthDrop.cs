using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // to have access to Light2D component


public class HealthDrop : MonoBehaviour
{
    [Tooltip ("The scaling factor with which the light will decay. " +
              "A score of 1 means it will lose 1 point of health per second. " +
              "With a score of 0 it will never decay.")]
    [SerializeField] float decayFactor = 1f;

    // member variables
    private float healthStored = 0f;
    bool isDecaying = false;
    float spriteVerticalSqueezing = 0.8f;

    // cache
    Light2D dropLight;
    SpriteRenderer flameSprite;

    private void Awake()
    {
        // cache
        dropLight = GetComponentInChildren<Light2D>();
        flameSprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (isDecaying) { Decay(); }
    }

    public void InitialiseDrop(float proportionToMaxHealth,  
                               float healthAmount, 
                               float lightIntensity)
    {
        healthStored = healthAmount;
        dropLight.intensity = lightIntensity;
        flameSprite.transform.localScale = new Vector3(proportionToMaxHealth * spriteVerticalSqueezing,
                                                       proportionToMaxHealth,
                                                       1);
        
        if (decayFactor != 0f)
        {
            isDecaying = true;
        }
    }

    private void Decay()
    {
        dropLight.intensity -= Time.deltaTime * decayFactor;
        if (dropLight.intensity <= 0)
        {
            Destroy(this.gameObject);
        }

        float newScale = flameSprite.transform.localScale.x 
                            - Time.deltaTime * decayFactor;

        flameSprite.transform.localScale = new Vector3(newScale,
                                                       newScale,
                                                       1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player_HealthGlisten playerHealth = collision.gameObject.GetComponent<Player_HealthGlisten>();
        if (playerHealth)
        {
            playerHealth.HealDamage(healthStored);
            Destroy(this.gameObject);
        }
    }

}
