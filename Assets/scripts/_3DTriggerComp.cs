using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _3DTriggerComp : MonoBehaviour
{
    Animator anim;
    Collider coll;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
        anim = GetComponent<Animator>();
        AnimationClip[] clips = GetComponent<Animator>().runtimeAnimatorController.animationClips;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (anim)
        {
            anim.SetBool("in", true);
            foreach(var item in clips)
        }

        
    }
}
