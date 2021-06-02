using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private const string titleScreen = "TitleScreen";
    private const string creditsScreen = "CreditsScreen";
    private const string controlsScreen = "ControlsScreen";
    private const int firstLevelIndex = 3;

    public void LoadTitleScreen()
    {
        SceneManager.LoadScene(titleScreen);
    }

    public void LoadCredits()
    {
        SceneManager.LoadScene(creditsScreen);
    }

    public void LoadControlsScreen()
    {
        SceneManager.LoadScene(controlsScreen);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void LoadFirstLevel()
    {
        SceneManager.LoadScene(firstLevelIndex);
    }

    public void LoadNextScene()
    {
        int nextSceneNr = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneNr >= SceneManager.sceneCount)
        {
            nextSceneNr = 0;
        }
        SceneManager.LoadScene(nextSceneNr);
    }


}
