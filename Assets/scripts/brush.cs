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
    public Transform leftTarget;
    public Transform rightTarget;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player3D"))
        {
            popTips();
            if (Input.GetKeyDown(KeyCode.Q))
            {
                LevelManager.Instance.setBrush(number);
                transform.SetParent(other.transform);
                transform.position = brushPivot.position;
                transform.rotation = brushPivot.rotation;
                transform.localScale = brushPivot.localScale;
                leftHand.weight = 1f;
                rightHand.weight = 1f;
                other.GetComponent<PlayerController3D>().nip = brushNip;
            }
        }
    }

    private void popTips()
    {
        Debug.Log("捡笔");
    }
}
