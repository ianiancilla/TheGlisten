using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialText : MonoBehaviour
{ public string tutorialText;
    public TextMeshProUGUI textBox;
    public GameObject textBoxImage;
    private bool displayText;
    public PlayerController player;
    [SerializeField]
        Player_HealthGlisten health;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        health = FindObjectOfType<Player_HealthGlisten>();
    }

    // Update is called once per frame
    void Update()
    {
        if (displayText)
        {

            textBox = FindObjectOfType<TextMeshProUGUI>();
            textBox.text = tutorialText;
            if (Input.GetButtonDown("Jump"))
            {
               textBoxImage.SetActive(false);
                Destroy(gameObject);
                player.canMove = true;
                health.takesDamagePerSecond = true;
                Time.timeScale = 1;
            }
        }
    }
        

       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){

            textBoxImage.SetActive(true);
            displayText=true;
            player.canMove = false;
            //player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            //health.takesDamagePerSecond = false;
            Time.timeScale = 0;
            
            }
        }
            
            
    }


