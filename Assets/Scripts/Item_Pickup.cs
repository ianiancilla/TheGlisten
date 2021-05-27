using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item_Pickup : MonoBehaviour
{public float timerLength;
 private float timerCounter;
 private PlayerController pc;
 public GameObject gameOverScreen;
 public Text timer;
 private string time;
  
    // Start is called before the first frame update
    void Start()
    {
        timerCounter = timerLength;
        pc = FindObjectOfType<PlayerController>();   
    }

    // Update is called once per frame
    void Update()
    {
        time = timerCounter.ToString("F0");
        timer.text = "" + time;
        timerCounter -=Time.deltaTime;
        if (timerCounter <= 0)
        {
            timerCounter = 0;
            pc.canMove = false;
            gameOverScreen.SetActive(true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){

        }
    }
}
