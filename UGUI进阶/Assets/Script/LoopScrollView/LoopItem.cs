﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//处理item内部逻辑
public class LoopItem : MonoBehaviour
{
    private RectTransform itemRect;
    private RectTransform viewRect;
    private Vector3[] rectCorners;
    private Vector3[] viewCorners;

    #region 事件
    // Action 回调一对一
    public Action onAddHead;
    public Action onRemoveHead;
    public Action onAddLast;
    public Action onRemoveLast;
    #endregion
    private void Awake()
    {
        itemRect = transform.GetComponent<RectTransform>();
        viewRect = transform.GetComponentInParent<ScrollRect>().GetComponent<RectTransform>();
    }

    void Update()
    {
        ListenerCorners();
    }

    //获取四角坐标
    public void ListenerCorners()
    {
        //获取自身边界 （世界坐标）
        itemRect.GetWorldCorners(rectCorners);
        //获取显示区域边界 
        viewRect.GetWorldCorners(viewCorners);

        if(IsFirst())
        {
            if (rectCorners[0].y > viewCorners[1].y)
            {
                //隐藏并销毁头节点
                if(onRemoveHead != null)
                {
                    onRemoveHead();//委托事件
                }    
            }

            if (rectCorners[1].y < viewCorners[1].y)
            {
                //添加并显示头结点
                if(onAddHead != null)
                {
                    onAddHead();
                }
            }
        }

        if(IsLast())
        {
            if (rectCorners[1].y > viewCorners[0].y)
            {
                //添加并显示尾节点
                if (onAddLast != null)
                {
                    onAddLast();
                }
            }

            if (rectCorners[1].y < viewCorners[0].y)
            {
                //隐藏并销毁尾节点
                if(onRemoveLast != null)
                {
                    onRemoveLast();
                }    
            }
        }
    }

    public bool IsFirst()
    {
        for(int i = 0; i < transform.parent.childCount; i++)
        {
            if(transform.parent.GetChild(i).gameObject.activeSelf)
            {
                if(transform.GetChild(i) == transform)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }

    public bool IsLast()
    {
        for (int i = transform.parent.childCount; i >= 0; i--)
        {
            if (transform.parent.GetChild(i).gameObject.activeSelf)
            {
                if (transform.GetChild(i) == transform)
                {
                    return true;
                }
                break;
            }
        }
        return false;
    }
}
