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
    public ParticleSystem PoofPS;
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


    private void TileSelected()
    {
        Magazine.Instance.TilesInMagazine.Add(gameObject.GetComponent<Tile>());
        EventManager.Instance.onClickOnTile?.Invoke(this, EventArgs.Empty);
        EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);
        EventManager.Instance.CheckLostEvent?.Invoke(this, EventArgs.Empty);
        Interactable = false;
        if (check)
        {
        check.ToggleSpine();
        }
    }

    public IEnumerator DestoryTile()
    {
        if (tile.Type == 99)
        {
            gameObject.SetActive(false);
        }
        VFX.GetComponent<MeshRenderer>().enabled = false;
        skeleton.timeScale = 0.85f;
        Magazine.Instance.TilesInMagazine.Remove(this.tile);
        BoardManager.Instance.TilesInBoard.Remove(this.tile);
        yield return new WaitForSeconds(0.25f);
        PoofPS.Play();
        skeleton.AnimationState.End += AnimationState_End;
        yield return new WaitForSeconds(0.4f);
        gameObject.SetActive(false);
        EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);

    }

    private void AnimationState_End(Spine.TrackEntry trackEntry)
    {
        throw new NotImplementedException();
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
        else if (tile.Joker)
        {
            TileSelected();
        }
        if (Comboable)
        {
            Magazine.Instance.DestoryTiles(tile.Type);
        }
      //  EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);

    }
}

