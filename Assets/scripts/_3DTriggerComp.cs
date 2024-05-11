using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class _3DTriggerComp : MonoBehaviour
{
    Collider coll;
    public PlayableDirector director;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player3D"&& director)
        {
            director.Play();
        }
    }
}
