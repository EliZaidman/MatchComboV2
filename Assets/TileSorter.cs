using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSorter : MonoBehaviour
{
    public static TileSorter Instance { get; private set; }
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
    Magazine _mag;
    private void Start()
    {
        _mag = Magazine.Instance;
        EventManager.Instance.SortTileEvent += MoveTile;
    }

    private void MoveTile(object sender, EventArgs e)
    {
        StartCoroutine(TileMover(5f));
    }

    public IEnumerator TileMover(float duration)
    {
        EventManager.Instance.MagazineSorterEve?.Invoke(this, EventArgs.Empty);
        print("TIleMoverr");
        float t = 0;
        while (t < duration)
        {
            yield return new WaitForEndOfFrame();
            for (int i = 0; i < Magazine.Instance.SortedMagazine.Count; i++)
            {
                t += Time.deltaTime;
                _mag.SortedMagazine[i].transform.position = Vector2.Lerp(_mag.SortedMagazine[i].transform.position, _mag.MagazineSlots[i].position, t / duration);
            }
        }
        EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);
    }
}
