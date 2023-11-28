using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory
{


    public Tile[] GetTiles(int numberOfPairs)
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

        return tiles;
    }

}
