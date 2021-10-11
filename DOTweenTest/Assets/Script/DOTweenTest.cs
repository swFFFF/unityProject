using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//https://juejin.cn/post/6981236475983577124#heading-15
public class DOTweenTest : MonoBehaviour
{
    public Vector3 value = new Vector3(0, 0, 0);
    public Text text;
    public GameObject sphere;
    private void Awake()
    {
        
    }

    private void Start()
    {
        DOTween.defaultAutoPlay = AutoPlay.None;
        //改变值
        DOTween.To(() => value, x => value = x, new Vector3(10, 10, 0), 2);
        //动画
        transform.DOMove(Vector3.forward.normalized * 10, 10).SetEase(Ease.OutQuint).SetLoops(3).From(true)
            .OnStart(() => { Debug.Log("开始"); }).OnComplete(() => { Debug.Log("结束"); });

        transform.DOScale(new Vector3(2, 2, 2), 5);
        transform.GetComponent<Renderer>().material.DOColor(Color.red, 5);

        //设置id，直接调用缓存里的tween，SetId
        //transform.DOMove(Vector3.one, 2).SetId("MYID"); 
        //DOTween.Play("MYID");
        // 是否可回收SetRecyclable
        // transform.DOMove(Vector3.one, 2).SetRecyclable(true); 
        //增量运动SetRelative
        //transform.DOMove(Vector3.one, 2).SetRelative(true);
        if (sphere != null)
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append(sphere.transform.DOMove(new Vector3(0, 10, 0), 2));
            sequence.Append(sphere.transform.DOScale(new Vector3(2, 2, 2), 5));
            sequence.AppendCallback(OnStop);
        }


        Camera.main.transform.DOShakePosition(2, 2.0f);

        //文字打印
        if(text != null)
        {
            text.DOText("显示了个啥紫马", 2);
        }

       
        //Invoke("OnStop",5);
    }

    private void Update()
    {

    }

    private void OnStop()
    {
        //transform.DOKill();
        Debug.Log("callback");
    }
}
