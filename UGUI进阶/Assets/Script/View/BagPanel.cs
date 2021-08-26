using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : ViewBase
{
    #region 数据
    private List<Article> articles = new List<Article>();
    private List<GameObject> articleItems = new List<GameObject>(); 
    #endregion

    #region 字段
    public GameObject articalItemPrefab;
    public BagGrid[] bagGrids;
    public MenuPanel menuPanel;
    #endregion

    #region Unity回调
    private void Awake()
    {
        InitArticleData();
        bagGrids = transform.GetComponentsInChildren<BagGrid>();
    }

    private void Start()
    {
        LoadData();
    }
    #endregion
    #region 方法
    public override void Hide()
    {
        base.Hide();
        menuPanel.Show();
    }

    public override void Show()
    {
        Invoke("ShowExc", 1.0f);
    }

    public void ShowExc()
    {
        base.Show();
    }

    //初始化物品数据
    public void InitArticleData()
    {
        //武器
        articles.Add(new Article("奥伯莱恩裂魂之剑", "Sprite/weapon/weapon_0", ArticleType.Weapon, 1));
        articles.Add(new Article("龙蛇大剑", "Sprite/weapon/weapon_1", ArticleType.Weapon, 2));
        articles.Add(new Article("诅咒大剑", "Sprite/weapon/weapon_2", ArticleType.Weapon, 3));
        articles.Add(new Article("辉煌圣剑", "Sprite/weapon/weapon_3", ArticleType.Weapon, 4));
        articles.Add(new Article("皲齿重剑", "Sprite/weapon/weapon_4", ArticleType.Weapon, 5));
        articles.Add(new Article("牛头人酋长的献祭", "Sprite/weapon/weapon_5", ArticleType.Weapon, 6));
        //药品
        articles.Add(new Article("苹果", "Sprite/drug/drug_0", ArticleType.Drug, 1));
        articles.Add(new Article("香蕉", "Sprite/drug/drug_1", ArticleType.Drug, 2));
        articles.Add(new Article("橙子", "Sprite/drug/drug_2", ArticleType.Drug, 3));
        articles.Add(new Article("葡萄", "Sprite/drug/drug_3", ArticleType.Drug, 4));
        articles.Add(new Article("木瓜", "Sprite/drug/drug_4", ArticleType.Drug, 5));
        articles.Add(new Article("哈密瓜", "Sprite/drug/drug_5", ArticleType.Drug, 6));
        //护甲
        articles.Add(new Article("战士束甲", "Sprite/clothes/clothes_0", ArticleType.Armor, 1));
        articles.Add(new Article("轻灵皮甲", "Sprite/clothes/clothes_1", ArticleType.Armor, 2));
        articles.Add(new Article("法师布甲", "Sprite/clothes/clothes_2", ArticleType.Armor, 3));
        articles.Add(new Article("沙漠布衣", "Sprite/clothes/clothes_3", ArticleType.Armor, 4));
        articles.Add(new Article("精灵外衣", "Sprite/clothes/clothes_4", ArticleType.Armor, 5));
        articles.Add(new Article("贵族服饰", "Sprite/clothes/clothes_5", ArticleType.Armor, 6));
        articles.Add(new Article("贵族正装", "Sprite/clothes/clothes_5", ArticleType.Armor, 7));
        //技能道具
        articles.Add(new Article("神圣技能书", "Sprite/book/book_4", ArticleType.Book, 1));
        articles.Add(new Article("邪恶技能书", "Sprite/book/book_5", ArticleType.Book, 2));
        articles.Add(new Article("交易技能书", "Sprite/book/book_6", ArticleType.Book, 3));
        articles.Add(new Article("魅惑技能书", "Sprite/book/book_7", ArticleType.Book, 4));
        articles.Add(new Article("暗影技能书", "Sprite/book/book_8", ArticleType.Book, 5));

    }

    //加载数据(全部)
    public void LoadData()
    {
        HideAllArticleItems();
        for (int i =0; i < articles.Count; i++)
        {
            GetBagGrid().SetArticleItem(LoadArtivcalItem(articles[i]));
        }
    }

    public void LoadData(ArticleType articleType)
    {
        HideAllArticleItems();
        for (int i = 0; i < articles.Count; i++)
        {
            if(articles[i].articleType == articleType)
            {
                GetBagGrid().SetArticleItem(LoadArtivcalItem(articles[i]));
            }
        }
    }

    //获取一个空闲的格子
    public BagGrid GetBagGrid()
    {
        for(int i = 0; i < bagGrids.Length; i++)
        {
            if(bagGrids[i].ArticleItem == null)
            {
                return bagGrids[i];
            }
        }
        return null;
    }

    public ArticleItem LoadArtivcalItem(Article article)
    {
        GameObject obj = GetArticleItem();
        ArticleItem articleItem = obj.GetComponent<ArticleItem>();
        articleItem.SetArticle(article);
        return articleItem;
    }

    //获取实例化对象
    public GameObject GetArticleItem()
    {
        for (int i = 0; i < articleItems.Count; i++)
        {
            if(articleItems[i].activeSelf == false)
            {
                articleItems[i].SetActive(true);
                return articleItems[i];
            }
        }
        return GameObject.Instantiate(articalItemPrefab);
    }

    //清理 隐藏所有物品
    public void HideAllArticleItems()
    {
        for(int i = 0; i < bagGrids.Length; i++)
        {
            if(bagGrids[i].ArticleItem != null)
            {
                bagGrids[i].ClearGrid();
            }
        }
    }
    #endregion

    #region 点击事件
    public void OnAllToggleValueChange(bool v)
    {
        if(v)
        {
            LoadData();
        }
    }

    public void OnWeaponToggleValueChange(bool v)
    {
        if (v)
        {
            LoadData(ArticleType.Weapon);
        }
    }
    public void OnArmorToggleValueChange(bool v)
    {
        if (v)
        {
            LoadData(ArticleType.Armor);
        }
    }
    public void OnDrugToggleValueChange(bool v)
    {
        if (v)
        {
            LoadData(ArticleType.Drug);
        }
    }
    public void OnBookToggleValueChange(bool v)
    {
        if (v)
        {
            LoadData(ArticleType.Book);
        }
    }
    #endregion
}
