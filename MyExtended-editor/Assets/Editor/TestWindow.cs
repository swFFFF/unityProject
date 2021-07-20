using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

//自定义编辑器窗口
public class TestWindow : EditorWindow, IHasCustomMenu
{
    private bool isOpen = false;
    private void OnGUI()
    {
        if(GUILayout.Button("TEST"))
        {
            Debug.Log("test window button");
        }
    }

    private void Awake()
    {
        
    }

    private void OnDestroy()
    {
        
    }

    private void OnFocus()
    {
        
    }

    private void OnLostFocus()
    {
        
    }

    private void Update()
    {
        
    }

    public void AddItemsToMenu(GenericMenu menu)    //添加下拉列表
    {
        menu.AddItem(new GUIContent("test1"), isOpen, () =>
        {
            isOpen = !isOpen;
            Debug.Log("test1");

            if(isOpen)
            {
                //TODO
            }
            else
            {
                //TODO
            }
        });

        menu.AddDisabledItem(new GUIContent("test2"));
    }
}
