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
  
    // Start is called before the first frame update
    void Start()
    {
        timerCounter = timerLength;
        pc = FindObjectOfType<PlayerController>();
        countdown = true;
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

        //What happens when the time runs out.
        if (timerCounter <= 0)
        {
            timerCounter = 0;
            TriggerGameOver();
        }
    }

    public void TriggerGameOver()
    {
        pc.canMove = false;
        gameOverScreen.SetActive(true);
    }

}

