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

    public GridLayoutGroup contentLayoutGroup;

    public ContentSizeFitter sizeFitter;
    public RectTransform content;
    #endregion

    #region Unity回调
    private void Awake()
    {
        Init();
    }

    private void Start()
    {
        //添加一个子节点

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

    }

    //移除顶部物体
    public void OnRemoveHead()
    {

    }

    //添加底部物体
    public void OnAddLast()
    {

    }

    //删除底部物体
    public void OnRemoveLast()
    {

    }

    #endregion
}
