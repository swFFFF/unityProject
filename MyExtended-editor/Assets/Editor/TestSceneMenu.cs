using UnityEditor;
using UnityEngine;

public class TestSceneMenu : MonoBehaviour
{
    private void Start()
    {
        
    }
    [InitializeOnLoadMethod]    //初始化加载编辑器类方法的特性
    static void InitializeOnLoad()
    {
        UnityEditor.SceneView.duringSceneGui += (sceneView) =>
        {
            if (Event.current != null && Event.current.button == 1 && Event.current.type == EventType.MouseUp)//监听鼠标事件
            {
                //Debug.Log("鼠标右键抬起");

                Rect position = new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y - 100, 100, 100);
                GUIContent[] contents = new GUIContent[]    //GUI 元素数组
                {
                    new GUIContent("Test1"), new GUIContent("SWF/Test2")
                };
                EditorUtility.DisplayCustomMenu(position, contents, -1, (data, opt, select) => 
                {
                    Debug.LogFormat("data:{0}, opt:{1},select:{2}, value:{3}", data,opt,select, opt[select]);
                }, null);

                Event.current.Use();    //鼠标点击时间不再传递
            }
        };//委托事件
    }
}
