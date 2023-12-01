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

    [SerializeField] Button button;

    [SerializeField] TextMeshProUGUI textMeshProUGUI;

    SaveManager saveManager;

    int levelsCompleted = 0;

    // Start is called before the first frame update
    void Start()
    {
        //mainMenu.SetActive(true);
        //levelSelect.SetActive(true);

        saveManager = new SaveManager();
        levelsCompleted = saveManager.LoadData();
        Debug.Log(levelsCompleted);
        gameController.levelCompleted += OnLevelComplete;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            button.interactable = !button.interactable;
        }

        textMeshProUGUI.text = levelsCompleted.ToString();
    }


    public void OnLevelSelect(int level)
    {
        //There should probably be a better way to store how many tiles each level has.
        //but for this assignment I think this is good enough;
        switch(level)
        {
            case 1:
                gameController.StartLevel(1, 2, 2);
                break;

            case 2:
                gameController.StartLevel(2, 4, 4);
                break;
        }
    }

    public void OnLevelComplete(int level)
    {
        levelsCompleted = level;
        saveManager.SaveData(levelsCompleted);
        Debug.Log(levelsCompleted);
        Debug.Log(saveManager.LoadData());
    }


}
