using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Target_Start : MonoBehaviour
{
    [SerializeField] Player_HealthGlisten health;

    private Animator myAnim;
    public bool target;[SerializeField] int waitTime;
    // Start is called before the first frame update
    void Start()
    {//Makes the caamera focus on the goal at the start of every level.
        myAnim = GetComponent<Animator>();
        myAnim.SetBool("Target", true);
        Invoke("CameraPlayer",waitTime);
        target = true;
        health = FindObjectOfType<Player_HealthGlisten>();
        health.takesDamagePerSecond = false;
    }

  
    void CameraPlayer()
    {
        myAnim.SetBool("Target", false);
        target = false;
        health.takesDamagePerSecond = true;
    }
}
