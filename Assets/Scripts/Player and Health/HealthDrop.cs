using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HealthDrop : MonoBehaviour
{
    [Tooltip("Whether the light will decay e disappear in time.")]
    [SerializeField] bool decays = true;

    [Tooltip("How many seconds the light stays on screen." +
              "A score of 1 means light goes off in 1 second." +
              "A score of 2 means 2 points of health disappear each second." +
              "With a score of 0 health does not decay.")]
    [SerializeField] float decayTime = 5f;
    [Tooltip ("A measure of the variance in decay time of flames, added for a more natural effect." +
              "At 1 all flames have the same decay time, at 2 the longest living flames live double the assigned time.")]
    [SerializeField] float decayVariance = 1.2f;

    float lifeLeft;
    float maxIntensity;

    // cache
    LightFlicker dropLight;

    private void Awake()
    {
        // cache
        dropLight = GetComponentInChildren<LightFlicker>();
    }

    private void Start()
    {
        decayTime *= decayVariance;
        lifeLeft = decayTime;
        maxIntensity = dropLight.GetIntensity();
    }

    private void Update()
    {
        if (decays) { Decay(); };
    }

    private void Decay()
    {
        lifeLeft -= Time.deltaTime;

        if (lifeLeft <= 0) { Destroy(gameObject); }

        float currentProportion = lifeLeft / decayTime;
        dropLight.SetIntensity(maxIntensity * currentProportion);

        transform.localScale = new Vector3(currentProportion, currentProportion, 1);
    }
}
