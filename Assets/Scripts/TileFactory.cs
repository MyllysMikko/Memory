using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    /// <summary>
    /// This function creates multiple tilePrefabs and returns an array of their TileView classes
    /// tilePrefabs contain TileView class which handles a tile's animations (While also holding the Tile class for easy access)
    /// This could be done in one loop, but I feel like it's a bit easier to read when you first create tile data (Tile class) and then create the tiles themselves (TileView class)
    /// 
    /// Why we're returning an array of TileView classes instead of the instantiated prefabs is because this cuts down on GetComponent calls later on. 
    /// </summary>
    /// <param name="numberOfPairs">How many pairs of tiles are to be made</param>
    /// <returns></returns>
    public TileView[] GetTiles(int numberOfPairs)
    {
        int numberOfTiles = numberOfPairs * 2;
        Tile[] tiles = new Tile[numberOfTiles];

        int tileIndex = 0;

        for (int i = 0; i < numberOfPairs; i++)
        {
            //Here I am casting a number to TileColor enum.
            //0 = Black
            //1 = Red
            //This could be a problem if you were to ask for more pairs than there were available colors.
            tiles[tileIndex] = new Tile((Tile.TileColor)i);
            tiles[tileIndex+1] = new Tile((Tile.TileColor)i);
            tileIndex += 2;
        }

        TileView[] tileViews = new TileView[numberOfTiles];

        for (int i = 0; i < tiles.Length; i++)
        {
            //TODO CALCULATE POSITION
            Vector3 pos = Vector3.zero;

            GameObject spawnedTile = Instantiate(tilePrefab, pos, Quaternion.identity);

            TileView tileView = spawnedTile.GetComponent<TileView>();
            tileView.SetTile(tiles[i], i);


            tileViews[i] = tileView;
        }

        return tileViews;
    }

}
