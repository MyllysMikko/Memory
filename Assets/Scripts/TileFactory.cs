using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileFactory : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;

    Color[] colorArray =
    {
        Color.red,
        Color.blue,
        Color.green,
        Color.yellow,
        new Color(1, 0.5f, 0), //orange
        Color.cyan,
        new Color(0.5f, 0, 1), //violet
        Color.magenta,
        new Color(0.01f, 0.19f, 0.13f), //Dark green
        new Color(0.55f, 0, 0), //Dark red
        new Color(1, 0, 0.5f), //dark pink
        Color.gray,
        Color.black,
        Color.white,
        new Color(0, 0.98f, 0.6f), //Medium Spring Green
        Color.gray,
        new Color(0.94f, 0.9f, 0.55f), //Khaki
        new Color(1, 0.63f, 0.48f) //Salmon


    };

    /// <summary>
    /// This function creates multiple tilePrefabs and returns an array of their TileView classes
    /// tilePrefabs contain TileView class which handles a tile's animations (While also holding the Tile class for easy access)
    /// This could be done in one loop, but I feel like it's a bit easier to read when you first create tile data (Tile class) and then create the tiles themselves (TileView class)
    /// 
    /// The reason why we're returning an array of TileView classes instead of the instantiated prefabs is because this cuts down on GetComponent calls later on. 
    /// </summary>
    /// <param name="numberOfPairs">How many pairs of tiles are to be made</param>
    /// <returns></returns>
    public List<Tile> GetTiles(List<Tile> tileList ,int numberOfPairs)
    {
        int numberOfTiles = numberOfPairs * 2;
        Tile[] tiles = new Tile[numberOfTiles];

        int tileIndex = 0;
        int colorIndex = 0;

        for (int i = 0; i < numberOfPairs; i++)
        {

            //Could this be simplified?
            if (tileList.Count >= tileIndex + 1)
            {
                tileList[tileIndex].SetColor(colorArray[i]);
            }
            else
            {
                tileList.Add(new Tile(colorArray[i]));
            }

            if (tileList.Count >= tileIndex + 2)
            {
                tileList[tileIndex + 1].SetColor(colorArray[i]);
            }
            else
            {
                tileList.Add(new Tile(colorArray[i]));
            }


            //Here I am casting a number to TileColor enum.
            //0 = Black
            //1 = Red
            //This could be a problem if you were to ask for more pairs than there were available colors.
            //tiles[tileIndex] = new Tile(colorArray[i]);
            //tiles[tileIndex+1] = new Tile(colorArray[i]);
            tileIndex += 2;
        }


        //We randomize the order of tiles and finally go through the array one last time to set their propex indexes.
        Shuffle(tileList, numberOfTiles);

        return tileList;
    }

    void Shuffle(List<Tile> tileArray, int numberOfTiles)
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            int shuffle = Random.Range(0, numberOfTiles);
            (tileArray[shuffle], tileArray[i]) = (tileArray[i], tileArray[shuffle]);
        }
    }

}
