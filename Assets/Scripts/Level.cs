using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
    Text levelText;
    Text levelDescriptionText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
            healthText = GameObject.Find("HealthText").GetComponent<Text>();
            levelText = GameObject.Find("LevelText").GetComponent<Text>();
            levelDescriptionText = GameObject.Find("LevelDescriptionText").GetComponent<Text>();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(HideLevelTextAfterDelay(3f)); // 3 másodperc után eltűnik a szint szöveg
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

    private IEnumerator HideLevelTextAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        levelText.gameObject.SetActive(false);
        levelDescriptionText.gameObject.SetActive(false);
    }
}
