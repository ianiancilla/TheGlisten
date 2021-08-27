using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wizard_Boss : MonoBehaviour
{
    public bool hit1;
    public bool hit2;
   
    [SerializeField] GameObject teleport1;
    [SerializeField] GameObject teleport2;
    [SerializeField] GameObject teleport3;
    [SerializeField] GameObject cameraControl;
    public Player_HealthGlisten health;
    public int waitTime;


    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { 
            if (hit1 == false) 
                {
                    myAnim.Play("Wizard_Damage");
                    
                    gameObject.GetComponent<BoxCollider2D>().enabled = false;
                }
            if (hit1 == true)
            {
                myAnim.Play("Wizard_Damage2");
                
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (hit2 == true)
            {
                myAnim.Play("Wizard_Damage3");
                
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }

    public void WizardTeleport1()
    {
        transform.position = teleport1.GetComponent<Transform>().position;
        myAnim.Play("Wizard_Idle_Right_Appear");
        
    }

        public void WizardTeleport2()
        {
            transform.position = teleport2.GetComponent<Transform>().position;
            myAnim.Play("Wizard_Idle_Right_Appear");
            
        }

    public void WizardTeleport3()
    {
        transform.position = teleport3.GetComponent<Transform>().position;
        myAnim.Play("Wizard_Idle_Right_Appear");

    }

    public void CameraTarget() {
        cameraControl.GetComponent<Animator>().SetBool("Target", true);
        health.takesDamagePerSecond = false;
        cameraControl.GetComponent<Target_Start>().target = true;
        Invoke("CameraPlayer", waitTime);

    }

    void CameraPlayer()
    {
        cameraControl.GetComponent<Animator>().SetBool("Target", false);
        cameraControl.GetComponent<Target_Start>().target = false;
        health.takesDamagePerSecond = true;
    }
}

