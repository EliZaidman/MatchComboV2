using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public float counter = 0;
    public List<Tile> TilesInBoard;
    Tile randomTile;
    Magazine mag;
    public static BoardManager Instance { get; private set; }
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
    private void Start()
    {
        mag = Magazine.Instance;
    }
    bool gaveHint;
    public float TimeUntilHintPopUp;
    [HideInInspector] public float timer;
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= TimeUntilHintPopUp && !gaveHint && !mag.MagazineIsFull && !mag.MagazineIsEmpty)
        {
            ChooseRandomTileFromList();
        }
        if (timer < 1)
        {
            gaveHint = false;
            counter = 0;
        }
    }

    private void ChooseRandomTileFromList()
    {

        ChooseRandomTile();

        foreach (var item in TilesInBoard)
        {
            if (randomTile.Type != item.Type)
            {
                ChooseRandomTile();
            }

            else if (item.Type == randomTile.Type && item.input.Interactable && counter < 2)
            {
                item.GetComponent<Animation>().Play("HintAnim");
                //item.GetComponent<Animator>().enabled = true;
                counter++;
                //yield return new WaitUntil(() => !item.GetComponent<Animation>().isPlaying);
            }
            

        }
        gaveHint = true;
    }

    private void ChooseRandomTile()
    {
        int itemIndex = Random.Range(0, (mag.TilesInMagazine.Count));

        randomTile = Magazine.Instance.TilesInMagazine[itemIndex];
        print("Random Tile is " + randomTile);
    }
}
