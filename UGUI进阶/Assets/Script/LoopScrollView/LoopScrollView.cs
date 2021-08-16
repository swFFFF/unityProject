using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LoopScrollViewType
{
    Horizontal,
    Vertical,
}
public class LoopScrollView : MonoBehaviour
{
    #region 字段
    //子物体的预制体
    public GameObject childItemPrefab;

    public LoopScrollViewType scrollViewType = LoopScrollViewType.Vertical;

    //动态添加子物体时不需要使用grid layout group
    private GridLayoutGroup contentLayoutGroup;

    private ContentSizeFitter sizeFitter;
    private RectTransform content;
    //接口对象
    public ILoopDataAdaptor dataAdaptor;
    public ISetLoopItemData setLoopItemData;
    #endregion

    #region 事件
    public Action onMoveDataEnd;
    #endregion

    #region Unity回调
    private void Awake()
    {
        Init();
    }
    #endregion

    #region 方法
    private void Init()
    {
        content = transform.Find("Viewport/Content").GetComponent<RectTransform>();
        if (content == null)
        {
            throw new System.Exception("找不到content");
        }
        contentLayoutGroup = content.GetComponentInParent<GridLayoutGroup>();
        if (contentLayoutGroup == null)
        {
            throw new System.Exception("找不到GridLayoutGroup");
        }
        sizeFitter = content.GetComponentInParent<ContentSizeFitter>();
        if (sizeFitter == null)
        {
            throw new System.Exception("找不到ContentSizeFitter");
        }

        //优先从自身获取
        dataAdaptor = transform.GetComponent<ILoopDataAdaptor>();
        if (dataAdaptor == null)
        {
            //如果没有获取到 使用默认数据
            dataAdaptor = new DataAdaptor();
        }

        setLoopItemData = transform.GetComponent<ISetLoopItemData>();
        if(setLoopItemData == null)
        {
            throw new System.Exception("未实现 设置数据接口");
        }
        //-----------------测试数据--------------
        //List<LoopDataItem> loopDataItems = new List<LoopDataItem>();
        //for (int i = 0; i < 100; i++)
        //{
        //    loopDataItems.Add(new LoopDataItem(i));
        //}
        //dataAdaptor.InitData(loopDataItems.ToArray());
        //------------------------------------------
    }

    //初始化数据
    public void InitData(object[] datas, GameObject childItem)
    {
        if(childItem != null)
        {
            this.childItemPrefab = childItem;
        }
        contentLayoutGroup.enabled = true;
        sizeFitter.enabled = true;
        //隐藏所有子节点
        HideAllChild();

        //初始化数据
        dataAdaptor.InitData(datas);

        //添加一个子节点
        OnAddHead();
        //禁用
        Invoke("EnableFalseGrid", 0.1f);
    }

    //添加数据
    public void AddData(object[] datas)
    {
        dataAdaptor.AddData(datas);
    }

    //获取一个子节点
    private GameObject GetChildItem()
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

        loopItem.SetLoopScrollViewType(this.scrollViewType);
        return childItem;
    }

    //在上面再添加一个物体
    private void OnAddHead()
    {
        object loopDataItem = dataAdaptor.GetHeadData();
        if (loopDataItem != null)
        {
            Transform first = FindFirst();

            GameObject gameObject = GetChildItem();
            gameObject.transform.SetAsFirstSibling();
            //设置数据
            setLoopItemData.SetData(gameObject, loopDataItem);
            //动态的设置位置
            if (first != null)
            {
                switch (scrollViewType)
                {
                    case LoopScrollViewType.Horizontal:
                        gameObject.transform.localPosition = first.localPosition - new Vector3(contentLayoutGroup.cellSize.x + contentLayoutGroup.spacing.x, 0, 0);
                        break;
                    case LoopScrollViewType.Vertical:
                        gameObject.transform.localPosition = first.localPosition + new Vector3(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y, 0);
                        break;
                    default:
                        break;
                }

            }
        }
    }

    //移除顶部物体
    private void OnRemoveHead()
    {
        if (dataAdaptor.RemoveHeadData())
        {
            Transform first = FindFirst();
            if (first != null)
            {
                first.gameObject.SetActive(false);
            }
        }

    }

    //添加底部物体
    private void OnAddLast()
    {
        object loopDataItem = dataAdaptor.GetLastData();
        if (loopDataItem != null)
        {
            Transform last = FindLast();

            GameObject gameObject = GetChildItem();
            gameObject.transform.SetAsLastSibling();
            //设置数据
            setLoopItemData.SetData(gameObject, loopDataItem);

            switch (scrollViewType)
            {
                case LoopScrollViewType.Horizontal:
                    //动态的设置位置
                    if (last != null)
                    {
                        gameObject.transform.localPosition = last.localPosition + new Vector3(contentLayoutGroup.cellSize.x + contentLayoutGroup.spacing.x, 0, 0);
                    }

                    //是否要增加高度
                    if (IsNeedAddContentHeight(gameObject.transform))
                    {
                        content.sizeDelta += new Vector2(contentLayoutGroup.cellSize.x + contentLayoutGroup.spacing.x, 0);
                    }
                    break;
                case LoopScrollViewType.Vertical:
                    //动态的设置位置
                    if (last != null)
                    {
                        gameObject.transform.localPosition = last.localPosition - new Vector3(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y, 0);
                    }

                    //是否要增加高度
                    if (IsNeedAddContentHeight(gameObject.transform))
                    {
                        content.sizeDelta += new Vector2(0, contentLayoutGroup.cellSize.y + contentLayoutGroup.spacing.y);
                    }
                    break;
                default:
                    break;
            }
        }
        else
        {
            //没有找到数据
            if(onMoveDataEnd != null)
            {
                onMoveDataEnd();
            }
        }
    }

    //删除底部物体
    private void OnRemoveLast()
    {
        if (dataAdaptor.RemoveLastData())
        {
            Transform last = FindLast();
            if (last != null)
            {
                last.gameObject.SetActive(false);
            }
        }
    }

    private Transform FindFirst()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            if (content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    private Transform FindLast()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
            if (content.GetChild(i).gameObject.activeSelf)
            {
                return content.GetChild(i);
            }
        }
        return null;
    }

    private void EnableFalseGrid()
    {
        contentLayoutGroup.enabled = false;
        sizeFitter.enabled = false;
    }

    //是否要增加content高度
    private bool IsNeedAddContentHeight(Transform trans)
    {
        Vector3[] rectCorners = new Vector3[4];
        Vector3[] contentCorners = new Vector3[4];
        trans.GetComponent<RectTransform>().GetWorldCorners(rectCorners);
        content.GetWorldCorners(contentCorners);

        switch (scrollViewType)
        {
            case LoopScrollViewType.Horizontal:
                if (rectCorners[3].x > contentCorners[3].x)
                {
                    return true;
                }
                break;
            case LoopScrollViewType.Vertical:
                if (rectCorners[0].y < contentCorners[0].y)
                {
                    return true;
                }
                break;
            default:
                break;
        }

        return false;

    }

    //public void SetData(GameObject childItem, object data)
    //{
    //    childItem.transform.Find("Text").GetComponent<Text>().text = ((LoopDataItem)data).id.ToString();
    //}

    //隐藏所有子节点 （回收所有子节点）
    private void HideAllChild()
    {
        for(int i =0; i < content.childCount; i++)
        {
            content.GetChild(i).gameObject.SetActive(false);
        }
    }
    #endregion
}
