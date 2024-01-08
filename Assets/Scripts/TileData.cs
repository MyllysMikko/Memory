using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{

    public List<Tile> tiles = new List<Tile>();

    public void Flip(int tileIndex, bool flipped)
    {
        tiles[tileIndex].flipped = flipped;
    }

    public void ResetTiles()
    {
        foreach (Tile tile in tiles)
        {
            tile.flipped = false;
        }
    }
}

[Serializable]
public class Tile
{
    [SerializeField]public Color Color { get; private set; }
    public bool flipped;

    public Tile(Color color)
    {
        Color = color;
        flipped = false;
    }

    public void SetColor(Color color)
    {
        Color = color;
    }
}
