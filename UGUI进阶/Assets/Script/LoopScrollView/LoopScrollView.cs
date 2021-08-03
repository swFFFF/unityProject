using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoopScrollView : MonoBehaviour
{
    #region 字段
    //子物体的预制体
    public GameObject childItemPrefab;
    //动态添加子物体时不需要使用grid layout group

    private GridLayoutGroup contentLayoutGroup;

    private ContentSizeFitter sizeFitter;
    private RectTransform content;
    #endregion

    #region Unity回调
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        contentLayoutGroup.enabled = true;
        sizeFitter.enabled = true;
        //添加一个子节点
        OnAddHead();
        //禁用
        Invoke("EnableFalseGrid", 0.1f);
    }
    #endregion

    #region 方法
    public void Init()
    {
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        if (content == null)
        {
            throw new System.Exception("找不到content");
        }
        contentLayoutGroup = content.GetComponentInParent<GridLayoutGroup>();
        if(contentLayoutGroup == null)
        {
            throw new System.Exception("找不到GridLayoutGroup");
        }
        sizeFitter = content.GetComponentInParent<ContentSizeFitter>();
        if (sizeFitter == null)
        {
            throw new System.Exception("找不到ContentSizeFitter");
        }
    }

    //获取一个子节点
    public GameObject GetChildItem()
    {
        //查找需要回收的子节点
        for (int i = 0; i < content.childCount; i++) 
        {
            if (!content.GetChild(i).gameObject.activeSelf) //item不可见
            {
                content.GetChild(i).gameObject.SetActive(true);
                return content.GetChild(i).gameObject;
            }
        }

        //没找到 创建一个
        GameObject childItem = GameObject.Instantiate(childItemPrefab, content.transform);
        //设置数据 保证实例化大小和预制体一致
        childItem.transform.localScale = Vector3.one;
        childItem.transform.localPosition = Vector3.zero;
        //设置锚点
        childItem.GetComponent<RectTransform>().anchorMin = new Vector2(0, 1);
        childItem.GetComponent<RectTransform>().anchorMax = new Vector2(0, 1);

        //设置宽高
        childItem.GetComponent<RectTransform>().sizeDelta = contentLayoutGroup.cellSize;//布局中每个单元格大小

        //加上LoopItem组件
        LoopItem loopItem = childItem.AddComponent<LoopItem>();
        loopItem.onAddHead += this.OnAddHead;//加入委托
        loopItem.onRemoveHead += this.OnRemoveHead;
        loopItem.onAddLast += this.OnAddLast;
        loopItem.onRemoveLast += this.OnRemoveLast;
        return childItem;
    }

    //在上面再添加一个物体
    public void OnAddHead()
    {
        Transform first = FindFirst();

        GameObject gameObject = GetChildItem();
        gameObject.transform.SetAsFirstSibling();
        //动态的设置位置
        if(first != null)
        {
            gameObject.transform.localPosition = first.localPosition + new Vector3(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y, 0);
        }
    }

    //移除顶部物体
    public void OnRemoveHead()
    {
        Transform first = FindFirst();
        if(first != null)
        {
            first.gameObject.SetActive(false);
        }
    }

    //添加底部物体
    public void OnAddLast()
    {
        Transform last = FindLast();

        GameObject gameObject = GetChildItem();
        gameObject.transform.SetAsLastSibling();
        //动态的设置位置
        if (last != null)
        {
            gameObject.transform.localPosition = last.localPosition - new Vector3(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y, 0);
        }

        //是否要增加高度
        if(IsNeedAddContentHeight(gameObject.transform))
        {
            content.sizeDelta += new Vector2(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y);
        }
    }

    //删除底部物体
    public void OnRemoveLast()
    {
        Transform last = FindLast();
        if (last != null)
        {
            last.gameObject.SetActive(false);
        }
    }

    public Transform FindFirst()
    {
        for(int i = 0; i < content.childCount; i++)
        {
            if( content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    public Transform FindLast()
    {
        for (int i = content.childCount - 1; i >= 0; i--) 
        {
            if(content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    public void EnableFalseGrid()
    {
        contentLayoutGroup.enabled = false;
        sizeFitter.enabled = false;
    }

    //是否要增加content高度
    public bool IsNeedAddContentHeight(Transform trans)
    {
        Vector3[] rectCorners = new Vector3[4];
        Vector3[] contentCorners = new Vector3[4];
        trans.GetComponent<RectTransform>().GetWorldCorners(rectCorners);
        content.GetWorldCorners(contentCorners);

        if(rectCorners[0].y < contentCorners[0].y)
        {
            return true;
        }
        return false;

    }
    #endregion
}
