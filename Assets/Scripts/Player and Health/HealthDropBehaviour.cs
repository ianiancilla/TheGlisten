using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDropBehaviour : MonoBehaviour
{
    CircleCollider2D circleCollider;
    private float random;
    private int random2;
    // Start is called before the first frame update
    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        random = Random.Range(1, 2);
        random2 = Random.Range(1, 6);

    }

    // Update is called once per frame

    private void Update()
    {
        if (random2 <= 2)
        {
            circleCollider.enabled = true;
        }
        if (random2 >= 3)
        {
            random -= Time.deltaTime;
            if (random <= 0)
            {
                circleCollider.enabled = true;
            }
        }
    }
}