using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    Magazine mag;
    [SerializeField]private GameObject Joker;
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
        GameObject joker;
        joker = Instantiate(Joker);
        joker.GetComponent<TileInput>().PressedOnTile();
        mag.ComboIsActive = true;
        EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);

    }
    private void MagazineAddCombo(object sender, EventArgs e)
    {
        mag.transform.localScale = new Vector3(0.42f, 0.4f, 0.4f);
        Magazine.Instance.mSize++;
        mag.CheckSlots();
        foreach (var item in mag.MagazineSlots)
        {
            item.transform.position = new Vector3(item.transform.position.x - 0.3f, item.transform.position.y, item.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


}
