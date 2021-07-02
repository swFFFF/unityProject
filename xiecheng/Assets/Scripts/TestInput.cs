using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInput : MonoBehaviour
{
    public GUIStyle uIStyle;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Input.GetMouseButtonDown(0);
        //Input.GetKeyDown(KeyCode.Space);
        //int i = Input.touchCount;
    }

    private void OnGUI()
    {
        //GUILayout.Label(Input.mousePosition.ToString(), uIStyle);

        //Event e = Event.current;//只能在OnGui内调用
        //if(e.isKey)
        //{
        //    Debug.Log("当前按下了："+ e.keyCode);
        //}

        float h = Input.GetAxis("Horizontal");
        GUILayout.Label(h.ToString(),uIStyle);
    }
}
