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

    private void Start()
    {
        layerOrder = GetComponent<MeshRenderer>();
        layerOrder.sortingOrder = (layerOrder.sortingOrder * 10) - 2;
        layerOrderID = layerOrder.sortingOrder - 1;
        tile = GetComponent<Tile>();
        dim.GetComponent<SpriteRenderer>().sortingOrder = layerOrder.sortingOrder +1;
    }
    private void Update()
    {

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
                    dim.gameObject.SetActive(true);
                    print("inside");
                }
            }
        }
    }


    private void OnTriggerExit(Collider other)
    {

        tile.input.Interactable = true;
        dim.gameObject.SetActive(false);
    }
}
