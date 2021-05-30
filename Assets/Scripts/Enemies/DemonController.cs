using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemonController : MonoBehaviour
{
    private Transform target;
    public float range;
    private Animator myAnim;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<PlayerController>().transform;
        myAnim = GetComponent<Animator>();
     

    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(target.position, transform.position) <= range)
        {
            if (target.position.x <= transform.position.x)
            {
                myAnim.SetBool("AttackLeft", true);
            }
        }
    }
}
