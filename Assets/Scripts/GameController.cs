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

    int gridX;
    int gridY;

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
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                TilePrefab tilePrefab = hit.collider.GetComponent<TilePrefab>();

                if (!tileData.tiles[tilePrefab.TileIndex].flipped)
                {
                    OnTileClicked(tilePrefab.TileIndex);
                }



                //tileView?.OnClick();

            }
        }
    }


    void OnTileClicked(int tileIndex)
    {
        // No additional flips are accepted until the last two are flipped back (If they didn't match)
        if (numberFlipped <= 1)
        {
            //tiles[tileIndex].Flip();
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
        this.gridX = gridX;
        this.gridY = gridY;
        GetTiles();

        tileView.SetTiles(tileData.tiles, gridX, gridY);
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


            //tile2.Flip();
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
           tileData.tiles = tileFactory.GetTiles((int)(numberOfTiles * 0.5f)).ToList();

            //
            //foreach (var tile in tiles)
            //{
            //    tile.tileClicked += OnTileClicked;
            //}
            //
            //PositionTiles();
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


    //To increase performance, we could (and probably should!) pool tiles and then reuse them
    //instead of deleting them and creating new ones each time.
    public void DestroyTiles()
    {
        tileData.tiles.Clear();
        tileView.DestroyTiles();
        //foreach (var tile in tiles)
        //{
        //    tile.tileClicked -= OnTileClicked;
        //    Destroy(tile.gameObject);
        //}
    }
}
