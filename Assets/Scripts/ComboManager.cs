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
        EventManager.Instance.Match3Event += MatchCombo;
        EventManager.Instance.JokerEvent += JokerCombo;
        EventManager.Instance.MagazineSizeIncrease += MagazineAddCombo;
        EventManager.Instance.BurnClipEvent += BurnClipEvent;
    }

    private void BurnClipEvent(object sender, EventArgs e)
    {
        if (!mag.MagazineIsFull)
        {
            EventManager.Instance.VFXAllign?.Invoke(this, EventArgs.Empty);
        }
        foreach (var item in mag.SortedMagazine)
        {
            StartCoroutine(item.input.DestoryTile());
        }
    }

    private void MatchCombo(object sender, EventArgs e)
    {
        print("Combo Of 3 (Called From" + this + ")");
    }
    private void JokerCombo(object sender, EventArgs e)
    {
        EventManager.Instance.VFXAllign?.Invoke(this, EventArgs.Empty);
        GameObject joker;
        joker = Instantiate(Joker, new Vector3(0, -3.8f, 0), Quaternion.identity);
        StartCoroutine(WaitBeforeRegister(joker));
    }
    private void MagazineAddCombo(object sender, EventArgs e)
    {
        EventManager.Instance.VFXAllign?.Invoke(this, EventArgs.Empty);
        if (mag.mSize >= 8)
        {
            EventManager.Instance.JokerEvent?.Invoke(this, EventArgs.Empty);

            return;
        }
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

    IEnumerator WaitBeforeRegister(GameObject tile)
    {
        yield return new WaitForSeconds(0.15f);
        tile.GetComponent<TileInput>().PressedOnTile();
    }
}
