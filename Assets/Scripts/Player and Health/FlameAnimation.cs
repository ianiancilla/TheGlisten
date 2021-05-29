using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlameAnimation : MonoBehaviour
{
    [Tooltip("How fast the shader will flicker when moving")]
    [SerializeField] float velocityFlickerScaler = 2f;

    [Tooltip("How much theplaye x size will increase when moving")]
    [SerializeField] float velocitySizeScaler = 0.2f;

    [Tooltip ("How long the player sprite will take to go back to idle")]
    [SerializeField] float timeToIdle = 1f;


    // member variables
    Vector3 previousPosition;
    float shaderSpeedDecrease;
    float spriteSizeDecrease;

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

        shaderSpeedDecrease = velocityFlickerScaler / timeToIdle;
        spriteSizeDecrease = velocitySizeScaler / timeToIdle;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateFlame();
    }

    private void AnimateFlame()
    {

        if (transform.position != previousPosition)
        {
            Vector3 lastMovement = (transform.position - previousPosition).normalized;

            flameSpriteRenderer.material.SetVector("_MovementSpeed", 
                                                   new Vector4(lastMovement.x * velocityFlickerScaler,
                                                               lastMovement.y * velocityFlickerScaler,
                                                               0,0));

            flameSpriteRenderer.transform.localScale = new Vector3(1 + Mathf.Abs(lastMovement.x * velocitySizeScaler),1,1);

        }
        else
        {
            // decrease local scale
            flameSpriteRenderer.transform.localScale = new Vector3(Mathf.Clamp(flameSpriteRenderer.transform.localScale.x - spriteSizeDecrease * Time.deltaTime,
                                                                               1,
                                                                               1 + velocitySizeScaler),
                                                                   1, 1);


            // slow shader speed
            //Vector4 currentShaderSpeed = flameSpriteRenderer.material.GetVector("_MovementSpeed");

            //if (currentShaderSpeed.x > 0)
            //{
            //    Debug.Log(currentShaderSpeed.x - shaderSpeedDecrease * Time.deltaTime);
            //    if (currentShaderSpeed.y > 0)
            //    {
            //        newShaderSpeed = new Vector4(currentShaderSpeed.x - shaderSpeedDecrease * Time.deltaTime,  
            //                                     currentShaderSpeed.y - shaderSpeedDecrease * Time.deltaTime,
            //                                     0, 0);
            //    }
            //    else if (currentShaderSpeed.y < 0)
            //    {
            //        newShaderSpeed = new Vector4(currentShaderSpeed.x - shaderSpeedDecrease * Time.deltaTime,
            //                                     currentShaderSpeed.y + shaderSpeedDecrease * Time.deltaTime,
            //                                     0, 0);
            //    }
            //}

            //else if (currentShaderSpeed.x < 0)
            //{
            //    if (currentShaderSpeed.y > 0)
            //    {
            //        newShaderSpeed = new Vector4(currentShaderSpeed.x + shaderSpeedDecrease * Time.deltaTime,
            //                                     currentShaderSpeed.y - shaderSpeedDecrease * Time.deltaTime,
            //                                     0, 0);
            //    }
            //    else if (currentShaderSpeed.y < 0)
            //    {
            //        newShaderSpeed = new Vector4(currentShaderSpeed.x + shaderSpeedDecrease * Time.deltaTime,
            //                                     currentShaderSpeed.y + shaderSpeedDecrease * Time.deltaTime,
            //                                     0, 0);
            //    }
            //}

            flameSpriteRenderer.material.SetVector("_MovementSpeed", Vector4.zero);
        }
        previousPosition = transform.position;
    }
}
