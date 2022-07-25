using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayState : MonoBehaviour
{
    public static PlayState Instance { get; private set; }
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
    Magazine mag;
    public int currentScene = 0;


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
                mag.UI.SetActive(true);
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
            currentScene++;
            SceneManager.LoadScene(currentScene);
        }

    }
}
