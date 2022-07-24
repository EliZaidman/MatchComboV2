using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileInput : MonoBehaviour
{
    private bool Interactable = true;
    Tile tile;
    public bool Comboable = false;


    private void Start()
    {
        tile = GetComponent<Tile>();
    }
    private void OnMouseDown()
    {
        if (Interactable && !Magazine.Instance.MagazineIsFull)
        {
            TileSelected();
            print("ClickedOnTile");
        }

        if (Comboable)
        {
            Magazine.Instance.DestoryCombo(tile.Type);
        }
    }

    private void TileSelected()
    {
        Interactable = false;
        Magazine.Instance.TilesInMagazine.Add(gameObject.GetComponent<Tile>());
        MoveTile movetile = gameObject.GetComponent<MoveTile>();
      //  movetile._movingTile = true;
        EventManager.Instance.onClickOnTile?.Invoke(this, EventArgs.Empty);
        EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);

    }
}
    