using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Type;
    public bool Joker;
    [HideInInspector] public TileInput input;
    Magazine mag;
    TileSorter sort;
    [HideInInspector] public TileMover move;


    private void Update()
    {
        if (Joker)
        {
            Type = 99;
        }
    }
    int FindSlot;
    private void Start()
    {
        move = GetComponent<TileMover>();
        sort = TileSorter.Instance;
        mag = Magazine.Instance;
        input = GetComponent<TileInput>();
        BoardManager.Instance.TilesInBoard.Add(this);
    }
    private void OnEnable()
    {
        if (Joker)
        {
            GetComponent<SpriteRenderer>().sprite = GetComponent<LayerChecker>().SpriteJoker;
        }

    }

    IEnumerator WaitBeforeRegister()
    {
        yield return new WaitForSeconds(0.35f);
        mag.TilesInMagazine.Add(this);
        yield return new WaitForSeconds(0.35f);
        mag.SortedMagazine.Add(this);

    }

}
