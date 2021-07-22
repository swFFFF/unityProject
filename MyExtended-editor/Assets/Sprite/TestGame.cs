using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode] //在编辑器模式下也执行
public class TestGame : MonoBehaviour
{
    private void OnGUI()    //运行时在game视图添加UI
    {
        if(GUILayout.Button("test1", GUILayout.Width(100)))
        {
            Debug.Log("test1");
        }
    }
}
