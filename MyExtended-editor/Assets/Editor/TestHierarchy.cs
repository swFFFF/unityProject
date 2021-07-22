using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestHierarchy : MonoBehaviour
{
    [MenuItem("GameObject/Test2", false , 0)]
    static void Test2()
    {
        Debug.Log("test2");
    }

    [InitializeOnLoadMethod]    //初始化加载编辑器类方法的特性
    static void InitializeOnLoad()
    {
        EditorApplication.hierarchyWindowItemOnGUI += (instanceID, rect) =>
        {
            if (Selection.activeObject != null && instanceID == Selection.activeObject.GetInstanceID()) //用实例ID来判断
            {
                string assGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));    //根据路径转换成GUID

                rect.x = rect.width - 50;
                rect.width = 100;
                if (GUI.Button(rect, "delete"))
                {
                    GameObject.DestroyImmediate(Selection.activeObject);
                }

            }
        };
    }
}
