using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{

    public List<Tile> tiles = new List<Tile>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[Serializable]
public class Tile
{
    [SerializeField]public Color Color { get; private set; }
    public bool flipped;

    public Tile(Color color)
    {
        this.Color = color;
        flipped = false;
    }
}
