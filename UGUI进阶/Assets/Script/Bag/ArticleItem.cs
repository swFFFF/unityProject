using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleItem : MonoBehaviour
{
    #region 字段
    private Image articleSprite;
    private Text number;
    private UIDrag uiDrag;

    private Article article;
    private Canvas canvas;
    private int defaultSort;
    private Vector3 currentLocalPosition;
    private float moveOriginTimer;  //计时器
    private float moveOriginTime = 0.2f;   //时间
    private bool isMovingOrigin = false;
    private Action onMoveEnd;
    #endregion

    #region Unity生命周期
    private void Awake()
    {
        articleSprite = transform.GetComponent<Image>();
        number = transform.Find("Text").GetComponent<Text>();
        uiDrag = transform.GetComponent<UIDrag>();
        canvas = transform.GetComponent<Canvas>();
        if (canvas != null)
        {
            defaultSort = canvas.sortingOrder;
        }
        //委托多播（组播）
        uiDrag.onStartDrag += this.OnStartDrag;
        uiDrag.onDraging += this.OnDrag;
        uiDrag.onEndDrag += this.OnEndDrag;
    }

    private void Update()
    {
        MovingOrigin();
    }
    #endregion

    #region 方法

    public void SetArticle(Article article)
    {
        this.article = article;
        //显示数据
        articleSprite.sprite = Resources.Load<Sprite>(article.spritePath);
        number.text = article.count.ToString();
    }

    public void OnStartDrag()
    {
        canvas.sortingOrder = defaultSort + 1;
        BagPanel._instance.currentDragArticle = this;
    }

    public void OnEndDrag()
    {
        if( BagPanel._instance.currentHoverGrid != null)
        {
            //切换格子
            BagPanel._instance.currentHoverGrid.DragToThisGrid(BagPanel._instance.currentDragArticle);
            canvas.sortingOrder = defaultSort;
        }
        else 
        {
            //回到原点
            MoveToOrigin(() =>
            {
                //恢复层级
                canvas.sortingOrder = defaultSort;
            });
        }

        BagPanel._instance.currentDragArticle = null;
    }

    public void OnDrag()
    {

    }

    //向圆点移动方法
    public void MovingOrigin()
    {
        if (isMovingOrigin)
        {
            moveOriginTimer += Time.deltaTime * (1 / moveOriginTime);
            transform.localPosition = Vector3.Lerp(currentLocalPosition, Vector3.zero, moveOriginTimer);
            if (moveOriginTimer >= 1)
            {
                isMovingOrigin = false;
                if (onMoveEnd != null)
                {
                    onMoveEnd();
                }
            }
        }
    }
    public void MoveToOrigin(Action onMoveEnd)
    {
        isMovingOrigin = true;
        moveOriginTimer = 0;
        currentLocalPosition = transform.localPosition;
        this.onMoveEnd = onMoveEnd;
    }

    public string GetArticleInfo()
    {
        return this.article.GetArticleInfo();
    }
    #endregion
}
