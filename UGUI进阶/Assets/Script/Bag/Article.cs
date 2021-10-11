using System.Collections;
using System.Collections.Generic;
using System.Text;
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

    public virtual string GetArticleInfo()
    {
        StringBuilder stringBuilder = new StringBuilder();
        //Text富文本
        stringBuilder.Append("<color=#92FF26>");
        stringBuilder.Append("名称：").Append(this.name);
        stringBuilder.Append("</color>");
        stringBuilder.Append("\n");
        stringBuilder.Append("<color=#FB4444>");
        stringBuilder.Append("类型：").Append(GetTypeName(this.articleType));
        stringBuilder.Append("</color>");
        stringBuilder.Append("\n");
        stringBuilder.Append("<color=#6565FB>");
        stringBuilder.Append("数量：").Append(this.count);
        stringBuilder.Append("</color>");
        return stringBuilder.ToString();
    }

    public string GetTypeName(ArticleType articleType)
    {
        switch (articleType)
        {
            case ArticleType.Weapon:
                return "武器";
            case ArticleType.Armor:
                return "护甲";
            case ArticleType.Book:
                return "技能书";
            case ArticleType.Drug:
                return "药品";
            default:
                break;
        }
        return "";
    }
}
