using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class titleScreenMusic : MonoBehaviour
{
    private Scene scene;

    private static titleScreenMusic instance = null;
    public static titleScreenMusic Instance
    {
        get { return instance; }
    }
    void Awake()
    {
       
        if (scene.name == "Cutscene")
        {
            Destroy(gameObject);
        }
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Cutscene")
        {
            Destroy(gameObject);
        }
    }
}