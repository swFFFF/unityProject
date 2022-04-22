using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MVPTest : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            //显示主面板
            MainPresenter.ShowMe();
        }
        else if (Input.GetKeyDown(KeyCode.N))
        {
            //隐藏面板
            MainPresenter.HideMe();
        }
    }
}
