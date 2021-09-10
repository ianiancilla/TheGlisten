using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_HealthGlisten : MonoBehaviour
{
    // properties
    [SerializeField] float maxHealth = 10f;
    public bool takesDamagePerSecond = true;
    [SerializeField] float damagePerSecond = 1f;

    [Header("UI")]
    [SerializeField] Image oxygenGauge;

    [Header ("Health drops")]
    [Tooltip("The prefab of the light the player spawns when receiving damage")]
    [SerializeField] GameObject healthDropPrefab;
    [Tooltip ("In degrees, amount of variance in the direction health drops will be launched at on damage")]
    [SerializeField] float healthDropDirectionNoise = 10f;
    [SerializeField] float healthDropScatterForce = 1f;
    [SerializeField] float healthDropVerticalForceScaler = 1.5f;

    // member variables
    private float lightIntensityMax;
    private float currentHealth;

    // cache
    LightFlicker playerLight;
    FlameAnimation flameAnimation;

    private void Awake()
    {
        // cache
        playerLight = GetComponentInChildren<LightFlicker>();
        flameAnimation = GetComponent<FlameAnimation>();
    }

    // Start is called before the first frame update
    void Start()
    {
        lightIntensityMax = playerLight.GetIntensity();
        currentHealth = maxHealth;
        UpdateOxygenGauge();
    }

    private void Update()
    {
        //Makes the timer count down.
        if (takesDamagePerSecond)
        {
            TakeDamage(Time.deltaTime * damagePerSecond);
        }
    }

    private void UpdateOxygenGauge()
    {
        // update gauge
        oxygenGauge.fillAmount = currentHealth / maxHealth;
    }

    /// <summary>
    /// Used to both deal and heal damage! pisitive numbers deal damage,
    /// negative numbers will heal the player.
    /// Player cannot go over starting health score.
    /// </summary>
    /// <param name="damageTaken"></param>
    public void TakeDamage(float damageTaken)
    {
        // reduce health
        currentHealth -= damageTaken;

        // check for overhealing
        if (currentHealth > maxHealth) { currentHealth = maxHealth; }

        UpdateOxygenGauge();

        DeathCheck();

        // reduce light intensity proportionally to damage taken
        float newIntensity = lightIntensityMax * (currentHealth / maxHealth);
        playerLight.SetIntensity(newIntensity);


        // stuff that only happens on monster collisions. This is ugly and dirty I KNOW
        if (damageTaken > damagePerSecond*Time.deltaTime *2)
        {
            // trigger animation
            flameAnimation.AnimateDamage();

            // spawn a HealthDrop for each point of damage taken, and make them fly off like Sonic rings
            int numberOfDrops =2* (int)damageTaken;
            SpawnHealthDrops(numberOfDrops);
        }
    }

    public void SpawnHealthDrops(int numberOfDrops)
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

    private void Die()
    {
        FindObjectOfType<GameOver>().TriggerGameOver();
    }

    private void DeathCheck()
    {
        // check for death
        if (currentHealth <= 0)
        {
            Die();
            return;
        }
    }

    public float GetCurrentHealth () { return currentHealth; }
    public float GetMaxHealth() { return maxHealth; }
}
