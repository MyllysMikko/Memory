using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI winText;
    [SerializeField] TextMeshProUGUI titleText;

    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject options;

    //This can be toggled on as a reward for completing all levels.
    [SerializeField] GameObject backGround;

    [SerializeField] GameController gameController;

    [SerializeField] Button[] buttons;

    Stack<MenuState> stateStack = new Stack<MenuState>();

    SaveManager saveManager;

    int levelsCompleted = 0;

    // Start is called before the first frame update
    void Start()
    {
        backGround.SetActive(false);
        winText.enabled = false;
        //Set mainmenu as the starting menu
        stateStack.Push(MenuState.MainMenu);
        SwitchMenu();

        //Get "save data" (How many levels have been completed)
        saveManager = new SaveManager();
        levelsCompleted = saveManager.LoadData();
        Debug.Log(levelsCompleted);
        gameController.levelCompleted += OnLevelComplete;

        UpdateLevelSelect();
    }





    public void OnLevelComplete(int level)
    {
        winText.enabled = true;

        if (level > levelsCompleted)
        {
            levelsCompleted = level;
            saveManager.SaveData(levelsCompleted);
            UpdateLevelSelect();
        }
    }

    //Updates which levels can be selected.
    //Since "levelsCompleted" starts counting at 1, and arrays start at 0. This convieniently let's us enable one more level than what has been complete.
    //Example: if levelsCompleted is 1. Levels 0-1 are enabled.
    void UpdateLevelSelect()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i <= levelsCompleted; 
        }

        if (levelsCompleted >= buttons.Length)
        {
            titleText.color = new Color(1, 0.84f, 0); //Gold color
        }
    }

    public void OnLevelSelect(int level)
    {
        stateStack.Push(MenuState.Level);
        SwitchMenu();

        //There should probably be a better way to store how many tiles each level has.
        //but for this assignment I think this is good enough;
        switch (level)
        {
            case 1:
                gameController.StartLevel(1, 2, 2);
                break;

            case 2:
                gameController.StartLevel(2, 4, 4);
                break;
            case 3:

                gameController.StartLevel(3, 6, 6);
                break;
        }
    }

    public void OnPlayPressed()
    {
        stateStack.Push(MenuState.LevelSelect);
        SwitchMenu();
    }

    public void OnOptionsPressed()
    {
        stateStack.Push(MenuState.Options);
        SwitchMenu();
    }

    public void OnLevelSelected()
    {
        stateStack.Push(MenuState.Level);
        SwitchMenu();
    }

    public void OnBackPressed()
    {
        if (stateStack.Peek() == MenuState.Level)
        {
            winText.enabled = false;
            gameController.DestroyTiles();
        }

        stateStack.Pop();
        SwitchMenu();
    }

    public void OnTitlePressed()
    {
        if (levelsCompleted >= buttons.Length)
        {
            backGround.SetActive(!backGround.activeInHierarchy);
        }
    }

    /// <summary>
    /// I'm using a stack to keep track of what menu we should be displaying.
    /// </summary>
    void SwitchMenu()
    {
        MenuState state;

        if(stateStack.TryPeek(out state))
        {
            mainMenu.SetActive(state == MenuState.MainMenu);
            levelSelect.SetActive(state == MenuState.LevelSelect);
            options.SetActive(state == MenuState.Options);
        }
        else
        {
            Application.Quit();
        }

    }

    public enum MenuState
    {
        MainMenu,
        LevelSelect,
        Options,
        Level
    }

}
