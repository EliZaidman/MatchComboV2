using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMover : MonoBehaviour
{

    TileSorter sort;
    int FindSlot;
    Magazine mag;
    Tile tile;
    private void Start()
    {
        tile = GetComponent<Tile>();
        sort = TileSorter.Instance;
        mag = Magazine.Instance;
    }
    private void Update()
    {
        if (mag.SortedMagazine.Contains(this.tile))
        {
            FindSlot = mag.SortedMagazine.IndexOf(this.tile);
            StartCoroutine(Sort());
        }


    }
    IEnumerator Sort()
    {
        yield return new WaitForEndOfFrame();
        float t = 0;
        t += Time.deltaTime;
        transform.position = Vector2.MoveTowards(transform.position, mag.MagazineSlots[FindSlot].position, t / sort.TileMoveSpeed);
    }
}
