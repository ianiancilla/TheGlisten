using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // to have access to Light2D component


public class LightFlicker : MonoBehaviour
{
    [SerializeField] bool isFlickering = true;
    [SerializeField] float noiseScale = 0.1f;
    [SerializeField] float flickerSpeed = 20f;
    [SerializeField] float startingIntensity = 2f;



    // member variables
    float noiseOffset;
    float intensity;

    // cache
    Light2D myLight;

    private void Awake()
    {
        myLight = GetComponent<Light2D>();

        myLight.intensity = startingIntensity;
        intensity = myLight.intensity;
    }

    private void Start()
    {
        noiseOffset = Random.Range(0f, 3f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering) { Flicker(); }
    }

    private void Flicker()
    {
        myLight.intensity = intensity + (Mathf.PerlinNoise(Time.time * flickerSpeed + noiseOffset,
                                                           0.5f)
                                         * noiseScale * intensity);
    }

    public void SetIntensity (float newIntensity) { intensity = newIntensity; }
    public float GetIntensity() { return intensity; }
}
