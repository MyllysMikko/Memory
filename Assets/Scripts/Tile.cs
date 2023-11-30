using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Tile
{
    public Color color { get; private set; }
    public bool flipped;

    public Tile(Color color)
    {
        this.color = color;
        flipped = false;
    }

    public void Print()
    {
        Debug.Log(color.ToString());
        Debug.Log($"Flipped: {flipped}");
    }
}
