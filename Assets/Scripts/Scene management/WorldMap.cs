using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMap : MonoBehaviour
{
    public float waitTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("NextLevel", waitTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void NextLevel()
    {//Loads the next lecel of the game.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
