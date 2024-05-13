using System;
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
    public float speedScale = 1f;
    private Rigidbody _rigidbody;
    private float horizontal; 
    private float vertical;
    
    private Vector3 lastPosition1;
    private Vector3 lastPosition2;
    [HideInInspector]
    public Transform pivot3D;

    [HideInInspector] public Transform nip;
    private Vector3 movement;

    public float rotatespeed=800f;

    private Animator animator;

    private float currentSpeed;
    private float targetSpeed;
    [HideInInspector] public Transform transform3D;
    public void Awake()
    {
        Instance = this;
        transform3D = transform;
    }
    void Start()
    {
        lastPosition1 = transform.position;
        lastPosition2 = transform.position;
        _rigidbody = GetComponent<Rigidbody>();
        
        animator = GetComponent<Animator>();
        animator.SetFloat("scaleFactor",1/animator.humanScale);
    }
    void Update()
    {
        if (CameraManager.Instance.is2D)
        {
            return;
        }
        GetInput();
        rotate();
        Draw();
    }
    private void OnAnimatorMove()
    {
        if (CameraManager.Instance.is2D)
        {
            return;
        }
        Move();
    }

    void GetInput()
    {
        float horizontal=Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movement=Vector3.ClampMagnitude(new Vector3(horizontal,0f,vertical),1f);
    }

    void Move()
    {
        targetSpeed = speed * movement.magnitude;
        animator.speed = speedScale;
        currentSpeed = Mathf.Lerp(currentSpeed,targetSpeed,  0.8f);
        
        animator.SetFloat("speed",currentSpeed);
        Debug.Log(animator.velocity);
        _rigidbody.velocity = animator.velocity;
        //characterController.SimpleMove(animator.velocity);
        //feetTween = Mathf.Repeat(animator.GetCurrentAnimatorStateInfo(0).normalizedTime, 1f);
        //feetTween = feetTween > 0.5f ? 1f : -1f;
    }

    void rotate()
    {
        if(movement.Equals(Vector3.zero))
            return;
        Quaternion targetRotation = Quaternion.LookRotation(movement, Vector3.up);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotatespeed * Time.deltaTime);
    }

    private void Draw()
    {
        if(!LevelManager.Instance.isDraw)
            return;
        Vector3 relativepoint = pivot3D.InverseTransformPoint(nip.position);
        Vector2 newpoint = new Vector2(relativepoint.x, relativepoint.y);
        Debug.Log(newpoint);

        if (Vector3.Distance(nip.position,lastPosition1) > 0.03f)
        {
            LevelManager.Instance.AddPoints(newpoint);
            lastPosition1 = nip.position;
        }

        if (Vector3.Distance(nip.position, lastPosition2) > 0.01f)
        {
            LevelManager.Instance.drawMask(newpoint);
            lastPosition2 = nip.position;
        }
    }
}

