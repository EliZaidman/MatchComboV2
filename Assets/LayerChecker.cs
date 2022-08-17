using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;
using System;

public class LayerChecker : MonoBehaviour
{
    Tile tile;
    public GameObject dim;
    private SkeletonAnimation skel;
    MeshRenderer layerOrder;
    public int layerOrderID;
    public Sprite Normal;
    public Sprite Dimmed;
    public bool SpineActivated;
    public SpriteRenderer _Sprite;
    private void Awake()
    {
        SpineActivated = true;
    }
    private void Start()
    {
        layerOrder = GetComponent<MeshRenderer>();
        layerOrder.sortingOrder = (layerOrder.sortingOrder * 10) - 2;
        _Sprite.sortingOrder = layerOrderID;
        tile = GetComponent<Tile>();
        //dim.GetComponent<SpriteRenderer>().sortingOrder = layerOrder.sortingOrder + 1;
        CheckSpineCondition();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tile" || other.gameObject.tag == "Joker")
        {
            if (other.gameObject.GetComponent<LayerChecker>())
            {
                if (layerOrderID < other.gameObject.GetComponent<LayerChecker>().layerOrderID)
                {
                    tile.input.Interactable = false;
                    _Sprite.sprite = Dimmed;
                    print("inside");
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {

        tile.input.Interactable = true;
        _Sprite.sprite = Normal;
    }

    private void CheckSpineCondition()
    {
        SpineActivated = !SpineActivated;

        if (SpineActivated)
        {
            layerOrder.enabled = true;
            skel.enabled = true;
        }
        else if (!SpineActivated)
        {
            layerOrder.enabled = false;
            skel.enabled = false;
        }
    }
}
