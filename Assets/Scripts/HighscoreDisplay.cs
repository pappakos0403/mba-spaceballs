using System.Collections.Generic;
using System.IO;
using UnityEngine;
using TMPro;  // Importáljuk a TextMeshPro namespace-t

public class HighscoreDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI highscoreText; // A TextMeshProUGUI komponenst használjuk

    private const string HighscoreFileName = "highscores.json";

    void Start()
    {
        // Betöltjük és megjelenítjük a magas pontszámokat
        DisplayHighscores();
    }

    private void DisplayHighscores()
    {
        string filePath = Path.Combine(Application.persistentDataPath, HighscoreFileName);
        Debug.Log($"Fájl keresése itt: {filePath}");

        // Ellenőrizzük, hogy létezik-e a fájl
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            Debug.Log("Fájl beolvasva: " + json);

            HighscoreList highscoreList = JsonUtility.FromJson<HighscoreList>(json);
            highscoreList.entries.Sort((entry1, entry2) => entry2.score.CompareTo(entry1.score));

            string displayText = "Highscores:\n";
            for (int i = 0; i < Mathf.Min(10, highscoreList.entries.Count); i++)
            {
                displayText += $"{i + 1}. {highscoreList.entries[i].name} - {highscoreList.entries[i].score}\n";
            }

            highscoreText.text = displayText;
            Debug.Log("Magas pontszámok megjelenítve.");
        }
        else
        {
            highscoreText.text = "Nincsenek magas pontszámok!";
            Debug.LogWarning("Nincs fájl!");
        }
    }
}
