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
    public Sprite D_Normal;

    public Sprite SpriteJoker;
    [SerializeField] Sprite D_SpriteJoker;

    public bool SpineActivated;
    public SpriteRenderer _Sprite;
    private void Awake()
    {
        SpineActivated = true;
    }
    private void Start()
    {
        layerOrder = GetComponent<MeshRenderer>();
        skel = GetComponent<SkeletonAnimation>();
        _Sprite.sortingOrder = layerOrder.sortingOrder;
        tile = GetComponent<Tile>();
        //dim.GetComponent<SpriteRenderer>().sortingOrder = layerOrder.sortingOrder + 1;
        ToggleSpine();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Tile" || other.gameObject.tag == "Joker")
        {
            if (other.gameObject.GetComponent<LayerChecker>())
            {
                if (_Sprite.sortingOrder < other.GetComponent<LayerChecker>()._Sprite.sortingOrder)
                {
                    if (tile.Joker)
                    {
                    tile.input.Interactable = false;
                        _Sprite.sprite = SpriteJoker;
                    }
                    else if (!tile.Joker && other.gameObject.GetComponent<Tile>().DiedOfHammer)
                    {
                        tile.input.Interactable = true;
                        _Sprite.sprite = Normal;
                    }
                    else
                    {
                        _Sprite.sprite = D_Normal;
                        tile.input.Interactable = false;
                    }
                }
            }

        }
    }

    private void OnTriggerExit(Collider other)
    {
        tile.input.Interactable = true;
        if (tile.Joker)
        {
            _Sprite.sprite = SpriteJoker;
        }
        else
            _Sprite.sprite = Normal;
    }

    public void ToggleSpine()
    {
        SpineActivated = !SpineActivated;

        if (SpineActivated)
        {
            layerOrder.enabled = true;
            skel.enabled = true;
            _Sprite.enabled = false;
        }
        else if (!SpineActivated)
        {
            layerOrder.enabled = false;
            skel.enabled = false;
            _Sprite.enabled = true;
        }
    }
}
