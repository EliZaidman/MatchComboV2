using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class TileInput : MonoBehaviour
{
    EventManager Emanager;
    public bool Interactable = true;
    Tile tile;
    Magazine _mag;
    public bool Comboable = false;
    private SkeletonAnimation skeleton;
    public SkeletonAnimation VFX;
    private LayerChecker LayerCheck;
    public ParticleSystem PoofPS;
    bool HammemrIsActive = false;
    private void Start()
    {
        //Caching alot of shit
        _mag = Magazine.Instance;
        Emanager = EventManager.Instance;
        LayerCheck = GetComponent<LayerChecker>();
        tile = GetComponent<Tile>();
        skeleton = GetComponent<SkeletonAnimation>();
        // VFX = gameObject.transform.GetChild(0).GetComponent<SkeletonAnimation>();
        Emanager.AllowInputEvent += ToggleInput;
        Emanager.BarHammerFromTile += ToggleHammer;
    }

    bool AllowInput = true;
    private void ToggleInput(object sender, EventArgs e)
    {
        AllowInput = !AllowInput;
    }
    private void ToggleHammer(object sender, EventArgs e)
    {
        HammemrIsActive = !HammemrIsActive;
    }
    private void Update()
    {
        //Active And Deactivate VFX
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
        skeleton.GetComponent<MeshRenderer>().sortingOrder = 55;
        //If You didnt win you can press on the tiles.
        if (!PlayState.Instance.CheckedIfWon && AllowInput)
        {
            PressedOnTile();
        }

    }

    public void TileSelected()
    {
        //Checking if there is LayerCheck its the same as ? and swapping to Spine instad
        if (LayerCheck && !tile.Joker)
        {
            LayerCheck.ToggleSpine();
        }
        //Making the tile Uninterectable, adds it to the UNSORTED list and then sorting it while checking if you lost(noob)
        //disabling input here as well, if i want for example to fake press it from other place and i dont use the PressedOnTile()
        Interactable = false;
        _mag.TilesInMagazine.Add(gameObject.GetComponent<Tile>());
        Emanager.MagazineSorterEve?.Invoke(this, EventArgs.Empty);
        if (tile.Joker)
        {
            _mag.SortedMagazine.Add(gameObject.GetComponent<Tile>());
            StartCoroutine(DelayJokerSort());
        }

        Emanager.CheckLostEvent?.Invoke(this, EventArgs.Empty);
    }

    public IEnumerator DestoryTile()
    {
        //If its joker dont destory it
        if (tile.Type == 99)
        {
            gameObject.SetActive(false);
        }
        //Turrning of VFX
        VFX.GetComponent<MeshRenderer>().enabled = false;
        //Activating the BIG BRAIN Animation STRAT
        skeleton.timeScale = 1f;
        _mag.TilesInMagazine.Remove(this.tile);
        yield return new WaitForSeconds(0.25f);
        //Giving time to VFX to play (1/4 second)
        PoofPS.Play();
        //Stops the Sorter from moving the tiles to the right place beacuse VFX is playing.
        Emanager.CorutineStopper?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(0.33f);
        //Starts the Sorter from moving the tiles to the right place beacuse VFX is stopped playing.
        Emanager.CorutineStarter?.Invoke(this, EventArgs.Empty);
        //Deactivating the GameObject(AKA Tile)
        gameObject.SetActive(false);
    }

    public IEnumerator WinDes()
    {
        skeleton.timeScale = 1.2f;
        PoofPS.Play();
        _mag.TilesInMagazine.Remove(this.tile);
        yield return new WaitForSeconds(0.33f);
        gameObject.SetActive(false);
    }
    public IEnumerator DelayJokerSort()
    {
        yield return new WaitForSeconds(0.11f);
        Emanager.MagazineSorterEve?.Invoke(this, EventArgs.Empty);

    }
    public void PressedOnTile()
    {

        if (Interactable && HammemrIsActive)
        {
            StartCoroutine(HammerAnim());

        }

        else if (Interactable && !_mag.MagazineIsFull && !Comboable && !HammemrIsActive)
        {
            BoardManager.Instance.timer = 0;
            //Removes the tile from the Board list
            //FailSafe for sorter not activating right
            BoardManager.Instance.TilesInBoard.Remove(this.tile);
            //Its Corutine beacuse i need delay in other placeses
            StartCoroutine(PlayAnim(0));

            //Force Removing The Layer Checker for Pain in the ass bugs
            Destroy(LayerCheck);
            Interactable = false;
            //FailSafe if it didnt remove the LayerChecker!
            if (GetComponent<LayerChecker>())
            {
                GetComponent<LayerChecker>().enabled = false;
            }
            TileSelected();
            print("ClickedOnTile");
        }




        if (Comboable)
        {
            //If its Comboable use the Text VFX and destory the tiles Sadge
            Emanager.VFXAllign += AllignVfxText;
            _mag.DestoryTiles(tile.Type);
            print("Combo");
        }
    }

    private IEnumerator PlayAnim(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        EventManager.Instance.CorutineStarter?.Invoke(this, EventArgs.Empty);
    }
    private IEnumerator StopAnim(float Timer)
    {
        yield return new WaitForSeconds(Timer);
        EventManager.Instance.CorutineStopper?.Invoke(this, EventArgs.Empty);
    }

    //This is what you use to make the VFX Appear in the right place
    private void AllignVfxText(object sender, EventArgs e)
    {
        VFXTest.Instance.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z);
    }

    IEnumerator HammerAnim()
    {
        tile.DiedOfHammer = true;
        BoardManager.Instance.timer = 0;
        BoardManager.Instance.TilesInBoard.Remove(this.tile);
        Emanager.BarHammerFromTile?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(0.2f);
        this.gameObject.SetActive(false);
    }
}

