using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] Tile[] tiles;
    TileFactory tileFactory;
    void Start()
    {
        tileFactory = new TileFactory();
    }

    // Update is called once per frame
    void Update()
    {


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

    void OnTileClicked(int tileIndex)
    {
        Debug.Log(tileIndex);
    }

    /// <summary>
    /// This function calls tileFactory to make an array of tiles.
    /// Each tile will be identified by it's index within this array.
    /// </summary>
    void GetTiles()
    {
        tiles = tileFactory.GetTiles(2);
        GameObject spawnedTile = Instantiate(tilePrefab, transform.position, Quaternion.identity);
        TileView tileView = spawnedTile.GetComponent<TileView>();

        tileView.SetTile(tiles[0], 0);
        tileView.tileClicked += OnTileClicked;

    }
}
