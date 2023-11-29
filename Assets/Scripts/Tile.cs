using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    public TileColor color { get; private set; }
    public bool flipped;

    public Tile(TileColor color)
    {
        this.color = color;
        flipped = false;
    }

    public void Print()
    {
        Debug.Log(color.ToString());
        Debug.Log($"Flipped: {flipped}");
    }



    public enum TileColor
    {
        Red,
        Green,
        Blue,
        Purple,
        Pink,
        Cyan,
        Orange,
        Black
    }
}
