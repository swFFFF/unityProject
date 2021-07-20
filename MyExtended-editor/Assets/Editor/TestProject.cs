using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestProject : MonoBehaviour
{
    [MenuItem("Assets/Test1")]
    static void Test1()
    {
        Debug.Log("test1");


    }

    [InitializeOnLoadMethod]    //初始化加载编辑器类方法的特性
    static void InitializeOnLoad()
    {
        EditorApplication.projectWindowItemOnGUI += (guid, rect) =>
        {
            //GUI.Button(new Rect(100, 100, 100, 100), "Test1");
            if (Selection.activeObject != null)
            {
                string assGuid = AssetDatabase.AssetPathToGUID(AssetDatabase.GetAssetPath(Selection.activeObject));    //根据资源路径转换成GUID
                if (assGuid == guid)//只在对应的选择后面绘制UI
                {
                    rect.x = rect.width - 100;
                    rect.width = 100;
                    //GUI.Button(rect, "Test1");
                    if (GUI.Button(rect, "delete"))
                    {
                        //AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(Selection.activeObject));    //删除资源文件
                        Debug.Log("删除文件:" + AssetDatabase.GetAssetPath(Selection.activeObject));
                    }
                }
            }
        };
    }
}
