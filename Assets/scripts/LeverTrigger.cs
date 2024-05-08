using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    
    [SerializeField]protected int number;
    public Transform pivot;
    private MeshFilter meshFilter;
    [HideInInspector]
    public Vector2 size;

    [HideInInspector] public Vector2 realSize;
    public bool isUp=false;
    private void Start()
    {
        meshFilter = GetComponent<MeshFilter>();
        if (meshFilter != null && meshFilter.mesh != null)
        {
            // 获取模型的Mesh
            Mesh mesh = meshFilter.mesh;
            // 获取模型的边界框（AABB）
            Bounds bounds = mesh.bounds;
            // 获取模型的尺寸
            //if (isUp)
            //{
            //    size = new Vector2(bounds.size.x,bounds.size.y);
            //}
            //else
            //{
            //    size = new Vector2(bounds.size.x,bounds.size.z);
            //}
            Vector3 bsz = bounds.size;
            Vector3 tl = transform.localScale;
            Vector3 sz= new Vector3(bsz.x* tl.x,bsz.y*tl.y,bsz.z*tl.z);
            size = new Vector2(sz.x,sz.z);
            //realSize= new Vector2(bsz.x,bsz.z);
        }
        else
        {
            Debug.Log("找不到MeshFilter组件或Mesh为空");
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player3D"))
        {
            Debug.Log(size);
            LevelManager.Instance.set3Dlevel(gameObject,number,size);
            LevelManager.Instance.isDraw = true;
        }
    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player3D"))
        {
            LevelManager.Instance.isDraw = false;
        }
    }
}

