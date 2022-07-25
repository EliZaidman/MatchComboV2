using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : MonoBehaviour
{
    Magazine mag;
    private void Start()
    {
        mag = Magazine.Instance;
        EventManager.Instance.CheckLostEvent += LostEvent;
        EventManager.Instance.CheckWinEvent += WinEvent;
    }

    private void LostEvent(object sender, EventArgs e)
    {
        if (true)
        {
            if (mag.SortedMagazine.Count == mag.mSize && mag._NumberOfones < 3 && mag._NumberOftwos < 3 && mag._NumberOfthrees < 3 && mag._NumberOffours < 3 && mag._NumberOffives < 3 && mag._NumberOfsix < 3)
            {
                print("YouLoose");
            }
        }

    }

    private void WinEvent(object sender, EventArgs e)
    {
        int emptySpaces = mag.MagazineSlots.Count - mag.SortedMagazine.Count;
        if (BoardManager.Instance.TilesInBoard.Count <= emptySpaces)
        {
            //DO WIN ANIMATION
            print("You Win!");
        }

    }
}
