using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{

    TileSorter sort;
    int FindSlot;
    Magazine mag;
    Tile tile;
    bool CanSort;
    private void Start()
    {
        EventManager.Instance.CorutineStopper += StopAnimCor;
        EventManager.Instance.CorutineStarter += StartAnim;
        tile = GetComponent<Tile>();
        sort = TileSorter.Instance;
        mag = Magazine.Instance;
    }
    private void Update()
    {
        if (mag.SortedMagazine.Contains(this.tile) && CanSort)
        {
            FindSlot = mag.SortedMagazine.IndexOf(this.tile);
            if (transform.position != mag.MagazineSlots[FindSlot].transform.position)
            {
            StartCoroutine(Sort());
            }
        }
    }
    IEnumerator Sort()
    {
        print("Sorting" + this);
        yield return new WaitForEndOfFrame();
        float t = 0;
        t += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, mag.MagazineSlots[FindSlot].position, t / sort.TileMoveSpeed);
    }

    private void StartAnim(object sender, EventArgs e)
    {
        CanSort = true;
    }
    private void StopAnimCor(object sender, EventArgs e)
    {
        CanSort = false;
    }
}
