using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTile : MonoBehaviour
{
    public bool _movingTile = false;
    public Transform target;
    Magazine _mag;


    private void Start()
    {
        EventManager.Instance.onClickOnTile += MoveingTile;
        _mag = Magazine.Instance;

    }
    public void MoveingTile(object sender, EventArgs e)
    {
        if (_movingTile == true)
        {
           // StartCoroutine(TileMover(1f));
            EventManager.Instance.onClickOnTile -= MoveingTile;
        }


    }


}

