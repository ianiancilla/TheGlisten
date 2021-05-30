using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // to have access to Light2D component

public class Player_HealthGlisten : MonoBehaviour
{
    // properties
    [SerializeField] float maxHealth = 10f;
    [Tooltip ("The prefab of the light the player spawns when receiving damage")]

    [Header ("Health drops")]
    [SerializeField] GameObject healthDropPrefab;
    [Tooltip ("In degrees, amount of variance in the direction health drops will be launched at on damage")]
    [SerializeField] float healthDropDirectionNoise = 10f;
    [SerializeField] float healthDropScatterForce = 1f;
    [SerializeField] float healthDropVerticalForceScaler = 1.5f;

    // member variables
    private float lightIntensityMax;
    private float currentHealth;

    // cache
    Light2D playerLight;
    FlameAnimation flameAnimation;

    private void Awake()
    {
        // cache
        playerLight = GetComponentInChildren<Light2D>();
        flameAnimation = GetComponent<FlameAnimation>();

    }

    // Start is called before the first frame update
    void Start()
    {
        lightIntensityMax = playerLight.intensity;
        currentHealth = maxHealth;
    }


    public void TakeDamage(float damageTaken)
    {
        // check for death
        if (damageTaken >= currentHealth)
        {
            Die();
            return;
        }

        // reduce health
        currentHealth -= damageTaken;

        // reduce light intensity proportionally to damage taken
        float damageProportion = damageTaken / maxHealth;
        float intensityChange = lightIntensityMax * damageProportion;
        playerLight.intensity -= intensityChange;

        // trigger animation
        flameAnimation.AnimateDamage();

        // spawn a HealthDrop for each point of damage taken, and make them fly off like Sonic rings
        int numberOfDrops = (int)damageTaken;
        SpawnHealthDrops(numberOfDrops);

    }

    private void SpawnHealthDrops(int numberOfDrops)
    {
        float angleOfVelocity = 180f / (numberOfDrops + 1);

        for (int i = 0; i < numberOfDrops; i++)
        {
            // create the drop
            GameObject healthDrop = Instantiate(healthDropPrefab, transform.position, Quaternion.identity);

            // shoot it off in appropriate direction, so that between them all drops form an arc
            float velocityDegrees = angleOfVelocity * (i + 1);
            velocityDegrees += +Random.Range(0 - healthDropDirectionNoise, healthDropDirectionNoise);
            velocityDegrees = Mathf.Clamp(velocityDegrees, 10, 170);

            float velocityRadians = velocityDegrees * Mathf.Deg2Rad;

            Vector2 direction = new Vector2(Mathf.Cos(velocityRadians),
                                            Mathf.Sin(velocityRadians) * healthDropVerticalForceScaler);
            healthDrop.GetComponent<Rigidbody2D>().velocity = healthDropScatterForce * direction;
        }
    }

    // commented out as feature was removed
    //public void HealDamage (float damageHealed)
    //{
    //    // increase health score
    //    currentHealth += damageHealed;

    //    // increase light intensity proportionally to damage taken
    //    float healingProportion = damageHealed / maxHealth;
    //    playerLight.intensity += lightIntensityMax * healingProportion;
    //}

    private void Die()
    {
        FindObjectOfType<GameOver>().TriggerGameOver();
    }

}
