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
        EventManager.Instance.CorutineStopper += StopAnimCor;
        EventManager.Instance.CorutineStarter += StartAnim;
    }

    public bool StopAnim = false;
    private void LateUpdate()
    {
        //Change The Speed Depending if you won or not
        if (!StopAnim)
            SortMagazineCorutine(0.03f);
        //else
        //    SortMagazineCorutine(0.01f);


    }
    private void StartAnim(object sender, EventArgs e)
    {
        StopAnim = false;
    }
    private void StopAnimCor(object sender, EventArgs e)
    {
        StopAnim = true;
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
