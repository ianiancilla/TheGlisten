using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlameAnimation : MonoBehaviour
{
    [Header ("Movement animation")]
    [Tooltip("How fast the shader will flicker when moving")]
    [SerializeField] float velocityFlickerScaler = 2f;

    [Tooltip("How much theplaye x size will increase when moving")]
    [SerializeField] float velocitySizeScaler = 0.2f;

    [Tooltip ("How long the player sprite will take to go back to idle")]
    [SerializeField] float timeToIdle = 1f;

    [Header("Damage animation")]
    [SerializeField] float damageAnimationLength = 20f;
    [ColorUsageAttribute(true, true)]
    [SerializeField] Color damageColour;


    // member variables
    Vector3 previousPosition;
    float spriteSizeDecrease;
    bool isAnimatingMovement = true;

    Color startColor;
    float startNoiseScale;
    float startScatter;
    float startSmoothness;
    float startBrightness;
    float startTiling;


    float damageNoiseScale = 35.5f;
    float damageScatter = 2f;
    float damageSmoothness = 1.3f;
    float damageBrightness = 1f;
    float damageTiling = 1.5f;


    // cache
    SpriteRenderer flameSpriteRenderer;

    private void Awake()
    {
        // cache
        flameSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        previousPosition = transform.position;
        spriteSizeDecrease = velocitySizeScaler / timeToIdle;

        startColor = flameSpriteRenderer.material.GetColor("_Color");
        startNoiseScale = flameSpriteRenderer.material.GetFloat("_NoiseScale");
        startScatter = flameSpriteRenderer.material.GetFloat("_Scatter");
        startSmoothness = flameSpriteRenderer.material.GetFloat("_Smoothness");
        startBrightness = flameSpriteRenderer.material.GetFloat("_Brightness");
        startTiling = flameSpriteRenderer.material.GetFloat("_Tiling");
    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimatingMovement) { AnimateMovement(); }
    }

    private void AnimateMovement()
    {
        // using input instead of velocity to fix glitch that came up with characontroller edit. Ugly but no time
        //if (transform.position != previousPosition)
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Epsilon)
        {
            Vector3 lastMovement = (transform.position - previousPosition).normalized;

            // normalise to avoid glitching introduced by characontroller edits
            lastMovement = lastMovement.normalized;

            flameSpriteRenderer.material.SetVector("_MovementSpeed", 
                                                   new Vector4(Input.GetAxis("Horizontal") * velocityFlickerScaler,
                                                               0,0,0));

            flameSpriteRenderer.transform.localScale = new Vector3(1 + Mathf.Abs(lastMovement.x * velocitySizeScaler),1,1);
        }
        else
        {
            // decrease local scale
            flameSpriteRenderer.transform.localScale = new Vector3(Mathf.Clamp(flameSpriteRenderer.transform.localScale.x - spriteSizeDecrease * Time.deltaTime,
                                                                               1,
                                                                               1 + velocitySizeScaler),
                                                                   1, 1);

            // decrease speed
            float currentSpeed = flameSpriteRenderer.material.GetVector("_MovementSpeed").x;
            float newSpeed = Mathf.Lerp(currentSpeed, 0, 0.5f);

            if (newSpeed > Mathf.Epsilon)
            {
                flameSpriteRenderer.material.SetVector("_MovementSpeed", new Vector4(newSpeed, 0,0,0));
            }
            else
            {
                flameSpriteRenderer.material.SetVector("_MovementSpeed", Vector4.zero);
            }
        }
        previousPosition = transform.position;
    }

    public void AnimateDeath() { }

    public void AnimateDamage()
    {
        StartCoroutine(DamageAnimation());
    }

    IEnumerator DamageAnimation()
    {
        isAnimatingMovement = false;

        flameSpriteRenderer.material.SetColor("_Color", damageColour);
        flameSpriteRenderer.material.SetFloat("_NoiseScale", damageNoiseScale);
        flameSpriteRenderer.material.SetFloat("_Scatter", damageScatter);
        flameSpriteRenderer.material.SetFloat("_Smoothness", damageSmoothness);
        flameSpriteRenderer.material.SetFloat("_Brightness", damageBrightness);
        flameSpriteRenderer.material.SetFloat("_Tiling", damageTiling);

        int animationFrames = (int)(damageAnimationLength / Time.deltaTime);

        for (int i = 0; i <= animationFrames; i++)
        {
            float interpolationRatio = (float)i / (float)animationFrames;

            flameSpriteRenderer.material.SetColor("_Color", Color.Lerp(damageColour,
                                                                       startColor,
                                                                       interpolationRatio));
            flameSpriteRenderer.material.SetFloat("_NoiseScale", Mathf.Lerp(damageNoiseScale,
                                                                            startNoiseScale,
                                                                            interpolationRatio));
            flameSpriteRenderer.material.SetFloat("_Scatter", Mathf.Lerp(damageScatter,
                                                                         startScatter,
                                                                         interpolationRatio));
            flameSpriteRenderer.material.SetFloat("_Smoothness", Mathf.Lerp(damageSmoothness,
                                                                            startSmoothness,
                                                                            interpolationRatio));
            flameSpriteRenderer.material.SetFloat("_Brightness", Mathf.Lerp(damageBrightness,
                                                                            startBrightness,
                                                                            interpolationRatio));
            flameSpriteRenderer.material.SetFloat("_Tiling", Mathf.Lerp(damageTiling,
                                                                        startTiling,
                                                                        interpolationRatio));

            yield return null;
        }

        flameSpriteRenderer.material.SetColor("_Color", startColor);
        flameSpriteRenderer.material.SetFloat("_NoiseScale", startNoiseScale);
        flameSpriteRenderer.material.SetFloat("_Scatter", startScatter);
        flameSpriteRenderer.material.SetFloat("_Smoothness", startSmoothness);
        flameSpriteRenderer.material.SetFloat("_Brightness", startBrightness);
        flameSpriteRenderer.material.SetFloat("_Tiling", startTiling);

        isAnimatingMovement = true;
    }
}
