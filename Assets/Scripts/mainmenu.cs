using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
   public void NewGame()
    {
        if (Level.instance != null)
        {
            Level.instance.ResetGame();
        }

        SceneManager.LoadScene("Story");
    }
    
    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SceneSettings");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void GoToHighscoreMenu()
    {
        SceneManager.LoadScene("SceneHighscore");
    }


    public void QuitGame()
    {
        Application.Quit();
    }
}
