using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TileView : MonoBehaviour
{
    [SerializeField] GameObject tilePrefab;
    List<TilePrefab> tiles = new List<TilePrefab>();

    [Header("Tiles")]
    [SerializeField] float spacing = 2;


    public void SetTiles(List<Tile> tileList, int numberOfTiles, int gridX, int gridY)
    {
        for (int i = 0; i < numberOfTiles; i++)
        {
            if (tiles.Count >= i + 1)
            {
                tiles[i].SetTile(tileList[i].Color, i);
                tiles[i].gameObject.SetActive(true);
            }
            else
            {
                GameObject spawnedTile = Instantiate(tilePrefab);
                TilePrefab tilePref = spawnedTile.GetComponent<TilePrefab>();
                tilePref.SetTile(tileList[i].Color, i);

                tiles.Add(tilePref);
            }
        }
        PositionTiles(numberOfTiles, gridX, gridY);
    }

    public void Flip(int tileIndex, bool flipped)
    {
        tiles[tileIndex].animator.SetBool("flipped", flipped);
    }

    public void ResetTiles()
    {
        foreach(TilePrefab tile in tiles)
        {
            tile.gameObject.SetActive(false);
        }
    }

    void PositionTiles(int numberOfTiles, int gridX, int gridY)
    {

        Vector3 startPos = Camera.main.transform.position;
        startPos.y -= spacing * Math.Max(gridX, gridY);

        startPos.x -= spacing * gridX * 0.5f;
        startPos.x += spacing * 0.5f;

        startPos.z -= spacing * gridY * 0.5f;
        startPos.z += spacing * 0.5f;



        for (int i = 0; i < numberOfTiles; i++)
        {
            float rowf = Mathf.Floor(i / gridX);

            int row = Convert.ToInt32(rowf);
            int column = i % gridX;

            Vector3 position = new Vector3(column * spacing, 0f, row * spacing);

            tiles[i].transform.position = startPos + position;
        }
    }
}


