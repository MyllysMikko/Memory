using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            MouseClick();
        }
    }
    /// <summary>
    /// Clicking on a tile calls a function from it.
    /// </summary>
    void MouseClick()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Tile"))
            {
                TileView tileView = hit.collider.GetComponent<TileView>();

                tileView?.OnClick();

            }
        }
    }
}
