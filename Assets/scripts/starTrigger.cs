using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class starTrigger : MonoBehaviour
{
    public int number;
    public GameObject set2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (LevelManager.Instance.levelNumber2D == number)
        {
            LevelManager.Instance.star2D++;
            gameObject.SetActive(false);
            set2D.SetActive(false);
        }
    }
}
