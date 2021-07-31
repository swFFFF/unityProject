using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventTest : MonoBehaviour, 
    IPointerUpHandler,IPointerClickHandler,IPointerDownHandler,IPointerExitHandler,IPointerEnterHandler,
    IBeginDragHandler,IEndDragHandler
{//监听鼠标事件
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("开始拖拽");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("结束拖拽");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("鼠标点击");
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("鼠标按下");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("鼠标进入");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("鼠标退出");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("鼠标抬起");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
