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
    [SerializeField] Player_HealthGlisten health;
    [SerializeField] HorseDeath horseDeath;
    public bool horse;
    public GameObject continueText;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        health = FindObjectOfType<Player_HealthGlisten>();
        horseDeath = FindObjectOfType<HorseDeath>();
        displayText = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (horse == true)
        {
            if (horseDeath.horseDeath == true)
            {
                //displayText = true;
                textBoxImage.SetActive(true);
                player.canMove = false;
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                health.takesDamagePerSecond = false;
                textBox = FindObjectOfType<TextMeshProUGUI>();
                textBox.text = tutorialText;
                Invoke("Continue", 1f);
            }
        }

        if (displayText)
        {
            continueText.SetActive(true);
           
            if (Input.GetButtonDown("Jump"))
            {
                
                textBoxImage.SetActive(false);
                Destroy(gameObject);
                player.canMove = true;
                health.takesDamagePerSecond = true;
                continueText.SetActive(false);
                displayText = false;
            }
        }
    }
        
    public void Continue()
    {
        displayText = true;
        
    }
       

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){

            textBoxImage.SetActive(true);
            //displayText=true;
            player.canMove = false;
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            health.takesDamagePerSecond = false;
            textBox = FindObjectOfType<TextMeshProUGUI>();
            textBox.text = tutorialText;
            Invoke("Continue", 1f);
            //Time.timeScale = 0;

        }
        }
            
            
    }


