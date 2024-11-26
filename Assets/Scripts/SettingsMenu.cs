using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeslider;
    [SerializeField] private TMP_Text volumetext;

    private void Awake()
    {
        volumeslider.value = PlayerPrefs.GetInt("volume",100);
        volumetext.text = ((int)volumeslider.value).ToString();
        Screen.fullScreen = PlayerPrefs.GetInt("fullscreen",1) == 1;
        fullscreenToggle.isOn = Screen.fullScreen;
        
    }

    public void RefreshFullscreen()
    {
        Screen.fullScreen = fullscreenToggle.isOn;
        PlayerPrefs.SetInt("fullscreen",fullscreenToggle.isOn ? 1 : 0);
    }
    public void RefreshVolume()
    {
        PlayerPrefs.SetInt("volume",(int)volumeslider.value);
        volumetext.text = ((int)volumeslider.value).ToString();
    }
    
    
}
