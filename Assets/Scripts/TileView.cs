using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileView : MonoBehaviour
{
    //[SerializeField] Renderer backSideRenderer;

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

    /// <summary>
    /// Event is called when this tile is clicked.
    /// tileIndex is passed along this event, which GameController uses to identify this tile in the array and check it's information. (mainly to check it's color)
    /// !tile.flipped ensures that events are not called for tiles that are already flipped.
    /// (This check could be done in GameController since it can already access this tile using tileIndex, but I find it better to not send an event at tall if a tile can't be flipped)
    /// 
    /// </summary>
    public void OnClick()
    {
        if (!tile.flipped)
        {
            tileClicked?.Invoke(tileIndex);
        }

    }


    public void SetTile(Tile tile)
    {
        this.tile = tile;
        //TODO set model state to mirror tile state (bool flipped)
        //backSideRenderer.material.SetColor("_Color", Color.red);
    }

    public void SetIndex(int index)
    {
        tileIndex = index;
    }
}
