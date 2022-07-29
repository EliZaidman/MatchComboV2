using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboMaker : MonoBehaviour
{

    Magazine mag;
    void Start()
    {
        mag = Magazine.Instance;
        EventManager.Instance.MatchEvent += MatchCombo;
        EventManager.Instance.JokerEvent += JokerCombo;
        EventManager.Instance.MagazineSizeIncrease += MagazineAddCombo;
    }

    private void MatchCombo(object sender, EventArgs e)
    {
        print("Combo Of 3 (Called From" + this +")");
    }
    private void JokerCombo(object sender, EventArgs e)
    {
        foreach (var item in mag.SortedMagazine)
        {
            StartCoroutine(item.input.DestoryTile());
        }

    }
    private void MagazineAddCombo(object sender, EventArgs e)
    {
        Magazine.Instance.mSize++;
        mag.CheckSlots();
    }



    // Update is called once per frame
    void Update()
    {

    }


}
