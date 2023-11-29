using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    [SerializeField] TileView[] tiles;
    [SerializeField] TileFactory tileFactory;
    void Start()
    {
        //tileFactory = gameObject.AddComponent<TileFactory>();
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
        TileView tileView = tiles[tileIndex].GetComponent<TileView>();
        Debug.Log($"{tileView.tile.color}, {tileView.tile.flipped}");
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
