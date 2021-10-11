using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArticleInformation : ViewBase
{
    private RectTransform rectInfo;
    private Text text;
    public float offSet = 0.15f;

    private Vector3[] infoCorners;
    private Vector3[] screenCorners;

    private void Awake()
    {
        infoCorners = new Vector3[4];
        screenCorners = new Vector3[4];
        rectInfo = transform.Find("Info").GetComponent<RectTransform>();
        text = rectInfo.GetComponentInChildren<Text>();
        Hide();
    }

    private void Update()
    {
        //矩形框锚点和鼠标初始点重合
        rectInfo.anchoredPosition = Input.mousePosition;
    }

    private void LateUpdate()
    {
        LinstenerCorners();
    }

    public override void Show()
    {
        base.Show();
        rectInfo.pivot = new Vector2(-offSet, 1 + offSet);
    }

    public void LinstenerCorners()
    {
        rectInfo.GetWorldCorners(infoCorners);
        transform.GetComponent<RectTransform>().GetWorldCorners(screenCorners);

        Vector2 pivot = rectInfo.pivot;

        if(infoCorners[0].x < screenCorners[0].x)
        {
            //左边超出边界
            pivot.x = -offSet;
        }

        if(infoCorners[3].x > screenCorners[3].x)
        {
            //右边超出边界
            pivot.x = 1 + offSet;
        }

        if(infoCorners[1].y > screenCorners[1].y)
        {
            //上面超出边界
            pivot.y = 1 + offSet;
        }

        if(infoCorners[0].y < screenCorners[0].y)
        {
            //下面超出边界
            pivot.y = -offSet;
        }

        rectInfo.pivot = pivot;
    }

    public void SetShowInfo(string info)
    {
        if (text != null)
        {
            text.text = info;
        }
    }
}
