using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainmenu : MonoBehaviour
{
   public void NewGame()
    {
        SceneManager.LoadScene("Level1");
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
