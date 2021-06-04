using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Cutscene : MonoBehaviour
{
    public bool intro;
    public bool outro;
    private const string titleScreen = "TitleScreen";
    // Start is called before the first frame update
    void Start()
    {
        if (intro)
        {
            Invoke("StartGame", 14f);
        }

        if (outro)
        {
            Invoke("EndGame", 13f);
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
