using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            PlayerPrefs.DeleteAll();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerPrefs.SetInt("resolutionIndex", 100);
            PlayerPrefs.Save();
        }

        if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log(PlayerPrefs.GetInt("resolutionIndex", -1));
        }
    }
}
