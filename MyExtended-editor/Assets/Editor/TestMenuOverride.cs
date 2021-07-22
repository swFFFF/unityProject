using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestMenuOverride
{
    [InitializeOnLoadMethod]
    static void InitOnLoad()
    {
        EditorApplication.hierarchyWindowItemOnGUI += (instanceID, rect) =>
        {
            if(Event.current != null && Event.current.button == 1 &&
                Event.current.type == EventType.MouseUp)
            {
                Debug.Log("鼠标抬起");
                //EditorUtility.DisplayPopupMenu(new Rect(Event.current.mousePosition.x, Event.current.mousePosition.y, 0 , 0), 
                //    "GameObject", null);
                //Event.current.Use();
            }
        };
    }
}
