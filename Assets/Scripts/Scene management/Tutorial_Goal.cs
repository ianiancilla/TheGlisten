using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Goal : MonoBehaviour
{
    public GameObject textBox;
    public GameObject image;
    

    // Start is called before the first frame update
    void Start()
    {
        Invoke("deactivateTextBox", 3f);
    }

    // Update is called once per frame
   void deactivateTextBox()
    {

        textBox.SetActive(false);
        image.SetActive(true);
    }
}
