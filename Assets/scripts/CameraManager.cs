using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CameraManager : MonoBehaviour
{
    [SerializeField]private CinemachineVirtualCamera Camera3D;
    [SerializeField]private CinemachineVirtualCamera Camera2D;
    [SerializeField]private CinemachineVirtualCamera[] Cameras;
    public static CameraManager  Instance;
    public bool is2D=false;

    private bool lock_change;
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Camera2D = Cameras[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.E) && !lock_change)
        {
            StartCoroutine(Lock_change());
            if(!is2D)
            {
                Camera2D.Priority = 20;
                Camera3D.Priority = 10;
                is2D = true;
            }
            else
            {
                Camera3D.Priority = 20;
                Camera2D.Priority = 10;
                is2D = false;
            }
        }
    }

    public void changeCamera(int number)
    {
        Camera2D.Priority = 10;
        Camera2D = Cameras[number];
        Camera2D.Priority = 20;
    }

    IEnumerator Lock_change()
    {
        lock_change = true;
        yield return new WaitForSeconds(1f);
        lock_change = false;
    }
}
