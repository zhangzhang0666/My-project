using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    Light light;
    public bool islight;

    int timer;
    public int totaltime;
    // Start is called before the first frame update
    void Start()
    {
        light= GetComponent<Light>();
        light.intensity = 0;
        timer = 0;
        totaltime = 5;
    }

    private void OnEnable()
    {
        islight= true;
    }

    // Update is called once per frame
    void Update()
    {
        if (islight)
        {
            timer++;
            if (timer >= totaltime)
            {
                LightScale();
                timer = 0;
            }
        }
    }

    public void LightScale()
    {

        light.intensity = Mathf.Clamp(light.intensity+1, 0, 24);
    }

    public void LightActive()
    {
        gameObject.SetActive(true);
    }
}
