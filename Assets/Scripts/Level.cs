using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class LevelData
{
    public int levelNumber;
    public string title;
    public string description;
}

[System.Serializable]
public class LevelTextData
{
    public List<LevelData> levels;
}

public class Level : MonoBehaviour
{
    public static Level instance;

    uint numDestructables = 0;
    bool startNextLevel = false;
    float nextLevelTimer = 3f;

    string[] levels = { "Level1", "Level2", "Level3" };
    int currentLevel = 1;

    int score = 0;
    Text scoreText;
    int health = 3;
    Text healthText;

    [SerializeField] private Text levelIntroText;
    [SerializeField] private Text levelIntroDescription;
    [SerializeField] private CanvasGroup levelIntroPanel; 
    private float introDisplayTime = 3f;
    private LevelTextData levelTextData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            healthText = GameObject.Find("HealthText").GetComponent<Text>();

            TextAsset jsonFile = Resources.Load<TextAsset>("LevelTexts");
            if (jsonFile != null)
            {
                levelTextData = JsonUtility.FromJson<LevelTextData>(jsonFile.text);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        ShowLevelIntro();
    }

    void Update()
    {
        if (startNextLevel)
        {
            if (nextLevelTimer <= 0)
            {
                currentLevel++;
                if (currentLevel <= levels.Length)
                {
                    string sceneName = levels[currentLevel - 1];
                    SceneManager.LoadSceneAsync(sceneName);
                    ShowLevelIntro();  // Új szint intro megjelenítése
                }
                else
                {
                    Debug.Log("GAME OVER!");
                }
                nextLevelTimer = 3;
                startNextLevel = false;
            }
            else
            {
                nextLevelTimer -= Time.deltaTime;
            }
        }
    }

    private void ShowLevelIntro()
    {
        if (levelIntroPanel != null && levelIntroText != null && levelIntroDescription != null)
        {
            // Aktuális szint adatainak keresése
            LevelData currentLevelData = levelTextData.levels.Find(x => x.levelNumber == currentLevel);
            
            if (currentLevelData != null)
            {
                // Szöveg beállítása
                levelIntroText.text = currentLevelData.title;
                levelIntroDescription.text = currentLevelData.description;
                
                // Panel megjelenítése
                levelIntroPanel.alpha = 1;
                levelIntroPanel.interactable = true;
                levelIntroPanel.blocksRaycasts = true;

                // Coroutine indítása a panel elrejtéséhez
                StartCoroutine(HideLevelIntroAfterDelay());
            }
        }
    }

    private IEnumerator HideLevelIntroAfterDelay()
    {
        yield return new WaitForSeconds(introDisplayTime);

        // Fokozatos elhalványítás
        float elapsedTime = 0;
        float fadeTime = 1f;

        while (elapsedTime < fadeTime)
        {
            elapsedTime += Time.deltaTime;
            levelIntroPanel.alpha = Mathf.Lerp(1, 0, elapsedTime / fadeTime);
            yield return null;
        }

        levelIntroPanel.interactable = false;
        levelIntroPanel.blocksRaycasts = false;
    }

    public void AddScore(int amountToAdd)
    {
        score += amountToAdd;
        scoreText.text = score.ToString();
    }

    public void DecreaseHealth(int amountToDecrease)
    {
        health -= amountToDecrease;
        healthText.text = health.ToString();
    }

    public void IncreaseHealth(int amountToIncrease)
    {
        if (health < 5) // Maximum 5 élet lehet
        {
            health += amountToIncrease;
            healthText.text = health.ToString();
        }
    }

    public void AddDestructable()
    {
        numDestructables++;
    }

    public void RemoveDestructable()
    {
        numDestructables--;

        if (numDestructables == 0)
        {
            startNextLevel = true;
        }
    }
}