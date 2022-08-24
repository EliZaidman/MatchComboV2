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
        EventManager.Instance.CorutineStopper += StopCorutine;
        EventManager.Instance.CorutineStarter += StartCorutine;
    }

    public bool WeWon = false;
    private void LateUpdate()
    {
        //Change The Speed Depending if you won or not
        if (!WeWon)
            SortMagazineCorutine(0.15f);
        else
            SortMagazineCorutine(0.01f);


    }
    private void StartCorutine(object sender, EventArgs e)
    {
        WeWon = false;
    }
    private void StopCorutine(object sender, EventArgs e)
    {
        WeWon = true;
    }

    public void SortMagazineCorutine(float duration)
    {
        //Takes the SortedMagazine and automaticly change its position to the magSlot
        //Yes its EVERY Second trying to fix its position
        //ONE DAY IT WILL BE BETTER I SWEAR
        //I WILL LEARN HOW TO MAKE IT BETTER
        //SOMETIMES TO WIN A WAR YOU MUST LOOSE A FIGHT!
        float t = 0;
        for (int i = 0; i < _mag.SortedMagazine.Count; i++)
        {
            t += Time.deltaTime;
            _mag.SortedMagazine[i].transform.position = Vector2.MoveTowards(_mag.SortedMagazine[i].transform.position, _mag.MagazineSlots[i].position, t / duration);
        }

    }
}
