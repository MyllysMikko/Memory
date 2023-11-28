using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] Tile[] tiles;
    TileFactory tileFactory;
    void Start()
    {
        tileFactory = new TileFactory();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MouseClick();
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GetTiles();
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (tiles != null)
            {
                foreach (var tile in tiles)
                {
                    tile.Print();
                }
            }
        }
    }

    void MouseClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("hit");
        }
    }

    void GetTiles()
    {
        tiles = tileFactory.GetTiles(2);
    }
}
