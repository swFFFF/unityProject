using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDrag : MonoBehaviour, IBeginDragHandler, ICanvasRaycastFilter, IDragHandler
{
    private Vector3 mousePosition;

    private RectTransform rect;
    public Action onStartDrag;
    public Action onDraging;
    public Action onEndDrag;

    private bool isDraging = false;

    private void Awake()
    {
        rect = transform.GetComponent<RectTransform>();
        if(rect == null)
        {
            throw new System.Exception("只能拖拽UI物体");
        }
    }

    private void Update()
    {
        if(isDraging)
        {
            rect.anchoredPosition += (Vector2)(Input.mousePosition - mousePosition);
            mousePosition = Input.mousePosition;
            if (onDraging != null)
            {
                onDraging();
            }
        }

        if(Input.GetMouseButtonUp(0) && isDraging)
        {
            if (onEndDrag != null) { onEndDrag(); }
            isDraging = false;
        }
    }

    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        //物体位置加上鼠标移动位置
        mousePosition = Input.mousePosition;
        if (onStartDrag != null) {onStartDrag();}
        isDraging = transform;
    }

    //时间渗透
    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        return !isDraging;
    }

    public void OnDrag(PointerEventData eventData)
    {
        
    }
}
