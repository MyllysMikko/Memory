using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] TileView[] tiles;
    [SerializeField] TileFactory tileFactory;

    [Header("Tiles")]
    //[SerializeField] Transform startPos;
    [SerializeField] float spacing = 0.1f;
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
        tiles = tileFactory.GetTiles(numberOfPairs);

        foreach (var tile in tiles)
        {
            tile.tileClicked += OnTileClicked;
        }

        PositionTiles();

    }

    void PositionTiles()
    {
        int tilesPerRow = numberOfPairs / 2;

        Vector3 startPos = Camera.main.transform.position;
        startPos.y -= 5;

        //startPos.z += tilesPerRow * 0.5f * spacing;
        //startPos.x -= tilesPerRow * 0.5f * spacing;


        for (int i = 0; i < tiles.Length; i++)
        {
            float rowf = Mathf.Floor(i / tilesPerRow);

            int row = Convert.ToInt32(rowf);
            int column = i % tilesPerRow;

            Vector3 position = new Vector3(column * spacing, 0f, row * spacing);

            tiles[i].transform.position = startPos + position;
        }
    }
}
