using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TileInput : MonoBehaviour
{
    public bool Interactable = true;
    Tile tile;
    public bool Comboable = false;
    private SkeletonAnimation skeleton;
    public SkeletonAnimation VFX;
    private LayerChecker check;

    private void Start()
    {
        check = GetComponent<LayerChecker>();
        tile = GetComponent<Tile>();
        skeleton = GetComponent<SkeletonAnimation>();
        // VFX = gameObject.transform.GetChild(0).GetComponent<SkeletonAnimation>();
    }
    private void Update()
    {
        if (Comboable)
        {
            tile.input.VFX.gameObject.SetActive(true);
        }
        else
        {
            tile.input.VFX.gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        PressedOnTile();
    }


    private void TileSelected()
    {
        Magazine.Instance.TilesInMagazine.Add(gameObject.GetComponent<Tile>());
        EventManager.Instance.onClickOnTile?.Invoke(this, EventArgs.Empty);
        EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);
        EventManager.Instance.CheckLostEvent?.Invoke(this, EventArgs.Empty);
        Interactable = false;
    }

    public IEnumerator DestoryTile()
    {
        skeleton.timeScale = 1;
        Magazine.Instance.TilesInMagazine.Remove(this.tile);
        BoardManager.Instance.TilesInBoard.Remove(this.tile);
        yield return new WaitForSeconds(0.1f);
        gameObject.SetActive(false);
        EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);
    }

    public void PressedOnTile()
    {
        if (Interactable && !Magazine.Instance.MagazineIsFull)
        {
            Destroy(check);
            Interactable = false;
            if (gameObject.GetComponent<LayerChecker>())
            {
                gameObject.GetComponent<LayerChecker>().enabled = false;
            }
            TileSelected();
            print("ClickedOnTile");
        }

        if (Comboable)
        {
            Magazine.Instance.DestoryTiles(tile.Type);
        }
    }
}
