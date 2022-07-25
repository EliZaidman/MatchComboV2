using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public int Type;
    public bool Joker;
    [HideInInspector]public TileInput input;
    private void Start()
    {
        input = GetComponent<TileInput>();
        BoardManager.Instance.TilesInBoard.Add(this);
    }
}
