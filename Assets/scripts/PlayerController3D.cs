using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
public class PlayerController3D : MonoBehaviour
{
    public static PlayerController3D Instance;
    public float speed = 5f;
    private Rigidbody _rigidbody;
    private float horizontal; 
    private float vertical;
    
    private Vector3 lastPosition1;
    private Vector3 lastPosition2;
    public Transform pivot3D;
    
    public void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        lastPosition1 = transform.position;
        lastPosition2 = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
    }
    void Update()
    {
        
        horizontal=Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");     
    }
    private void FixedUpdate()
    {
        if (CameraManager.Instance.is2D)
        {
            return;
        }
        Vector3 move = new Vector3(horizontal * speed, 0f,vertical * speed);
        _rigidbody.MovePosition(_rigidbody.position + move * Time.deltaTime);
        
        if(!LevelManager.Instance.isDraw)
            return;
        Vector3 relativepoint = pivot3D.InverseTransformPoint(transform.position);
        Vector2 newpoint = new Vector2(relativepoint.x, relativepoint.z);
        if (Vector3.Distance(_rigidbody.position,lastPosition1) > 0.03f)
        {
            LevelManager.Instance.AddPoints(newpoint);
            lastPosition1 = _rigidbody.position;
        }

        if (Vector3.Distance(_rigidbody.position, lastPosition2) > 0.01f)
        {
            LevelManager.Instance.drawMask(newpoint);
            lastPosition2 = _rigidbody.position;
        }
    }
}

