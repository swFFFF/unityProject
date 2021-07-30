using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public enum PageScrollType
{
    Horizontal,
    Vertical
}

public class PageScrollView : MonoBehaviour, IEndDragHandler, IBeginDragHandler
{
    #region 字段
    protected ScrollRect rect;
    protected int pageCount;
    private RectTransform content;
    protected float[] pages;
    public float moveTime = 0.3f;
    private float timer = 0;
    private float startMovePos;
    protected int curPage = 0;
    private bool isMoving = false;
    /// <summary>
    /// 是否开启自动滚动
    /// </summary>
    public bool isAutoScroll = true;
    private float AutoScrollTime = 2;
    private float AutoScrollTimer = 0;
    private bool isDraging = false;

    public PageScrollType pageScrollType = PageScrollType.Horizontal;
    #endregion

    #region 事件
    public Action<int> OnPageChange;
    #endregion

    #region Unity回调
    // Start is called before the first frame update
    protected virtual void Start()
    {
        Init();
    }
    // Update is called once per frame
    protected virtual void Update()
    {
        ListenerMove();
        ListenerAutoScroll();
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        this.ScrollToPage(CalculateMiniDistancePage());
        isDraging = false;
        AutoScrollTimer = 0;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        isDraging = true;
    }
    #endregion

    #region 方法
    public void Init()
    {
        rect = gameObject.transform.GetComponent<ScrollRect>();
        if (rect == null)
        {
            throw new System.Exception("未查询到ScrollRect");
        }
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        pageCount = content.childCount; //查看子节点个数
        if(pageCount == 1)
        {
            throw new System.Exception("无需分页");
        }
        pages = new float[pageCount];
        for (int i = 0; i < pages.Length; i++)
        {
            switch(pageScrollType)
            {
                case PageScrollType.Horizontal:
                    pages[i] = i * (1.0f / (float)(pageCount - 1));
                    break;
                case PageScrollType.Vertical:
                    pages[i] = 1 - i * (1.0f / (float)(pageCount - 1));
                    break;
            }
        }
    }
    public void ScrollToPage(int page)
    {
        this.curPage = page;
        timer = 0;
        switch (pageScrollType)
        {
            case PageScrollType.Horizontal:
                startMovePos = rect.horizontalNormalizedPosition;
                break;
            case PageScrollType.Vertical:
                startMovePos = rect.verticalNormalizedPosition;
                break;
            default:
                break;
        }
        
        isMoving = true;
        if(OnPageChange != null)
        {
            OnPageChange(this.curPage);
        }
    }

    public void ListenerMove()
    {
        if (isMoving)
        {
            timer += Time.deltaTime * (1 / moveTime); //移动到位置所需时间
            switch(pageScrollType)
            {
                case PageScrollType.Horizontal:
                    rect.horizontalNormalizedPosition = Mathf.Lerp(startMovePos, pages[curPage], timer);
                    break;
                case PageScrollType.Vertical:
                    rect.verticalNormalizedPosition = Mathf.Lerp(startMovePos, pages[curPage], timer);
                    break;
                default:
                    break;
            }
            
            if (timer >= 1)
            {
                isMoving = false;
            }
        }
    }

    public void ListenerAutoScroll()
    {
        if (isDraging) { return; }
        if(isAutoScroll)
        {
            AutoScrollTimer += Time.deltaTime;
            if(AutoScrollTimer >= AutoScrollTime)
            {
                AutoScrollTimer = 0;
                curPage++;
                curPage %= pageCount;
                ScrollToPage(curPage);
            }
        }
    }

    /// <summary>
    /// 计算距离最近的页面
    /// </summary>
    public int CalculateMiniDistancePage()
    {
        int minipage = 0;
        //计算出距离最近的页面
        for (int i = 0; i < pages.Length; i++)
        {
            switch (pageScrollType)
            {
                case PageScrollType.Horizontal:
                    if (Mathf.Abs(pages[i] - rect.horizontalNormalizedPosition) < Mathf.Abs(pages[minipage] - rect.horizontalNormalizedPosition))
                    {
                        minipage = i;
                    }
                    break;
                case PageScrollType.Vertical:
                    if (Mathf.Abs(pages[i] - rect.verticalNormalizedPosition) < Mathf.Abs(pages[minipage] - rect.verticalNormalizedPosition))
                    {
                        minipage = i;
                    }
                    break;
                default:
                    break;
            };
        }
        return minipage;
    }

    #endregion
}
