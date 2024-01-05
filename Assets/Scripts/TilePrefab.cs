using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilePrefab : MonoBehaviour
{
    [SerializeField] Renderer backSideRenderer;
    public Animator animator;

    public int TileIndex {  get; private set; }


    public void Flip(bool flipped)
    {
        animator.SetBool("flipped", flipped);
    }


    public void SetTile(Color color, int tileIndex)
    {
        //TODO set model state to mirror tile state (bool flipped)
        backSideRenderer.material.SetColor("_Color", color);
        TileIndex = tileIndex;
        //backSideRenderer.sharedMaterial.SetColor("_Color", tile.Color);
    }

    public void SetIndex(int index)
    {
        TileIndex = index;
    }
}