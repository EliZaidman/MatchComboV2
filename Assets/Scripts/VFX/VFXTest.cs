using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using System;

public class VFXTest : MonoBehaviour
{
    public static VFXTest Instance { get; private set; }

    private void Update()
    {
        if (transform.position.x < -1.37f)
        {
            transform.position = new Vector3(-1.37f, transform.position.y);
        }
    }
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

        skel = GetComponent<SkeletonAnimation>();

    }
    SkeletonAnimation skel;
    public SkeletonDataAsset _Match4;
    public SkeletonDataAsset _Match5;
    public SkeletonDataAsset _Match6;
    public int AnimationIndex = 0;

    public Transform _MiddlePos;

    private void Start()
    {
        EventManager.Instance.Match4Event += Match4;
        EventManager.Instance.Match5Event += Match5;
        EventManager.Instance.Match6Event += Match6;




    }
    private void Match4(object sender, EventArgs e)
    {
        if (Magazine.Instance.MagazineIsFull)
        {
        transform.position = _MiddlePos.position;
        }
        AnimationIndex = UnityEngine.Random.Range(0, 3);
        skel.skeletonDataAsset = _Match4;
        skel.Initialize(true);

        if (AnimationIndex == 0)
        {
        skel.AnimationState.SetAnimation(1, "COOL", false);
        }
        if (AnimationIndex == 1)
        {
            skel.AnimationState.SetAnimation(1, "NICE", false);
        }
        if (AnimationIndex == 2)
        {
            skel.AnimationState.SetAnimation(1, "WOW!", false);
        }
    }

    private void Match5(object sender, EventArgs e)
    {
        AnimationIndex = UnityEngine.Random.Range(0, 2);
        skel.skeletonDataAsset = _Match5;
        skel.Initialize(true);

        if (AnimationIndex == 0)
        {
            skel.AnimationState.SetAnimation(1, "AWESOME", false);
        }
        if (AnimationIndex == 1)
        {
            skel.AnimationState.SetAnimation(1, "GREAT", false);
        }

    }

    private void Match6(object sender, EventArgs e)
    {
        skel.skeletonDataAsset = _Match6;
        skel.Initialize(true);
        skel.AnimationState.SetAnimation(1, "animation", false);
    }
}