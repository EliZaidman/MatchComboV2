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
    float duration;
    private void Start()
    {
        duration = TileSorter.Instance.TileMoveSpeed;
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
        float t = 0;
        if (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(transform.position, mag.MagazineSlots[FindSlot].position, t / duration);
            yield return new WaitUntil(() => transform.position == mag.MagazineSlots[FindSlot].position);
        }
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
