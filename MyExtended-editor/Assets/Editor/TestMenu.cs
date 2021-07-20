using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestMenu : MonoBehaviour
{
    [MenuItem("SWF/Test1 %#M",false,20)]//路径（设置快捷键%代表CTRL shift# UP/DOWN/LEFT/RIGHT）是否验证函数 优先级 优先级相差10以上会出现分割线
    static void Test1()
    {
        Debug.Log("111");
    }

    [MenuItem("SWF/Test2", false,1)]
    static void Test2()
    {
        Debug.Log("111");
    }

    [MenuItem("SWF/Test1",true)]
    static bool Test1Validate()
    {
        return false;
    }
}
