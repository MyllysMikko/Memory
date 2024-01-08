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
    /// Takes in a list, and depending on said list, will either add new tiles or modify existing tiles as needed.
    /// </summary>
    /// <param name="numberOfPairs">How many pairs of tiles are to be made</param>
    /// <returns></returns>
    public List<Tile> GetTiles(List<Tile> tileList ,int numberOfPairs)
    {
        int numberOfTiles = numberOfPairs * 2;

        int tileIndex = 0;

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

    /// <summary>
    /// Shuffles part of a list.
    /// Part of the list that is shuffled is from index 0 to numberOfTiles.
    /// </summary>
    /// <param name="tileList">List to be modified</param>
    /// <param name="numberOfTiles">Number of tiles that will be shuffled.</param>
    void Shuffle(List<Tile> tileList, int numberOfTiles)
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            int shuffle = Random.Range(0, numberOfTiles);
            (tileList[shuffle], tileList[i]) = (tileList[i], tileList[shuffle]);
        }
    }

}
