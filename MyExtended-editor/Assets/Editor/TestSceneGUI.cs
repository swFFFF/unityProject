using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestScene))]
public class TestSceneGUI : Editor
{
    private void OnSceneGUI()//在scene窗口绘制UI
    {
        Handles.BeginGUI();

        if (GUILayout.Button("test1", GUILayout.Width(100), GUILayout.Height(100)))
        {
            Debug.Log(target.name);
        }

        GUILayout.Label(target.name);

        Handles.EndGUI();
    }

    [InitializeOnLoadMethod]
    static void InittalizeOnLoad() //初始化时绘制UI
    {
        SceneView.duringSceneGui += (SceneView) =>
        {
            Handles.BeginGUI();

            if (GUILayout.Button("test2"))
            {
                Debug.Log("test2");
            }

            Handles.EndGUI();
        };
    }
}
