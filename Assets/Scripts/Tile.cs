using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that holds tile information
/// (Tile's color and if it has been flipped)
/// </summary>
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
}
