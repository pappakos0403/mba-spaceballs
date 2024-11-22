using UnityEngine;
using UnityEngine.SceneManagement;

public class ShipController : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f; // Megállítja az idõt
        SceneManager.LoadScene("PauseMenu", LoadSceneMode.Additive); // Additív betöltés
        isPaused = true;
    }

    void ResumeGame()
    {
        Time.timeScale = 1f; // Idõ visszaállítása
        SceneManager.UnloadSceneAsync("PauseMenu"); // PauseMenu Scene eltávolítása
        isPaused = false;
    }
}
