using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FlameAnimation : MonoBehaviour
{
    [SerializeField] float velocityScaler = 1f;

    // member variables
    Vector3 previousPosition;

    // cache
    SpriteRenderer flameSpriteRenderer;
    Rigidbody2D rb;

    private void Awake()
    {
        // cache
        flameSpriteRenderer = GetComponentInChildren<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        previousPosition = transform.position;

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

            flameSpriteRenderer.material.SetVector("_MovementSpeed", new Vector4(lastMovement.x * velocityScaler,                                                                                 lastMovement.y * velocityScaler));
        }
        else
        {
            flameSpriteRenderer.material.SetVector("_MovementSpeed", new Vector4(0,0));
        }
        previousPosition = transform.position;
    }
}
