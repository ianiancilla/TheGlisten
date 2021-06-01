using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Target_Start : MonoBehaviour
{
   
    private Animator myAnim;
    public bool target;
    // Start is called before the first frame update
    void Start()
    {//Makes the caamera focus on the goal at the start of every level.
        myAnim = GetComponent<Animator>();
        myAnim.SetBool("Target", true);
        Invoke("CameraPlayer", 2f);
        target = true;
    }

  
    void CameraPlayer()
    {
        myAnim.SetBool("Target", false);
        target = false;
    }
}
