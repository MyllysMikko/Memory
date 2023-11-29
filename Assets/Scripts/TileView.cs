using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    int tileIndex = 0;
    public Tile tile;

    public delegate void GameEvent(int tileIndex);

    public event GameEvent tileClicked;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClick()
    {
        if (!tile.flipped)
        {
            tileClicked?.Invoke(tileIndex);
        }

    }


    public void SetTile(Tile tile, int tileIndex)
    {
        this.tile = tile;
        this.tileIndex = tileIndex;
        //TODO set model state to mirror tile state (bool flipped)
    }
}
