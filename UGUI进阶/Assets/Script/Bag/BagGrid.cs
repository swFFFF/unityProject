using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BagGrid : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private ArticleItem articleItem;

    public ArticleItem ArticleItem
    {
        get { return articleItem; }
    }

    private Image bagImage;
    private Color defaultBagImageColor;

    private void Awake()
    {
        bagImage = transform.GetComponent<Image>();
        defaultBagImageColor = bagImage.color;
    }
    //设置格子数据
    public void SetArticleItem(ArticleItem articleItem)
    {
        this.articleItem = articleItem;
        this.articleItem.gameObject.SetActive(true);
        //设置父物体
        this.articleItem.transform.SetParent(transform);
        //设置位置
        this.articleItem.MoveToOrigin(() =>
        {
            //设置边距
            this.articleItem.GetComponent<RectTransform>().offsetMin = new Vector2(5, 5);
            this.articleItem.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -5);
        });
        //设置scale
        this.articleItem.transform.localScale = Vector3.one;

    }

    //清空格子
    public void ClearGrid()
    {
        articleItem.gameObject.SetActive(false);
        //articleItem.transform.SetParent(null);
        articleItem = null;
    }

    public void DragToThisGrid(ArticleItem articleItem)
    {
        //设置新格子 并清空格子原先内容
        BagGrid lastGrid = articleItem.transform.parent.GetComponent<BagGrid>();
        //判断格子有没有物品
        if (this.articleItem == null)
        {
            //清空格子
            lastGrid.ClearGrid();
            //设置
            SetArticleItem(articleItem);
        }
        else
        {
            //要进行交换
            lastGrid.SetArticleItem(this.articleItem);
            //设置
            SetArticleItem(articleItem);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (BagPanel._instance.currentDragArticle != null)
        {
            BagPanel._instance.currentHoverGrid = this;
            bagImage.color = Color.yellow;
        }

        if (this.articleItem != null)
        {
            //显示当前物品信息
            BagPanel._instance.articleInformation.Show();
            BagPanel._instance.articleInformation.SetShowInfo(this.articleItem.GetArticleInfo());
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        BagPanel._instance.currentHoverGrid = null;
        bagImage.color = defaultBagImageColor;
        //隐藏格子的物品信息
        BagPanel._instance.articleInformation.Hide();
    }
}
