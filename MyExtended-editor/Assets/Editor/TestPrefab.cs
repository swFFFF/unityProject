using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//窗口中显示预览信息
public class TestPrefab : EditorWindow
{
    private Object obj;
    private Object lastObj;
    private Editor previewObj;

    private void OnGUI()
    {
        obj = EditorGUILayout.ObjectField(obj, typeof(Object), false);  //创建一个字段

        if(obj != null && obj != lastObj)
        {
            previewObj = Editor.CreateEditor(obj);
            lastObj = obj;
        }

        if(previewObj != null && previewObj.HasPreviewGUI())
        {
            previewObj.OnPreviewGUI(GUILayoutUtility.GetRect(400, 400), EditorStyles.label);    //绘制object
        }
    }
}
