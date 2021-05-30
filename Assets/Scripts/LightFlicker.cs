using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal; // to have access to Light2D component


public class LightFlicker : MonoBehaviour
{
    [SerializeField] bool isFlickering = true;
    [SerializeField] float noiseScale = 0.1f;

    // member variables
    float noiseOffset = 3f;
    [HideInInspector] public float intensity;

    // cache
    Light2D myLight;

    private void Awake()
    {
        myLight = GetComponent<Light2D>();
    }

    private void Start()
    {
        intensity = myLight.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFlickering) { Flicker(); }
    }

    private void Flicker()
    {
        myLight.intensity = intensity * Mathf.PerlinNoise(Time.time, 0.5f);
    }
}
