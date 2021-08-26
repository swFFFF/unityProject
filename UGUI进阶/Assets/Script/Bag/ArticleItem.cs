using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleItem : MonoBehaviour
{
    private Image articleSprite;
    private Text number;

    private Article article;
    private void Awake()
    {
        articleSprite = transform.GetComponent<Image>();
        number = transform.Find("Text").GetComponent<Text>();
    }

    public void SetArticle(Article article)
    {
        this.article = article;
        //显示数据
        articleSprite.sprite = Resources.Load<Sprite>(article.spritePath);
        number.text = article.count.ToString();
    }
}
