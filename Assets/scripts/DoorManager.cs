using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Playables;

public class DoorManager : MonoBehaviour
{
    Animator anim;
    bool isOne;
    public PlayableDirector director;
    public GameObject level1trigger;
    public GameObject player2d;
    public GameObject playerSprite;
    public GameObject endpos;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        isOne = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetBool("isFirst", true);
        }
        if (level1trigger.GetComponent<level1trigger>().m_isGetInTrigger&&isOne)
        {
            director.Play();
            isOne = false;
            playerSprite.SetActive(false);
            Invoke("FinishDoor", (float)director.duration);
        }
    }

    void FinishDoor()
    {
        player2d.transform.position = endpos.transform.position;
        playerSprite.SetActive(true);
        gameObject.SetActive(false);
    }
}
