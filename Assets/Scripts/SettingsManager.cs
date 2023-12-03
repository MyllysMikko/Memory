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

    Vector2[] resolutions =
    {
        new Vector2(1920, 1090),
        new Vector2(1600, 900),
        new Vector2(1280, 720)
    };

    int resolutionIndex;

    //
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log($"Resolution index at start {resolutionIndex}");
        LoadPrefs();
        Debug.Log($"Resolution index after load {resolutionIndex}");
        fullscreenToggle.isOn = fullscreen;

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
        Debug.Log("Change resolution");
        //resolutionIndex = Mathf.Clamp(resolutionIndex, 0, Screen.resolutions.Length);

        Vector2 resolution = resolutions[resolutionIndex];
        Screen.SetResolution((int)resolution.x, (int)resolution.y, fullscreen);
        SavePrefs();

    }

    public void OnResolutionChange(int index)
    {
        resolutionIndex = index;
        SetResolution();
    }

    public void OnFullscreenChange(bool change)
    {
        if (change != fullscreen)
        {
            fullscreen = change;
            SetResolution();
        }
    }

    void LoadPrefs()
    {
        resolutionIndex = PlayerPrefs.GetInt("resolutionIndex", 0);
        fullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1;
    }

    void SavePrefs()
    {
        Debug.Log($"ResolutionIndex before save{resolutionIndex}");
        PlayerPrefs.SetInt("fullscreen", Convert.ToInt32(fullscreen));
        PlayerPrefs.SetInt("resolutionIndex", resolutionIndex);

        PlayerPrefs.Save();

        Debug.Log($"ResoltuionIndex after save {PlayerPrefs.GetInt("resolutionIndex", -100)}");
    }
}
