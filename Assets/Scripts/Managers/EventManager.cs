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
    public EventHandler Match3Event;
    public EventHandler Match4Event;
    public EventHandler Match5Event;
    public EventHandler Match6Event;

    //Bar Events


    //Bar Incress Boosters
    public EventHandler BarIncressCombo3;
    public EventHandler BarIncressCombo4;
    public EventHandler BarIncressCombo5;
    public EventHandler BarIncressCombo6;
    public EventHandler BarIncressCombo7;

    //Pre-game boosters:
    public EventHandler BarPreGameFasterFillingBar;
    public EventHandler BarPreGameBarClipIncrease;
 // public EventHandler *Better bar rewards*

    //NormalBoosters
    public EventHandler BarJoker;
    public EventHandler BarHammer;
    public EventHandler BarPropellor;
    public EventHandler BarMagicTileChanger;

    //Mid-game boosters:
    public EventHandler BarUndo;
    public EventHandler BarReshuffle;
    public EventHandler BarMoveTilesFromClipToBoard;



}
