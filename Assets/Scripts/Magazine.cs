using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Magazine : MonoBehaviour
{
    public static Magazine Instance { get; private set; }
    EventManager Emanager;
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
        Emanager = EventManager.Instance;
        Emanager.MagazineSorterEve += SortMagazine;
        Emanager.DelayedSort += DelayedSortEvent;
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

        if (Input.GetKey(KeyCode.E))
        {
            foreach (var item in SortedMagazine)
            {
                if (item == item.Joker)
                {
                    SortedMagazine[4] = item;
                }
            }
        }
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
            //Sidur(NumOfJokers, 99, i);
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
    bool wentInto = false;
    private void Sidur(int Seira, int Mispar, int Rezef)
    {
        // If Joker //IS NOT\\ You Have Same Type And Same Name Add +1 To The Counter
        if (Seira == Rezef && !JokerIsActive)
        {
            foreach (var item in TilesInMagazine)
            {

                if (item.Type == Mispar)
                {
                    SortedMagazine.Add(item);
                    print("NOT ACTIVE");
                }
            }
        }

        // If Joker //IS Active\\ And You Have Same Type And Same Name Add +1 And Fix JokerPoS
        else
        {
            if (Seira == Rezef && JokerIsActive)
            {

                foreach (var item in TilesInMagazine)
                {
                    if (item.Type == Mispar)
                    {
                        SortedMagazine.Add(item);

                    }

                }
                //Checking From Highest Combo To Lowest So it allways will be with Highest Combo Possible.
                JokerPlace(6);
                JokerPlace(5);
                JokerPlace(4);
                JokerPlace(3);
                JokerPlace(2);

                //It Dose it only when thereis Combo/Its First Tile So It Will Be Registerd
                foreach (var item in TilesInMagazine)
                {

                    if (item.Type == 99 && !SortedMagazine.Contains(item))
                    {
                        SortedMagazine.Add(item);
                        print("ADDED JOKER");
                    }
                }

            }
        }

    }


    private void JokerPlace(int combo)
    {
        if (_NumberOfones == combo
        || _NumberOftwos == combo
        || _NumberOfthrees == combo
        || _NumberOffours == combo
        || _NumberOffives == combo
        || _NumberOfsix == combo
        || _NumberOfSeven == combo
        || _NumberOfEight == combo
        || NumberOfNine == combo)
        {
            foreach (var item in TilesInMagazine)
            {
                //Adding The Joker To List Beacuse i dont do it manualy above(It Dose Alot Of Bugs)

                if (item.Type == 99 && !SortedMagazine.Contains(item) && NumOfJokers == 1)
                {
                    SortedMagazine.Add(item);
                    print("ADDED JOKER");
                }
                if (item == item.Joker && NumOfJokers <= 2)
                {
                    //If It Found Combo Take it Place and Announce to everyone not to take "Joker Spot"
                    if (combo > 2)
                    {
                        wentInto = true;
                        Swap(SortedMagazine, combo, combo);
                        break;
                    }
                    else
                    //Forcses the List To Take the slot after Combo
                    //If The Highest Combo 4 For Example, it will take slot 4
                    //Beacuse The list Start From 0 its easy to manuly and effectivly place the Joker at the right place
                    if (!wentInto)
                    {
                        SortedMagazine[combo] = item;
                        print("Place Joker At The Right Place");

                    }
                }
            }
        }
    }

    //Black Magic Ignore This
    public static IList<T> Swap<T>(IList<T> list, int indexA, int indexB)
    {
        (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
        return list;
    }
    private void TileCounter()
    {
        //Reset The Counters
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
        //Search for right Tile Type and add it to the right counter
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

    public void ComboMaker(int TileAmountIs, int TileType)
    {
        //If Joker is active The Lowest amount of the same tiles is 2 for Combo
        if (JokerIsActive && TileAmountIs > 1)
        {
            foreach (var Combo in SortedMagazine)
            {

                if (Combo.Type == TileType)
                {
                    //Waits for Other Methods to be over so it will register just in time
                    StartCoroutine(Wait(Combo));
                }
                //99 is Joker
                if (Combo.Type == 99)
                {
                    //FailSafe if it dosnt Add The Joker For SomeReason(May happen on slow phones)
                    if (NumOfJokers < 1)
                    {
                        NumOfJokers++;
                    }
                }
            }

        }
        else

        //Normal Combos, Lowest amount of tiles is for combo is 3
        if (TileAmountIs > 2)
        {
            foreach (var Combo in SortedMagazine)
            {

                if (Combo.Type == TileType)
                {
                    //Waits for Other Methods to be over so it will register just in time to Annouce this tile can be Interactable AKA Comboable
                    StartCoroutine(Wait(Combo));
                }
            }
        }
        else
        {
            foreach (var Combo in SortedMagazine)
            {

                if (Combo.Type == TileType)
                {
                    //Waits for Other Methods to be over so it will register just in time to Annouce this tile can be Interactable AKA Comboable
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
            //If Joker is Active Make Sure its Beeing Deleted as well as the other tiles
            if (JokerIsActive)
            {
                if (item.Type == 99)
                {
                    StartCoroutine(item.input.DestoryTile());
                    //SortedMagazine.Remove(item);
                    //TilesInMagazine.Remove(item);
                }
            }
            //For Everytile that beeing destory add +1 To The Counter, in the end it will annouce the combo 
            if (item.Type == num)
            {
                counter++;
                StartCoroutine(item.input.DestoryTile());
                //SortedMagazine.Remove(item);
                //EventManager.Instance.MagazineSorterEve?.Invoke(this, EventArgs.Empty);
            }

            //StartCoroutine(DelayedSort(0.45f));
        }

        //Easy way to make the combo +1
        if (JokerIsActive)
            Combos(counter + 1);
        else
            Combos(counter);

        //Call The Sort Little bit later so it will have enough time to finish all the methods.
        Emanager.DelayedSort?.Invoke(this, EventArgs.Empty);




    }

    //Take the Combo And call the right event
    public void Combos(int amount)
    {
        switch (amount)
        {
            case 3:
                Emanager.Match3Event?.Invoke(this, EventArgs.Empty);
                break;
            case 4:
                Emanager.BurnClipEvent?.Invoke(this, EventArgs.Empty);
                Emanager.Match4Event?.Invoke(this, EventArgs.Empty);
                break;
            case 5:
                Emanager.JokerEvent?.Invoke(this, EventArgs.Empty);
                Emanager.Match5Event?.Invoke(this, EventArgs.Empty);
                break;
            case 6:
                Emanager.MagazineSizeIncrease?.Invoke(this, EventArgs.Empty);
                Emanager.Match6Event?.Invoke(this, EventArgs.Empty);
                break;
            case 7:
                Emanager.MagazineSizeIncrease?.Invoke(this, EventArgs.Empty);
                Emanager.Match6Event?.Invoke(this, EventArgs.Empty);
                break;
            case 8:
                Emanager.MagazineSizeIncrease?.Invoke(this, EventArgs.Empty);
                Emanager.Match6Event?.Invoke(this, EventArgs.Empty);
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
        Emanager.MagazineSorterEve?.Invoke(this, EventArgs.Empty);
        //EventManager.Instance.CorutineStarter?.Invoke(this, EventArgs.Empty);
    }

    private void DelayedSortEvent(object sender, EventArgs e)
    {
        StartCoroutine(DelayedsSort(0.25f));
    }


    //Maybe Good Bool to check for Joker?
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
