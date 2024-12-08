using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private InputField nameInputField; // Név beírásához
    [SerializeField] private Text scoreText;            // Pontszám megjelenítéséhez
    private int playerScore;

    private const string HighscoreFileName = "highscores.json";

    void Start()
    {
        playerScore = PlayerPrefs.GetInt("PlayerScore", 0);
        scoreText.text = $"Pontszám: {playerScore}";
    }

    public void GoToHighscore()
    {
        string playerName = nameInputField.text;

        // Ellenőrizzük, hogy van-e megadva név
        if (string.IsNullOrWhiteSpace(playerName))
        {
            Debug.LogWarning("Adj meg egy nevet!");
            return;
        }

        // Mentjük a játékos nevét és pontszámát a JSON fájlba
        SaveHighscore(playerName, playerScore);

        // Highscore scene betöltése
        SceneManager.LoadSceneAsync("SceneHighscore");
    }

    private void SaveHighscore(string playerName, int score)
    {
        string filePath = Path.Combine(Application.persistentDataPath, HighscoreFileName);

        // Betöltjük a meglévő adatokat, ha vannak
        List<HighscoreEntry> highscores = new List<HighscoreEntry>();
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            highscores = JsonUtility.FromJson<HighscoreList>(json).entries;
        }

        // Hozzáadjuk az új eredményt
        highscores.Add(new HighscoreEntry { name = playerName, score = score });

        // Mentjük a módosított listát
        HighscoreList highscoreList = new HighscoreList { entries = highscores };
        string newJson = JsonUtility.ToJson(highscoreList, true);
        File.WriteAllText(filePath, newJson);

        Debug.Log($"Highscore mentve: {filePath}");
    }
}

[System.Serializable]
public class HighscoreEntry
{
    public string name;
    public int score;
}

[System.Serializable]
public class HighscoreList
{
    public List<HighscoreEntry> entries = new List<HighscoreEntry>();
}
