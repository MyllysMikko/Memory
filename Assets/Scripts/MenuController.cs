using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject levelSelect;
    [SerializeField] GameObject options;

    [SerializeField] GameController gameController;

    [SerializeField] Button[] buttons;

    Stack<MenuState> stateStack = new Stack<MenuState>();

    SaveManager saveManager;

    int levelsCompleted = 0;

    // Start is called before the first frame update
    void Start()
    {
        stateStack.Push(MenuState.MainMenu);
        SwitchMenu();

        saveManager = new SaveManager();
        levelsCompleted = saveManager.LoadData();
        Debug.Log(levelsCompleted);
        gameController.levelCompleted += OnLevelComplete;

        UpdateLevelSelect();
    }

    // Update is called once per frame
    void Update()
    {

    }




    public void OnLevelComplete(int level)
    {
        levelsCompleted = level;
        saveManager.SaveData(levelsCompleted);
        UpdateLevelSelect();
        Debug.Log(levelsCompleted);
        Debug.Log(saveManager.LoadData());
    }

    void UpdateLevelSelect()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = i <= levelsCompleted; 
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
            gameController.DestroyTiles();
        }

        stateStack.Pop();
        SwitchMenu();
    }

    void SwitchMenu()
    {
        MenuState state;

        if(stateStack.TryPeek(out state))
        {
            mainMenu.SetActive(state == MenuState.MainMenu);
            levelSelect.SetActive(state == MenuState.LevelSelect);
            //options.SetActive(state == MenuState.Options);
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
