using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // to have access to Light2D component

public class Player_HealthGlisten : MonoBehaviour
{
    // properties
    [SerializeField] float maxHealth = 10f;
    [Tooltip ("The prefab of the light the player spawns when receiving damage")]
    [SerializeField] GameObject HealthDropPrefab;

    // member variables
    private float lightIntensityMax;
    private float currentHealth;

    // cache
    Light2D playerLight;

    private void Awake()
    {
        // cache
        playerLight = GetComponentInChildren<Light2D>();
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

        // spawn HealthDrop and set its intensity
        Vector3 dropPosition = CalculateDropPosition();
        GameObject healthDrop = Instantiate(HealthDropPrefab, dropPosition, Quaternion.identity);
        healthDrop.GetComponent<HealthDrop>().InitialiseDrop(damageProportion, damageTaken, intensityChange);
    }

    public void HealDamage (float damageHealed)
    {
        // increase health score
        currentHealth += damageHealed;

        // increase light intensity proportionally to damage taken
        float healingProportion = damageHealed / maxHealth;
        playerLight.intensity += lightIntensityMax * healingProportion;
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    private Vector3 CalculateDropPosition()
    {
        return transform.position + new Vector3(0, -1, 0);
    }
}
