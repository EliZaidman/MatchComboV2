using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{

    Magazine mag;
    EventManager Emanager;
    [SerializeField] private GameObject Joker;
    void Start()
    {
        //Register the Combos
        mag = Magazine.Instance;
        Emanager = EventManager.Instance;

        Emanager.Match3Event += MatchCombo;
        Emanager.JokerEvent += JokerCombo;
        Emanager.MagazineSizeIncrease += MagazineAddCombo;
        Emanager.BurnClipEvent += BurnClipEvent;
    }

    private void BurnClipEvent(object sender, EventArgs e)
    {
        //If the Mag is not full, use the normal vfx pos
        if (!mag.MagazineIsFull)
        {
            Emanager.VFXAllign?.Invoke(this, EventArgs.Empty);
        }
        //Loop from the Magazine and destory each tile 1 by 1
        foreach (var item in mag.SortedMagazine)
        {
            StartCoroutine(item.input.DestoryTile());
        }
        //CheckForCombo(0.2f);

    }


    //At least make the Combo of 3 do something its crying of boringness q=q
    //Look How Empty IT IS
    //HOW DARE YOU
    //WHY YOU DO THIS
    private void MatchCombo(object sender, EventArgs e)
    {
        print("Combo Of 3 (Called From" + this + ")");
    }

    //Yeha its Joker Combo
    //You Make Local Refrence and add the refrence to the list
    //Wait You Dont DAFAK
    //Huh so you just Instantiate an Joker as refrence
    private void JokerCombo(object sender, EventArgs e)
    {
        print("Combo Of JokerCombo (Called From" + this + ")");

        Emanager.VFXAllign?.Invoke(this, EventArgs.Empty);
        GameObject joker;
        joker = Instantiate(Joker, new Vector3(0, -3.8f, 0), Quaternion.identity);
        //StartCoroutine(WaitBeforeRegister(joker));
        //CheckForCombo(0.2f);

    }
    //If it dosnt have Magazine Dosnt have 8 slots add 1 more
    //If it dose, add Joker instad
    //Ofc you add Joker, you dont smart enough to make something else.
    //What about Combo of 7!??!
    //What about combo of 8?!?!?!
    private void MagazineAddCombo(object sender, EventArgs e)
    {
        print("Combo Of MagazineAddCombo (Called From" + this + ")");
        //Call the VFX yada yada yada
        Emanager.VFXAllign?.Invoke(this, EventArgs.Empty);
        //Call the Joker if not yada yada yada
        if (mag.mSize >= 8)
        {
            Emanager.JokerEvent?.Invoke(this, EventArgs.Empty);

        //ofc if it did enter to joker, no need to continue with the code.
            return;
        }
        //Compansate the Extra Slot by Increesing the size by *0.12
        mag.transform.localScale = new Vector3(0.42f, 0.4f, 0.4f);
        mag.mSize++;
        mag.CheckSlots();
        //Compansate the Extra Slot by Decreesing the x by -0.3
        foreach (var item in mag.MagazineSlots)
        {
            item.transform.position = new Vector3(item.transform.position.x - 0.3f, item.transform.position.y, item.transform.position.z);
        }
        //CheckForCombo(0.2f);
    }

    IEnumerator WaitBeforeRegister(GameObject tile)
    {
        yield return new WaitForSeconds(0.35f);
        tile.GetComponent<TileInput>().PressedOnTile();
    }  
    
    IEnumerator CheckForCombo(float time)
    {
        yield return new WaitForSeconds(time);
        EventManager.Instance.ComboEvent?.Invoke(this, EventArgs.Empty);
    }

}
