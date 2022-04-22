using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MPTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //显示主面板
            UIManager.GetInstance().ShowPanel<MP_MainPanel>("MainPanel");
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            //隐藏面板
            UIManager.GetInstance().HidePanel("MainPanel");
        }
    }
}
