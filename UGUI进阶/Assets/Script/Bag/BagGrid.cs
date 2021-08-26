using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGrid : MonoBehaviour
{
    private ArticleItem articleItem;

    public ArticleItem ArticleItem
    {
        get { return articleItem; }
    }
    
    //设置格子数据
    public void SetArticleItem(ArticleItem articleItem)
    {
        this.articleItem = articleItem;
        //设置父物体
        this.articleItem.transform.SetParent(transform);
        //设置位置
        this.articleItem.transform.localPosition = Vector3.zero;
        //设置scale
        this.articleItem.transform.localScale = Vector3.one;
        //设置
        this.articleItem.GetComponent<RectTransform>().offsetMin = new Vector2(5,5);
        this.articleItem.GetComponent<RectTransform>().offsetMax = new Vector2(-5, -5);
    }

    //清空格子
    public void ClearGrid()
    {
        articleItem.gameObject.SetActive(false);
        articleItem.transform.SetParent(null);
        articleItem = null;
    }
}
