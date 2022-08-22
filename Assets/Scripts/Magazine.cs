using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public static Magazine Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    //public List<int> numTiles;
    public List<Tile> TilesInMagazine;
    public List<Tile> SortedMagazine;
    public List<Transform> MagazineSlots;
    public int mSize;
    public int _NumberOfones, _NumberOftwos, _NumberOfthrees, _NumberOffours, _NumberOffives, _NumberOfsix, _NumberOfSeven, _NumberOfEight, NumberOfNine, NumOfJokers;
    public bool MagazineIsFull = false;
    public GameObject UI;

    private void Start()
    {
        EventManager.Instance.MagazineSorterEve += SortMagazine;
        EventManager.Instance.DelayedSort += DelayedSortEvent;
        CheckSlots();

    }
    public void CheckSlots()
    {
        for (int i = 0; i < mSize; i++)
        {
            MagazineSlots[i].gameObject.SetActive(true);
        }
    }
    private void Update()
    {
        if (mSize == TilesInMagazine.Count)
            MagazineIsFull = true;
        else
            MagazineIsFull = false;


        if (NumOfJokers > 0) JokerIsActive = true;
        else JokerIsActive = false;
    }

    public void SortMagazine(object sender, EventArgs e)
    {
        TileCounter();
        SederInMachsanit();
    }

    public void SederInMachsanit()
    {
        SortedMagazine.Clear();
        for (int i = mSize; i >= 0; i--)
        {
            Sidur(_NumberOfones, 1, i);
            Sidur(_NumberOftwos, 2, i);
            Sidur(_NumberOfthrees, 3, i);
            Sidur(_NumberOffours, 4, i);
            Sidur(_NumberOffives, 5, i);
            Sidur(_NumberOfsix, 6, i);
            Sidur(_NumberOfSeven, 7, i);
            Sidur(_NumberOfEight, 8, i);
            Sidur(NumberOfNine, 9, i);
            Sidur(NumOfJokers, 99, i);
        }

        ComboMaker(_NumberOfones, 1);
        ComboMaker(_NumberOftwos, 2);
        ComboMaker(_NumberOfthrees, 3);
        ComboMaker(_NumberOffours, 4);
        ComboMaker(_NumberOffives, 5);
        ComboMaker(_NumberOfsix, 6);
        ComboMaker(_NumberOfSeven, 7);
        ComboMaker(_NumberOfEight, 8);
        ComboMaker(NumberOfNine, 9);


    }

    private void Sidur(int Seira, int Mispar, int Rezef)
    {
        if (Seira == Rezef)
        {
            foreach (var item in TilesInMagazine)
            {
                if (item.Type == Mispar)
                {
                    SortedMagazine.Add(item);
                }
            }
        }

    }
    private void TileCounter()
    {
        _NumberOfones = 0;
        _NumberOftwos = 0;
        _NumberOfthrees = 0;
        _NumberOffours = 0;
        _NumberOffives = 0;
        _NumberOfsix = 0;
        _NumberOfSeven = 0;
        _NumberOfEight = 0;
        NumberOfNine = 0;
        NumOfJokers = 0;
        foreach (var item in TilesInMagazine)
        {
            switch (item.Type)
            {
                case 1:
                    _NumberOfones++;
                    break;
                case 2:
                    _NumberOftwos++;
                    break;
                case 3:
                    _NumberOfthrees++;
                    break;
                case 4:
                    _NumberOffours++;
                    break;
                case 5:
                    _NumberOffives++;
                    break;
                case 6:
                    _NumberOfsix++;
                    break;
                case 7:
                    _NumberOfSeven++;
                    break;
                case 8:
                    _NumberOfEight++;
                    break;
                case 9:
                    NumberOfNine++;
                    break;
                case 99:
                    NumOfJokers++;
                    break;
                default:
                    Debug.Log("error in switch");
                    break;
            }
        }
    }

    public void ComboMaker(int numberOf, int imgNumber)
    {
        if (JokerIsActive && numberOf > 1)
        {
            foreach (var Combo in SortedMagazine)
            {

                if (Combo.Type == imgNumber)
                {

                    StartCoroutine(Wait(Combo));
                }
                if (Combo.Type == 99)
                {
                    if (NumOfJokers < 1)
                    {
                        NumOfJokers++;
                    }
                }
            }

        }
        else
        if (numberOf > 2)
        {
            foreach (var Combo in SortedMagazine)
            {

                if (Combo.Type == imgNumber)
                {

                    StartCoroutine(Wait(Combo));
                }
            }
        }
        else
        {
            foreach (var Combo in SortedMagazine)
            {

                if (Combo.Type == imgNumber)
                {

                    Combo.input.Comboable = false;
                }
            }
        }
    }

    public bool JokerIsActive;
    public IEnumerator Wait(Tile tile)
    {

        yield return new WaitForSeconds(0.25f);
        tile.input.Comboable = true;

    }
    public void DestoryTiles(int num)
    {
        int counter = 0;

        foreach (var item in SortedMagazine)
        {
            if (JokerIsActive)
            {
                if (item.Type == 99)
                {
                    StartCoroutine(item.input.DestoryTile());
                    //SortedMagazine.Remove(item);
                    //TilesInMagazine.Remove(item);
                }
            }
            if (item.Type == num)
            {
                StartCoroutine(item.input.DestoryTile());
                //SortedMagazine.Remove(item);
                counter++;

            }

            //StartCoroutine(DelayedSort(0.45f));
        }
        if (JokerIsActive)
        {
            Combos(counter + 1);

            print("Joker Combo");
        }
        else Combos(counter);
        print("Normal Combo");
        EventManager.Instance.DelayedSort?.Invoke(this, EventArgs.Empty);




    }


    public void Combos(int amount)
    {
        switch (amount)
        {
            case 3:
                EventManager.Instance.Match3Event?.Invoke(this, EventArgs.Empty);
                break;
            case 4:
                EventManager.Instance.BurnClipEvent?.Invoke(this, EventArgs.Empty);
                EventManager.Instance.Match4Event?.Invoke(this, EventArgs.Empty);
                break;
            case 5:
                EventManager.Instance.JokerEvent?.Invoke(this, EventArgs.Empty);
                EventManager.Instance.Match5Event?.Invoke(this, EventArgs.Empty);
                break;
            case 6:
                EventManager.Instance.MagazineSizeIncrease?.Invoke(this, EventArgs.Empty);
                EventManager.Instance.Match6Event?.Invoke(this, EventArgs.Empty);
                break;
            default:
                print("Bad Switch Combo Count");

                break;
        }
    }

    private IEnumerator DelayedsSort(float SortDelay)
    {
        yield return new WaitForSeconds(SortDelay);
        TileCounter();
        SederInMachsanit();
        yield return new WaitForSeconds(SortDelay);
        EventManager.Instance.MagazineSorterEve?.Invoke(this, EventArgs.Empty);
        //EventManager.Instance.CorutineStarter?.Invoke(this, EventArgs.Empty);
    }

    private void DelayedSortEvent(object sender, EventArgs e)
    {
        StartCoroutine(DelayedsSort(0.25f));
    }

    public bool IsJokerActive()
    {
        bool FoundJoker = false;
        foreach (var item in TilesInMagazine)
        {
            if (item == item.Joker)
            {
                FoundJoker = true;

            }
            else
            {
                FoundJoker = false;
            }
        }

        if (FoundJoker)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
