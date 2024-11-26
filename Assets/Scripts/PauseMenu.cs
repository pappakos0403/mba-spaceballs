using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("PauseMenu"); // Elt�vol�tja a PauseMenu jelenetet
        Time.timeScale = 1f; // Id� �jraind�t�sa
    }

    public void ExitGame()
    {
        Application.Quit(); // Kilépés
    }
    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SceneSettings");
    }
}
