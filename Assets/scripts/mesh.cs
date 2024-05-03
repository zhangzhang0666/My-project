using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mesh : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SkinnedMeshRenderer meshFilter = GetComponent<SkinnedMeshRenderer>();
        if (meshFilter != null && meshFilter.sharedMesh!= null)
        {
            // 获取模型的Mesh
            Mesh mesh = meshFilter.sharedMesh;
            // 获取模型的边界框（AABB）
            Bounds bounds = mesh.bounds;
            // 获取模型的尺寸
            Vector3 size = bounds.size;
            Debug.Log(size);
        }
        else
        {
            Debug.Log("找不到MeshFilter组件或Mesh为空");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
