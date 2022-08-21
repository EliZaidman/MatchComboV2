using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }
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


    //Tiles Events
    public EventHandler onClickOnTile;
    public EventHandler onComboMake;
    public EventHandler CorutineStopper;
    public EventHandler CorutineStarter;

    //Combo Events
    public EventHandler ComboEvent;
    public EventHandler MatchEvent;
    public EventHandler JokerEvent;
    public EventHandler BurnClipEvent;
    public EventHandler MagazineSizeIncrease;
    public EventHandler CheckLostEvent;
    public EventHandler CheckWinEvent;
    public EventHandler ComboCaller;

    //Magazine Events
    public EventHandler MagazineSorterEve;
    public EventHandler DelayedSort;

    //Spine Events
    public EventHandler VFXAllign;
    public EventHandler Match4Event;
    public EventHandler Match5Event;
    public EventHandler Match6Event;

}
