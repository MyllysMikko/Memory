using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{

    [SerializeField] TMP_Dropdown dropDown;
    [SerializeField] Toggle fullscreenToggle;

    bool fullscreen;

    Resolution[] resolutions;
    List<Resolution> filteredResolutions = new List<Resolution>();
    int resolutionIndex;

    // Start is called before the first frame update
    void Start()
    {
        fullscreenToggle.isOn = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        resolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
        dropDown.value = resolutionIndex;

        resolutions = Screen.resolutions;
        dropDown.ClearOptions();

        foreach (Resolution resolution in resolutions)
        {
            float aspectRatio = (float)resolution.width / resolution.height;
            if (Mathf.Approximately(aspectRatio, 16f/9f))
            {
                filteredResolutions.Add(resolution);
                Debug.Log("burh");
            }
        }

        List<string> options = new List<string>();
        foreach (Resolution resolution in filteredResolutions)
        {
            options.Add($"{resolution.width} x {resolution.height}");
        }

        dropDown.AddOptions(options);
        dropDown.RefreshShownValue();
        SetResolution();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            fullscreenToggle.isOn = PlayerPrefs.GetInt("fullscreen", 1) == 1;
        }
    }

    public void SetResolution()
    {
        resolutionIndex = Mathf.Clamp(resolutionIndex, 0, Screen.resolutions.Length);

        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, fullscreen);
        SavePrefs();

    }

    public void OnResolutionChange(int index)
    {
        resolutionIndex = index;
        SetResolution();
    }

    public void OnFullscreenChange()
    {

    }

    void SavePrefs()
    {
        PlayerPrefs.SetInt("fullscreen", Convert.ToInt32(fullscreenToggle.isOn));
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
    }
}
