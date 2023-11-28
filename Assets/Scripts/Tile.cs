using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    TileColor color;
    public bool flipped {  get; private set; }

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
