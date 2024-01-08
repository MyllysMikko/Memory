using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    TileData tileData;
    [SerializeField] TileFactory tileFactory;
    [SerializeField] TileView tileView;

    int currentLevel;
    int pairsMatched;
    int numberOfTiles;

    int[] flipped;
    int numberFlipped;

    public delegate void GameEvent(int index);

    public event GameEvent LevelCompleted;

    void Start()
    {
        tileData = gameObject.AddComponent<TileData>();
        numberFlipped = 0;
        flipped = new int[2];
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MouseClick();
        }
    }

    void MouseClick()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                TilePrefab tilePrefab = hit.collider.GetComponent<TilePrefab>();

                if (!tileData.tiles[tilePrefab.TileIndex].flipped)
                {
                    OnTileClicked(tilePrefab.TileIndex);
                }
            }
        }
    }


    void OnTileClicked(int tileIndex)
    {
        // No additional flips are accepted until the last two are flipped back (If they didn't match)
        if (numberFlipped <= 1)
        {
            Flip(tileIndex, true);
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
        numberOfTiles = gridX * gridY;
        GetTiles();

        tileView.SetTiles(tileData.tiles, numberOfTiles, gridX, gridY);
    }

    /// <summary>
    /// Check whether flipped tiles match.
    /// If they don't match, we wait a bit before flipping them back.
    /// I'm using a courotine since it's easy to make something wait before continuing.
    /// </summary>
    /// <returns></returns>
    IEnumerator CheckSelected()
    {
        Color tile1 = tileData.tiles[flipped[0]].Color;
        Color tile2 = tileData.tiles[flipped[1]].Color;

        if (tile1 == tile2)
        {
            Debug.Log("Match!");
            pairsMatched++;
            numberFlipped = 0;

            //If all tiles have been flipped, we send an event indicating that this level has been completed.
            if (pairsMatched == numberOfTiles * 0.5f)
            {
                Debug.Log("Win!");
                LevelCompleted?.Invoke(currentLevel);
            }
        }
        else
        {
            Debug.Log("Didn't match!");
            yield return new WaitForSeconds(0.5f);
            Flip(flipped[0], false);
            Flip(flipped[1], false);

            numberFlipped = 0;
        }
    }



    /// <summary>
    /// Calls TileFactory to create a list of tiles.
    /// Function is provided with an already existing list so that any already existing tiles can be reused.
    /// </summary>
    void GetTiles()
    {

        if (numberOfTiles % 2 == 0)
        {
           tileFactory.GetTiles(tileData.tiles,(int)(numberOfTiles * 0.5f)).ToList();
        }
        else
        {
            Debug.LogError("Grid size results in uneven number of tiles");
        }
    }


    void Flip(int tileIndex, bool flipped)
    {
        tileData.Flip(tileIndex, flipped);
        tileView.Flip(tileIndex, flipped);
    }


    public void ResetTiles()
    {
        tileData.ResetTiles();
        tileView.ResetTiles();
    }
}
