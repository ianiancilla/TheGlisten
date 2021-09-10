using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class Wizard_Boss : MonoBehaviour
{
    public bool hit1;
    public bool hit2;
   
    [SerializeField] GameObject teleport1;
    [SerializeField] GameObject teleport2;
    [SerializeField] GameObject teleport3;
    [SerializeField] GameObject cameraControl;
    public Player_HealthGlisten health;
    public PlayerController pc;
    public int waitTime;
    [SerializeField] SFX_Manager sfx;
    private Animator myAnim;

    // Start is called before the first frame update
    void Start()
    {
        myAnim = GetComponent<Animator>();
        pc = FindObjectOfType<PlayerController>();
        sfx = FindObjectOfType<SFX_Manager>();
    }

   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.CompareTag("Player")) { 
            if (hit1 == false) 
                {//Loads the first Wizard Damage Animation first time the player hits him.
                    myAnim.Play("Wizard_Damage");
                
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
              
                }
            if (hit1 == true&& hit2==false)
            {//Loads the first Wizard Damage Animation second time the player hits him.
                myAnim.Play("Wizard_Damage_2");
                
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (hit2 == true & hit1==true)
            {//Loads the third Wizard Damage Animation first time the player hits him.
                myAnim.Play("Wizard_Damae_3");
                
                gameObject.GetComponent<BoxCollider2D>().enabled = false;
                
            }
        }
    }

    public void WizardTeleport1()
    {//Teleports the Wizard to the second Location.
        transform.position = teleport1.GetComponent<Transform>().position;
        myAnim.Play("Wizard_Idle_Right_Appear");
        hit1 = true;
        sfx.wizardAppear.Play();
        sfx.WizardLaugh.Play();

    }

        public void WizardTeleport2()
    {//Teleports the Wizard to the third Location.
        transform.position = teleport2.GetComponent<Transform>().position;
            myAnim.Play("Wizard_Idle_Right_Appear");
        hit2 = true;
        sfx.wizardAppear.Play();
        sfx.WizardLaugh.Play();

    }

    public void WizardTeleport3()
    {//Teleports the Wizardand activates teh cut scene of him leaving the area.
        transform.position = teleport3.GetComponent<Transform>().position;
        GetComponent<PlayableDirector>().Play();
        sfx.wizardAppear.Play();

    }

    public void CameraTarget() {

        //Focuses the camera on the Wizard.
        cameraControl.GetComponent<Animator>().SetBool("Target", true);
        health.takesDamagePerSecond = false;
        cameraControl.GetComponent<Target_Start>().target = true;
        Invoke("CameraPlayer", waitTime);

        //Freezes the player in place.
        pc.canMove = false;
        pc.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        pc.GetComponent<Rigidbody2D>().gravityScale = 0;

    }

    void CameraPlayer()
    {//Focuses the Camera on the Player.
        cameraControl.GetComponent<Animator>().SetBool("Target", false);
        cameraControl.GetComponent<Target_Start>().target = false;
        health.takesDamagePerSecond = true;
        //Allows the player to move again.
        pc.canMove = true;
        pc.GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}

