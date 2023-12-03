using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] TileView[] tiles;
    [SerializeField] TileFactory tileFactory;

    [Header("Tiles")]
    [SerializeField] int gridX;
    [SerializeField] int gridY;
    [SerializeField] float spacing = 2;

    int currentLevel;
    int pairsMatched;
    int numberOfTiles;

    int[] flipped;
    int numberFlipped;

    public TileView.GameEvent levelCompleted;
    void Start()
    {
        numberFlipped = 0;
        flipped = new int[2];
    }


    void OnTileClicked(int tileIndex)
    {
        // No additional flips are accepted until the last two are flipped back (If they didn't match)
        if (numberFlipped <= 1)
        {
            tiles[tileIndex].Flip();
            flipped[numberFlipped] = tileIndex;
            numberFlipped++;

            if (numberFlipped > 1)
            {
                StartCoroutine(CheckSelected());
            }
        }

    }

    /// <summary>
    /// Prepares current level by resetting any counters used to keep track of tiles and spawning new ones
    /// </summary>
    /// <param name="currentLevel">Level number, this is returned when a level is completed.</param>
    /// <param name="gridX">How wide a grid of tiles is</param>
    /// <param name="gridY">How high a grid of tiles is</param>
    public void StartLevel(int currentLevel, int gridX, int gridY)
    {
        numberFlipped = 0;
        pairsMatched = 0;
        this.currentLevel = currentLevel;
        this.gridX = gridX;
        this.gridY = gridY;
        GetTiles();
    }

    /// <summary>
    /// Check whether flipped tiles match.
    /// If they don't match, we wait a bit before flipping them back.
    /// I'm using a courotine since it's easy to make something wait before continuing.
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckSelected()
    {
        TileView tile1 = tiles[flipped[0]];
        TileView tile2 = tiles[flipped[1]];

        if (tile1.tile.color == tile2.tile.color)
        {
            Debug.Log("Match!");
            pairsMatched++;
            numberFlipped = 0;

            //If all tiles have been flipped, we send an event indicating that this level has been completed.
            if (pairsMatched == numberOfTiles * 0.5f)
            {
                Debug.Log("Win!");
                levelCompleted.Invoke(currentLevel);
            }
        }
        else
        {
            Debug.Log("Didn't match!");
            yield return new WaitForSeconds(0.5f);
            tile1.Flip();
            tile2.Flip();
            numberFlipped = 0;
        }
    }



    /// <summary>
    /// This function calls tileFactory to make an array of tiles.
    /// Each tile will be identified by it's index within this array.
    /// </summary>
    void GetTiles()
    {
        numberOfTiles = gridX * gridY;

        if (numberOfTiles % 2 == 0)
        {
           tiles = tileFactory.GetTiles((int)(numberOfTiles * 0.5f));

            foreach (var tile in tiles)
            {
                tile.tileClicked += OnTileClicked;
            }

            PositionTiles();
        }
        else
        {
            Debug.LogError("Grid size results in uneven number of tiles");
        }
    }

    /// <summary>
    /// Tiles are positioned in a grid in a way that they all fit withing the camera's view
    /// </summary>
    void PositionTiles()
    {

        Vector3 startPos = Camera.main.transform.position;


        startPos.y -= spacing * Math.Max(gridX, gridY);

        startPos.x -= spacing * gridX * 0.5f;
        startPos.x += spacing * 0.5f;

        startPos.z -= spacing * gridY * 0.5f;
        startPos.z += spacing * 0.5f;



        for (int i = 0; i < tiles.Length; i++)
        {
            float rowf = Mathf.Floor(i / gridX);

            int row = Convert.ToInt32(rowf);
            int column = i % gridX;

            Vector3 position = new Vector3(column * spacing, 0f, row * spacing);

            tiles[i].transform.position = startPos + position;
        }
    }


    //To increase performance, we could (and probably should!) pool tiles and then reuse them
    //instead of deleting them and creating new ones each time.
    public void DestroyTiles()
    {
        foreach (var tile in tiles)
        {
            tile.tileClicked -= OnTileClicked;
            Destroy(tile.gameObject);
        }
    }
}
