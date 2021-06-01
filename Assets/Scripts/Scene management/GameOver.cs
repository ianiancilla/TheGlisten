using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{public float timerLength;
 private float timerCounter;
 private PlayerController pc;
 public GameObject gameOverScreen;
 public Text timer;
 private string time;
 public bool countdown;
 public GameObject player;
 private SFX_Manager sfxManager;
    private bool alarm;
  
    // Start is called before the first frame update
    void Start()
    {
        timerCounter = timerLength;
        pc = FindObjectOfType<PlayerController>();
        countdown = false;
        sfxManager = FindObjectOfType<SFX_Manager>();
        alarm = true;
    }

    // Update is called once per frame
    void Update()
    {// Adding code so the time is displayed with no decimal points on screen.
       
        time = timerCounter.ToString("F0");
        timer.text = "" + time;

        //Makes the timer count down.
        if (countdown == true)
        {
            timerCounter -= Time.deltaTime;
        }
        //Plays the heartbeat as time starts to run out.
        if (timerCounter<= 8 && alarm==true)
        {
            sfxManager.countdown.Play();
            alarm = false;
            
        }

        //What happens when the time runs out.
        if (timerCounter <= 0)
        {
            timerCounter = 0;
           
        }
    }

    public void TriggerGameOver()
    {//Activates Game OVer. Stop the player moving, loads the game over screen
        //makes the player disappear.
        pc.canMove = false;
        gameOverScreen.SetActive(true);
        player.SetActive(false);
    }

   
    

}

