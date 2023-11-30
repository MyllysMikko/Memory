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
    [SerializeField] float distanceFromCamera = 10f;
    [SerializeField] int gridX;
    [SerializeField] int gridY;
    [SerializeField] float spacing = 0.1f;
    //[SerializeField] int numberOfPairs;
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
        // No additional flips are accepted until the last two are flipped back (If they didn't match)
        if (numberFlipped <= 1)
        {
            Debug.Log($"{tiles[tileIndex].tile.color}, {tiles[tileIndex].tile.flipped}");

            //tiles[tileIndex].tile.flipped = true;
            tiles[tileIndex].Flip();
            flipped[numberFlipped] = tileIndex;
            numberFlipped++;

            if (numberFlipped > 1)
            {
                StartCoroutine(CheckSelected());
            }
        }

    }

    IEnumerator CheckSelected()
    {
        TileView tile1 = tiles[flipped[0]];
        TileView tile2 = tiles[flipped[1]];

        if (tile1.tile.color == tile2.tile.color)
        {
            Debug.Log("Match!");
            numberFlipped = 0;
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

        int numberOfTiles = gridX * gridY;

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

    void PositionTiles()
    {

        Vector3 startPos = Camera.main.transform.position;


        startPos.y -= spacing * gridX;

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
}
