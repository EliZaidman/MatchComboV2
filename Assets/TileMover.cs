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
        tile = GetComponent<Tile>();
        sort = TileSorter.Instance;
        mag = Magazine.Instance;
        EventManager.Instance.CorutineStopper += StopAnimCor;
        EventManager.Instance.CorutineStarter += StartAnim;
        duration = sort.TileMoveSpeed;
    }
    private void Update()
    {
        FindSlot = mag.SortedMagazine.IndexOf(this.tile);


    }
    private void LateUpdate()
    {
        if (mag.SortedMagazine.Contains(this.tile) && CanSort)
        {
            if (transform.position != mag.MagazineSlots[FindSlot].transform.position)
            {
                //Sort();
                StartCoroutine(CoSort());
            }
        }
    }






    IEnumerator CoSort()
    {
        Application.targetFrameRate = 50;
        print("Sorting" + this);
        float t = 0;
        if (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.Lerp(transform.position, mag.MagazineSlots[FindSlot].position, t / duration);
            yield return new WaitUntil(() => transform.position == mag.MagazineSlots[FindSlot].position);
        }
    }
    void Sort()
    {
        Application.targetFrameRate = 50;
        print("Sorting" + this);
        float t = 0;
        if (t < duration)
        {
            t += Time.deltaTime / duration;
            transform.position = Vector2.MoveTowards(transform.position, mag.MagazineSlots[FindSlot].position, t / duration);
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
