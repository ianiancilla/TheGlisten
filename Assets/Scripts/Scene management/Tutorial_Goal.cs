using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Goal : MonoBehaviour
{
    public GameObject textBox;
    public GameObject image;
    public float timer;
    [SerializeField] PlayerController pc;
    

    // Start is called before the first frame update
    void Start()
    {//Close the textbox after a certain period of time.
        Invoke("deactivateTextBox", timer);
        pc = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
   void deactivateTextBox()
    {
        //Closes the text box and allows the player to move.
        textBox.SetActive(false);
        image.SetActive(true);
        pc.canMove = true;
    }
}
