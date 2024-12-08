using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
using System.IO;
using log4net.Core;

[Serializable]
public class StoryElement
{
    public int id;
    public string text;
}

[Serializable]
public class StoryData
{
    public List<StoryElement> story;
}

public class StoryManager : MonoBehaviour
{
    public Text storyText;  // Referencia a UI Text komponensre
    public GameObject continueButton;  // Opcionális: gomb a továbblépéshez

    private StoryData storyData;
    private int currentStoryIndex = 0;

    void Start()
    {
        // JSON betöltése Resources mappából
        TextAsset jsonFile = Resources.Load<TextAsset>("Story");
        storyData = JsonUtility.FromJson<StoryData>(jsonFile.text);

        // Első mondat megjelenítése
        DisplayNextStoryPart();

        // Ha van külön gomb, akkor azt is beállítjuk
        if (continueButton != null)
        {
            continueButton.GetComponent<Button>().onClick.AddListener(DisplayNextStoryPart);
        }
    }

    void Update()
    {
        // Kattintásra is lehessen továbblépni, ha nincs külön gomb
        if (continueButton == null && Input.GetMouseButtonDown(0))
        {
            DisplayNextStoryPart();
        }
    }

    void DisplayNextStoryPart()
    {
        if (currentStoryIndex < storyData.story.Count)
        {
            storyText.text = storyData.story[currentStoryIndex].text;
            currentStoryIndex++;
        }
        else
        {
            // Ha elértük az utolsó mondatot, betöltjük a Level1 Scenet
            SceneManager.LoadScene("Level1");
        }
    }
}