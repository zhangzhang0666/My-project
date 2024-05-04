using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverTrigger : MonoBehaviour
{
    
    [SerializeField]protected int number;
    public Transform pivot;
    private MeshFilter meshFilter;
    public Vector3 size;
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
            size = bounds.size;
            Debug.Log(size);
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
            LevelManager.Instance.set3Dlevel(gameObject,number,new Vector2(size.x,size.z));
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

