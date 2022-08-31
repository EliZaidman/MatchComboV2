using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KaimiraGames;

public class BarManager : MonoBehaviour
{

    //WeightedList<string> ChanceForCombo;
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

        if (BoostValue >= 100)
        {
            switch (Choose())
            {
                case "Joker":
                    print("Used Joker From Bar");
                    int rand = UnityEngine.Random.Range(0, board.TilesInBoard.Count - 1);
                    board.TilesInBoard[rand].Joker = true;
                    board.TilesInBoard[rand].gameObject.SetActive(false);
                    board.TilesInBoard[rand].gameObject.SetActive(true);
                    board.TilesInBoard[rand].name = "Joker";
                    break;

                case "Hammer":
                    print("Used Hammer From Bar");
                    int hammer = UnityEngine.Random.Range(0, board.TilesInBoard.Count - 1);
                    board.TilesInBoard.Remove(board.TilesInBoard[hammer]);
                    board.TilesInBoard[hammer].gameObject.SetActive(false);
                    break;

                case "Propellor":
                    print("Used Propellor From Bar");

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
        BoostValue -= 100;
        return ChanceForCombo.Next();
    }




}
