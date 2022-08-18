using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TileInput : MonoBehaviour
{
    public bool Interactable = true;
    Tile tile;
    Magazine _mag;
    public bool Comboable = false;
    private SkeletonAnimation skeleton;
    public SkeletonAnimation VFX;
    private LayerChecker check;
    public ParticleSystem PoofPS;
    private void Start()
    {
        _mag = Magazine.Instance;
        check = GetComponent<LayerChecker>();
        tile = GetComponent<Tile>();
        skeleton = GetComponent<SkeletonAnimation>();
        // VFX = gameObject.transform.GetChild(0).GetComponent<SkeletonAnimation>();
    }
    private void Update()
    {
        if (Comboable)
        {
           VFX.gameObject.SetActive(true);
        }
        else
        {
           VFX.gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        PressedOnTile();
    }
    private void OnMouseEnter()
    {
        
    }

    private void TileSelected()
    {
        if (check)
        {
            check.ToggleSpine();
        }
        Magazine.Instance.TilesInMagazine.Add(gameObject.GetComponent<Tile>());
        EventManager.Instance.onClickOnTile?.Invoke(this, EventArgs.Empty);
        EventManager.Instance.MagazineSorterEve?.Invoke(this, EventArgs.Empty);
        Interactable = false;
        EventManager.Instance.CheckLostEvent?.Invoke(this, EventArgs.Empty);
    }

    public IEnumerator DestoryTile()
    {
        if (tile.Type == 99)
        {
            gameObject.SetActive(false);
        }
        VFX.GetComponent<MeshRenderer>().enabled = false;
        skeleton.timeScale = 1f;
        Magazine.Instance.TilesInMagazine.Remove(this.tile);
        BoardManager.Instance.TilesInBoard.Remove(this.tile);
        yield return new WaitForSeconds(0.25f);
        PoofPS.Play();
        EventManager.Instance.CorutineStopper?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(0.33f);
        EventManager.Instance.CorutineStarter?.Invoke(this, EventArgs.Empty);
        gameObject.SetActive(false);
    }

    public void PressedOnTile()
    {
        if (Interactable && !Magazine.Instance.MagazineIsFull && !Comboable)
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
        else if (tile.Joker && !Comboable)
        {
            TileSelected();
        }
        if (Comboable)
        {
            Magazine.Instance.DestoryTiles(tile.Type);
            print("Combo");
        }
    }
}

