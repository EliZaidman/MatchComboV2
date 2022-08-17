using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    Magazine mag;
    [SerializeField] private GameObject Joker;
    void Start()
    {
        mag = Magazine.Instance;
        EventManager.Instance.MatchEvent += MatchCombo;
        EventManager.Instance.JokerEvent += JokerCombo;
        EventManager.Instance.MagazineSizeIncrease += MagazineAddCombo;
        EventManager.Instance.MagazineSizeIncrease += BurnClipEvent;
    }

    private void BurnClipEvent(object sender, EventArgs e)
    {
        foreach (var item in mag.SortedMagazine)
        {
            item.gameObject.SetActive(false);
        }
        print("Burn");
    }

    private void MatchCombo(object sender, EventArgs e)
    {
        print("Combo Of 3 (Called From" + this + ")");
    }
    private void JokerCombo(object sender, EventArgs e)
    {
        GameObject joker;
        joker = Instantiate(Joker, new Vector3(0, -3.8f, 0), Quaternion.identity);
        StartCoroutine(WaitBeforeRegister(joker));
        //EventManager.Instance.SortTileEvent?.Invoke(this, EventArgs.Empty);

    }
    private void MagazineAddCombo(object sender, EventArgs e)
    {
        //if (mag.mSize >= 8)
        //{
        //    foreach (var item in mag.SortedMagazine)
        //    {
        //        StartCoroutine(item.input.DestoryTile());
        //    }
        //}
        //else
        //{
            mag.transform.localScale = new Vector3(0.42f, 0.4f, 0.4f);
            Magazine.Instance.mSize++;
            mag.CheckSlots();
            foreach (var item in mag.MagazineSlots)
            {
                item.transform.position = new Vector3(item.transform.position.x - 0.3f, item.transform.position.y, item.transform.position.z);
            }
        //}
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator WaitBeforeRegister(GameObject tile)
    {
        yield return new WaitForSeconds(0.05f);
        tile.GetComponent<TileInput>().PressedOnTile();
    }
}
