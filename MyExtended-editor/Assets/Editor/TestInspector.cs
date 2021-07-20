using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Camera))]//指定组件
public class TestInspector : Editor
{
    //重写Inspector面板
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if(GUILayout.Button("test1"))
        {
            Debug.Log("test1");
            Camera camera = this.target as Camera;
            if(camera != null)
            {
                camera.depth++;
            }
        }
    }
}
