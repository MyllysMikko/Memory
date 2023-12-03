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

    int resolutionIndex;
    bool fullscreen;

    //Instead of automatically detecting what resolutions the user's monitor supports, I've decided to just define 3 resolutions that you can choose from.
    //Of course in a proper game this is most likely a horrible way to handle resolution settings.
    Vector2[] resolutions =
    {
        new Vector2(1920, 1090),
        new Vector2(1600, 900),
        new Vector2(1280, 720)
    };


    void Start()
    {
        //Load saved settings
        LoadPrefs();
        fullscreenToggle.isOn = fullscreen;

        //Set dropdown to display available resolutions
        dropDown.ClearOptions();
        List<string> options = new List<string>();
        foreach (Vector2 resolution in resolutions)
        {
            options.Add($"{resolution.x} x {resolution.y}");
        }
        dropDown.AddOptions(options);
        dropDown.value = resolutionIndex;
        dropDown.RefreshShownValue();

        SetResolution();
    }

    /// <summary>
    /// Sets resolution according to resolutionIndex. Also changes if the game is fullscreen.
    /// </summary>
    public void SetResolution()
    {
        Vector2 resolution = resolutions[resolutionIndex];
        Screen.SetResolution((int)resolution.x, (int)resolution.y, fullscreen);

    }

    /// <summary>
    /// Is called when user selects an option from the dropdown.
    /// </summary>
    /// <param name="index">Index of selected option</param>
    public void OnResolutionChange(int index)
    {
        resolutionIndex = index;
        SetResolution();
        SavePrefs();
    }

    //I've found that changing the value of "fullscreenToggle.isOn" will trigger "OnValueChanged" event even when it's done through script.
    //This was an issue, because Loading playerPrefs would trigger SetResolution before we had finished loading prefs causing all kinds of issues.
    //Now instead of just changing the value of fullscreenToggle.isOn, I have a separate bool "fullscreen".
    //fullscreenToggle.isOn is now used to show what the player has selected, and react to when player wants to change fullscreen.
    public void OnFullscreenChange(bool change)
    {
        if (change != fullscreen)
        {
            fullscreen = change;
            SetResolution();
            SavePrefs();
        }
    }

    void LoadPrefs()
    {
        resolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
        fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
    }

    void SavePrefs()
    {
        PlayerPrefs.SetInt("fullscreen", Convert.ToInt32(fullscreen));
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);
        PlayerPrefs.Save();
    }
}
