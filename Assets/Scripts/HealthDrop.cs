using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // to have access to Light2D component


public class HealthDrop : MonoBehaviour
{
    [Tooltip("How fast light should disappear." +
              "A score of 1 means 1 point of health disappears every second." +
              "A score of 2 means 2 points of health disappear each second." +
              "With a score of 0 health does not decay.")]
    [SerializeField] float decayFactor = 1f;

    // member variables
    private float healthStored = 0f;
    bool isDecaying = false;
    float spriteVerticalSqueezing = 0.8f;
    float lightDropPerSecond;
    float xDropPerSecond;
    float yDropPerSecond;

    // cache
    Light2D dropLight;

    private void Awake()
    {
        // cache
        dropLight = GetComponentInChildren<Light2D>();
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
        transform.localScale = new Vector3(proportionToMaxHealth * spriteVerticalSqueezing,
                                           proportionToMaxHealth,
                                           1);

        if (decayFactor != 0f)
        {
            isDecaying = true;

            lightDropPerSecond = (lightIntensity / healthStored) * decayFactor;
            xDropPerSecond = (transform.localScale.x / healthStored) * decayFactor;
            yDropPerSecond = (transform.localScale.y / healthStored) * decayFactor;
        }
    }

    private void Decay()
    {
        dropLight.intensity -= Time.deltaTime * lightDropPerSecond;

        if (dropLight.intensity <= 0)
        {
            Destroy(this.gameObject);
        }

        transform.localScale = new Vector3(transform.localScale.x - Time.deltaTime * xDropPerSecond,
                                           transform.localScale.y - Time.deltaTime * yDropPerSecond,
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
