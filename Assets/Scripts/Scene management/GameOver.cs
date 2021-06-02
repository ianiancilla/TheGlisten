using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    // properties
    public GameObject gameOverScreen;
    public GameObject player;
    [Tooltip ("At what percentage of health left the heartbeat alarm will be triggered.")]
    [SerializeField] float healthPercentageForAlarm = 10;

    // variables
    public bool countdown;
    private bool alarm;
    private float alarmThreshold;

    // cache
    private PlayerController pc;
    private Player_HealthGlisten playerHealth;
    private SFX_Manager sfxManager;

    // Start is called before the first frame update
    void Start()
    {
        // cache
        pc = FindObjectOfType<PlayerController>();
        playerHealth = pc.GetComponent<Player_HealthGlisten>();
        sfxManager = FindObjectOfType<SFX_Manager>();

        // set starting state
        countdown = false;
        alarm = true;

        alarmThreshold = (playerHealth.GetMaxHealth() * healthPercentageForAlarm) / 100f;
    }

    // Update is called once per frame
    void Update()
    {
        //Plays the heartbeat as time starts to run out.
        bool isDying = playerHealth.GetCurrentHealth() <= alarmThreshold;
        if (isDying && alarm==true)
        {
            sfxManager.countdown.Play();
            alarm = false;       
        }

    }

    public void TriggerGameOver()
    {
        //Activates Game OVer. Stop the player moving, loads the game over screen
        //makes the player disappear.
        pc.canMove = false;
        gameOverScreen.SetActive(true);
        player.SetActive(false);
        sfxManager.countdown.Stop();
    }

    // trimmed timer text code
    //public Text timer;
    //public float timerLength;
    //private string time;
    //private float timerCounter;


    // in start
    // timerCounter = timerLength;

    // in update
    // Adding code so the time is displayed with no decimal points on screen.
    //time = timerCounter.ToString("F0");
    //timer.text = "" + time;

}

