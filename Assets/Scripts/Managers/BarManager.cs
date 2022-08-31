using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KaimiraGames;

public class BarManager : MonoBehaviour
{
    public static BarManager Instance { get; private set; }
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
    //WeightedList<string> ChanceForCombo;
    public GameObject hammerSprite;
    Magazine mag;
    EventManager Emanager;
    BoardManager board;
    [Header("Values")]
    [SerializeField] Slider Bar;
    [SerializeField] float BoostValue;
    [Space]

    [Header("Combos Values")]
    [SerializeField] float ComboOf3Value;
    [SerializeField] float ComboOf4Value;
    [SerializeField] float ComboOf5Value;
    [SerializeField] float ComboOf6Value;
    [SerializeField] float ComboOf7Value;

    [Header("Combos Chance")]
    [Space]
    [SerializeField] int ChanceForJoker;
    [SerializeField] int ChanceForHammer;
    [SerializeField] int ChanceForPropellor;
    [SerializeField] int ChanceForMagicTileChanger;


    private void Update()
    {
        BoostValue = Mathf.Clamp(BoostValue, 0, 250);
        Bar.value = BoostValue;

        if (BoostValue >= 100 || JokerInBoard())
        {
            if (BoostValue >= 100)
            {
                BoostValue -= 100;
            }
            switch (Choose())
            {
                case "Joker":
                    if (board.JokerInBoard() || mag.JokerIsActive)
                    {
                        rerolled = false;
                        ReRoll();
                        break;
                    }
                    else
                    {
                        JokerInPlay = true;
                        print("Used Joker From Bar");
                        int rand = UnityEngine.Random.Range(0, board.TilesInBoard.Count - 1);
                        board.TilesInBoard[rand].Joker = true;
                        board.TilesInBoard[rand].gameObject.SetActive(false);
                        board.TilesInBoard[rand].gameObject.SetActive(true);
                        board.TilesInBoard[rand].name = "Joker";
                        rerolled = true;
                        break;
                    }
                case "Hammer":
                    print("Used Hammer From Bar");
                    Emanager.BarHammerFromTile?.Invoke(this, EventArgs.Empty);
                    hammerSprite.SetActive(true);
                    rerolled = true;
                    break;

                case "Propellor":
                    List<Tile> type1 = new List<Tile>();
                    List<Tile> type2 = new List<Tile>();
                    List<Tile> type3 = new List<Tile>();
                    List<Tile> type4 = new List<Tile>();
                    List<Tile> type5 = new List<Tile>();
                    List<Tile> type6 = new List<Tile>();
                    List<Tile> type7 = new List<Tile>();
                    List<Tile> type8 = new List<Tile>();
                    foreach (var item in board.TilesInBoard)
                    {
                        if (item.Type == 1)
                        {
                            type1.Add(item);
                        }
                        if (item.Type == 2)
                        {
                            type2.Add(item);
                        }
                        if (item.Type == 3)
                        {
                            type3.Add(item);
                        }
                        if (item.Type == 4)
                        {
                            type4.Add(item);
                        }
                        if (item.Type == 5)
                        {
                            type5.Add(item);
                        }
                        if (item.Type == 6)
                        {
                            type6.Add(item);
                        }
                        if (item.Type == 7)
                        {
                            type7.Add(item);
                        }
                        if (item.Type == 8)
                        {
                            type8.Add(item);
                        }
                    }
                    Delete(type1);
                    Delete(type2);
                    Delete(type3);
                    Delete(type4);
                    Delete(type5);
                    Delete(type6);
                    Delete(type7);
                    Delete(type8);
                    break;

                case "MagicTileChanger":
                    print("Used MagicTileChanger From Bar");

                    break;
                default:
                    print("Bad Switch Combo Count");
                    break;

            }
        }
    }

    private void Delete(List<Tile> tile)
    {
        int min = 1;
        for (int i = 0; i < 8; i++)
        {
            if (tile.Count < min)
            {
                int rand = UnityEngine.Random.Range(0, tile.Count - 1);
                board.TilesInBoard.Remove(tile[rand]);
                tile[rand].gameObject.SetActive(false);
                break;
            }
            else
            {
                min++;
                print("Added 1 to count");
            }
        }

    }

    protected WeightedList<string> ChanceForCombo = new WeightedList<string>();
    void Start()
    {
        // WeightedList<string> ChanceForCombo;

        board = BoardManager.Instance;
        ChanceForCombo.Add("Joker", ChanceForJoker);
        ChanceForCombo.Add("Hammer", ChanceForHammer);
        ChanceForCombo.Add("Propellor", ChanceForPropellor);
        ChanceForCombo.Add("MagicTileChanger", ChanceForMagicTileChanger);
        //Register the Combos
        mag = Magazine.Instance;
        Emanager = EventManager.Instance;

        //Combos
        Emanager.BarIncressCombo3 += ComboOf3;
        Emanager.BarIncressCombo4 += ComboOf4;
        Emanager.BarIncressCombo5 += ComboOf5;
        Emanager.BarIncressCombo6 += ComboOf6;
        Emanager.BarIncressCombo7 += ComboOf7;

        //PreBoosters
        Emanager.BarPreGameBarClipIncrease += PreClipIncrease;
        Emanager.BarPreGameFasterFillingBar += PreBarIncrease;

        //Boosters
        Emanager.BarJoker += Joker;
        Emanager.BarHammer += Hammer;
        Emanager.BarPropellor += Propellor;
        Emanager.BarMagicTileChanger += MagicTileChanger;

        //MidGameBoosters
        Emanager.BarMoveTilesFromClipToBoard += ClipToBoard;
        Emanager.BarReshuffle += Reshuffle;
        Emanager.BarUndo += Undo;
    }

    #region ComboOfX
    private void ComboOf3(object sender, EventArgs e)
    {
        BoostValue += ComboOf3Value;
    }
    private void ComboOf4(object sender, EventArgs e)
    {
        BoostValue += ComboOf4Value;
    }
    private void ComboOf5(object sender, EventArgs e)
    {
        BoostValue += ComboOf5Value;
    }
    private void ComboOf6(object sender, EventArgs e)
    {
        BoostValue += ComboOf6Value;
    }
    private void ComboOf7(object sender, EventArgs e)
    {
        BoostValue += ComboOf7Value;
    }
    #endregion

    #region BarPowers
    private void Joker(object sender, EventArgs e)
    {

    }
    private void Hammer(object sender, EventArgs e)
    {

    }
    private void Propellor(object sender, EventArgs e)
    {

    }
    private void MagicTileChanger(object sender, EventArgs e)
    {

    }
    #endregion

    #region Pre-game boosters
    private void PreBarIncrease(object sender, EventArgs e)
    {

    }

    private void PreClipIncrease(object sender, EventArgs e)
    {

    }
    #endregion

    #region Mid-game boosters:
    private void ClipToBoard(object sender, EventArgs e)
    {

    }
    private void Reshuffle(object sender, EventArgs e)
    {

    }
    private void Undo(object sender, EventArgs e)
    {

    }
    #endregion


    string Choose()
    {

        return ChanceForCombo.Next();
    }

    bool JokerInPlay = false;
    bool JokerInBoard()
    {
        if (JokerInPlay && !rerolled)
            return true;
        else
            return false;
    }

    bool rerolled = false;
    string ReRoll()
    {
        print("REROLL BOYYYYY");
        return Choose();
    }




}
