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
    [SerializeField] private Sprite JokerSprite;

    bool swapped = false;
    private void Update()
    {
        if (Joker && !swapped)
        {
            GetComponentInChildren<SpriteRenderer>().sprite = JokerSprite;
            swapped = true;
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


    IEnumerator WaitBeforeRegister()
    {
        yield return new WaitForSeconds(0.35f);
        mag.TilesInMagazine.Add(this);
        yield return new WaitForSeconds(0.35f);
        mag.SortedMagazine.Add(this);

    }

}
