using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public bool intro;
    public bool outro;
    public float time;
    private const string titleScreen = "TitleScreen";
    // Start is called before the first frame update
    void Start()
    {
        if (intro)
        {
            Invoke("StartGame", time);
        }

        if (outro)
        {
            Invoke("EndGame", time);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void EndGame()
    {
        SceneManager.LoadScene(titleScreen);
    }
}
