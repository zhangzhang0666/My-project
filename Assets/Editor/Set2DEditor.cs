using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(set2D))]
public class Set2DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        // 获取目标脚本对象
        set2D myScript = (set2D)target;

        // 在Inspector面板上添加一个按钮
        if (GUILayout.Button("Set2D"))
        {
            // 当点击按钮时执行的操作
            myScript.Set();
        }
    }
}
