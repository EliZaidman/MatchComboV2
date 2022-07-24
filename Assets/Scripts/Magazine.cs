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

    public List<Tile> TilesInMagazine;
    public List<Tile> SortedMagazine;
    public List<Transform> MagazineSlots;
    public int mSize;
    [SerializeField] int _NumberOfones, _NumberOftwos, _NumberOfthrees, _NumberOffours, _NumberOffives, _NumberOfsix;
    public bool MagazineIsFull = false;


    private void Start()
    {
        EventManager.Instance.MagazineSorterEve += SortMagazine;
        // EventManager.Instance.ComboEvent += ;

    }
    private void Update()
    {
        if (mSize == TilesInMagazine.Count)
            MagazineIsFull = true;
        else
            MagazineIsFull = false;

        //for (int i = 0; i < SortedMagazine.Count; i++)
        //{
        //    SortedMagazine[i].transform.position = MagazineSlots[i].position;
        //}
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
        }

        ComboMaker(_NumberOfones, 1);
        ComboMaker(_NumberOftwos, 2);
        ComboMaker(_NumberOfthrees, 3);
        ComboMaker(_NumberOffours, 4);
        ComboMaker(_NumberOffives, 5);
        ComboMaker(_NumberOfsix, 6);
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
                default:
                    Debug.Log("error in switch");
                    break;
            }
        }
    }

    public void ComboMaker(int numberOf, int imgNumber)
    {
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
    }

    public IEnumerator Wait(Tile tile)
    {

        yield return new WaitForSeconds(0.25f);
        tile.input.Comboable = true;

    }
    public void DestoryCombo(int num)
    {
        int counter = 0;
        print("outsideif");

        foreach (var item in SortedMagazine)
        {
            if (item.Type == num)
            {
                // Do Destory Animation StartCoroutine(PlayAnim(item));
                item.gameObject.SetActive(false);
                //SortedMagazine.Remove(item);
                TilesInMagazine.Remove(item);
                counter++;
                print("inside if");
            }
        }
        Combos(counter);
        SederInMachsanit();

    }


    public void Combos(int amount)
    {
        switch (amount)
        {
            case 3:
                print("Destoyed 3");
                break;
            case 4:
                EventManager.Instance.JokerEvent?.Invoke(this, EventArgs.Empty);
                break;
            case 5:
                EventManager.Instance.MagazineSizeIncrease?.Invoke(this, EventArgs.Empty);
                break;
            case 6:
                print("Destoyed 6");
                break;
            default:
                break;
        }
    }
    //Check For Win
}
