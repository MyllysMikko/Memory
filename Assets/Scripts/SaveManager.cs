using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager
{

    string saveFileName = "save.txt";

    //Since save data currently consists of only a single integer (How many levels have been completed). We could probably just use playePrefs to store this data.
    //But I'd rather use playerPrefs to save options (Player preferences like the name implies) and have save data be separate from that.
    public int LoadData()
    {
        int levelsCompleted = 0;

        string filePath = Path.Combine(Application.dataPath, saveFileName);

        if (File.Exists(filePath))
        {
            string text = File.ReadAllText(filePath);

            //Incase something has gone wrong the save file (Such as the user modifying it) we return a default value of 0.
            if(!int.TryParse(text, out levelsCompleted))
            {
                levelsCompleted = 0;
            }
        }

        return levelsCompleted;
    }

    public void SaveData(int levelsCompleted)
    {
        string filePath = Path.Combine(Application.dataPath, saveFileName);
        
        File.WriteAllText(filePath, levelsCompleted.ToString());
    }
}
