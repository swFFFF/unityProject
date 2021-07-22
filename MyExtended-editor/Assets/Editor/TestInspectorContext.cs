using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestInspectorContext : MonoBehaviour
{
    [MenuItem("CONTEXT/Transform/SetPosition")] //路径固定使用CONTEXT
    static void SetPosition(MenuCommand command)
    {
        Transform transform = command.context as Transform;
        if(transform != null)
        {
            transform.position += Vector3.one;
        }
    }
}
