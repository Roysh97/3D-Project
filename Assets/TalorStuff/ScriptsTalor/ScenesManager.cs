using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("MainSceneTalor");
    }

    public void ExitGame()
    {
        Debug.Log("Quit game!");
        Application.Quit();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("OpeningScene");
    }

    public void TryAgain()
    {
        SceneManager.LoadScene("MainSceneTalor");
    }
}

