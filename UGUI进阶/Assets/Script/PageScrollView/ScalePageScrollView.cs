﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScalePageScrollView : PageScrollView
{
    #region 字段
    /// <summary>
    /// 所有页Object
    /// </summary>
    protected GameObject[] items;
    public float currentScale = 1f;
    public float otherScale = 0.6f;
    public int lastPage;
    public int nextPage;
    #endregion

    #region Unity回调
    protected override void Start()
    {
        base.Start();
        items = new GameObject[pageCount];
        //初始化所有的GameObject
        for(int i =0; i < pageCount; i++)
        {
            items[i] = transform.Find("Viewport/Content").GetChild(i).gameObject;
        }
    }

    protected override void Update()
    {
        base.Update();
        ListenerScale();
    }
    #endregion

    #region 方法
    //监听scale update 内执行
    public void ListenerScale()
    {
        //找到上一页和下一页
        for(int i = 0; i < pages.Length; i++)
        {
            if(pages[i] <= rect.horizontalNormalizedPosition)
            {
                lastPage = i;
            }
        }

        for (int i = 0; i < pages.Length; i++)
        {
            if (pages[i] > rect.horizontalNormalizedPosition)
            {
                nextPage = i;
                break;
            }
        }

        if(nextPage == lastPage)
        {
            return;
        }
        //rect水平偏移占比
        float percent = (rect.horizontalNormalizedPosition - pages[lastPage]) / (pages[nextPage] - pages[lastPage]);
        items[lastPage].transform.localScale = Vector3.Lerp(Vector3.one * currentScale, Vector3.one * otherScale, percent);
        items[nextPage].transform.localScale = Vector3.Lerp(Vector3.one * currentScale, Vector3.one * otherScale, 1 - percent);

        for(int i = 0; i < items.Length; i++)
        {
            if(i != lastPage && i != nextPage)
            {
                items[i].transform.localScale = Vector3.one * otherScale;
            }
        }
    }
    #endregion
}
