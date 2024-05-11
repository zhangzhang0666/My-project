using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class brush : MonoBehaviour
{
    public int number;
    public Transform brushPivot;
    public Transform brushNip;
    public TwoBoneIKConstraint leftHand;
    public TwoBoneIKConstraint rightHand;
    private bool isPick;
    private bool isPickable;
    private void Update()
    {
        if (isPickable)
        {
            popTips();
            if (Input.GetKey(KeyCode.Q))
            {
                LevelManager.Instance.setBrush(number);
                transform.SetParent(PlayerController3D.Instance.transform3D);
                transform.position = brushPivot.position;
                transform.rotation = brushPivot.rotation;
                transform.localScale = brushPivot.localScale;
                leftHand.weight = 1f;
                rightHand.weight = 1f;
                PlayerController3D.Instance.nip = brushNip;
                isPick = true;
                isPickable = false;
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(isPick)
            return;
        if (other.CompareTag("Player3D"))
        {
            isPickable = true;
            
        }
    }

    private void popTips()
    {
        Debug.Log("捡笔");
    }
}
