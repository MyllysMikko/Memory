using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] TileView[] tiles;
    [SerializeField] TileFactory tileFactory;

    [Header("Tiles")]
    [SerializeField] int numberOfPairs;
    [SerializeField] int[] flipped;
    [SerializeField] int numberFlipped;
    void Start()
    {
        //tileFactory = gameObject.AddComponent<TileFactory>();
        numberFlipped = 0;
        flipped = new int[2];
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.G))
        {
            GetTiles();
        }
    }

    void OnTileClicked(int tileIndex)
    {
        Debug.Log($"{tiles[tileIndex].tile.color}, {tiles[tileIndex].tile.flipped}");

        tiles[tileIndex].tile.flipped = true;
        flipped[numberFlipped] = tileIndex;
        numberFlipped++;

        if (numberFlipped > 1)
        {
            CheckSelected();
            numberFlipped = 0;
        }
    }

    void CheckSelected()
    {
        TileView tile1 = tiles[flipped[0]];
        TileView tile2 = tiles[flipped[1]];

        if (tile1.tile.color == tile2.tile.color)
        {
            Debug.Log("Match!");
        }
        else
        {
            Debug.Log("Didn't match!");
            tile1.tile.flipped = false;
            tile2.tile.flipped = false;
        }
    }

    /// <summary>
    /// This function calls tileFactory to make an array of tiles.
    /// Each tile will be identified by it's index within this array.
    /// </summary>
    void GetTiles()
    {
        tiles = tileFactory.GetTiles(2);

        foreach (var tile in tiles)
        {
            tile.tileClicked += OnTileClicked;
        }

    }
}
