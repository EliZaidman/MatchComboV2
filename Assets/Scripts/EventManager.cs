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
    public  EventHandler onClickOnTile;
    public  EventHandler onComboMake;
    public  EventHandler SortTileEvent;

    //Combo Events
    public  EventHandler ComboEvent;
    public  EventHandler MatchEvent;
    public  EventHandler JokerEvent;
    public  EventHandler MagazineSizeIncrease;
    public EventHandler CheckLostEvent;
    public EventHandler CheckWinEvent;

    //Sort Events
    public  EventHandler MagazineSorterEve;
    public class TileCounterEventArgs : EventArgs
    {
        public int NumberOfones, NumberOftwos, NumberOfthrees, NumberOffours, NumberOffives, NumberOfsix;

    }



}
