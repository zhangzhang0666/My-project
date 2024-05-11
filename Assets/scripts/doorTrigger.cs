using System;
using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class doorTrigger : MonoBehaviour
{
    public int number;
    public int starNumber;
    public Animator animatorEnd;
    // Start is called before the first frame update
    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelManager.Instance.levelNumber2D == number && LevelManager.Instance.star2D==starNumber )
        {
            animatorEnd.SetTrigger("end");
            PlayerController2D.Instance.gameObject.SetActive(false);
            PlayerController2D.Instance.sprite.gameObject.SetActive(false);
        }
    }
    
}
