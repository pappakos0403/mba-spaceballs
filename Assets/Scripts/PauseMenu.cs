using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public void ResumeGame()
    {
        SceneManager.UnloadSceneAsync("PauseMenu"); // Eltávolítja a PauseMenu jelenetet
        Time.timeScale = 1f; // Idõ újraindítása
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; // Idõ visszaállítása
        SceneManager.LoadScene("MainMenu"); // Visszatérés a fõmenübe
    }
    public void GoToSettingsMenu()
    {
        SceneManager.LoadScene("SceneSettings");
    }
}
