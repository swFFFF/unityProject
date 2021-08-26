using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArticleType
{
    Weapon,
    Armor,
    Book,
    Drug,
}

public class Article
{
    public string name;
    public string spritePath;
    public ArticleType articleType;
    public int count;

    public Article(string name,string spritePath,ArticleType articleType, int count)
    {
        this.name = name;
        this.spritePath = spritePath;
        this.articleType = articleType;
        this.count = count;
    }
}
