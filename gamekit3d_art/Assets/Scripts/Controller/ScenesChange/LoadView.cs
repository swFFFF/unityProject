using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadView : ViewBase
{
    #region 属性
    //public static LoadView Instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            GameObject obj = new GameObject("LoadView");
    //            obj.AddComponent<LoadView>();
    //        }
    //        return _instance;
    //    }
    //}
    #endregion

    #region 字段
    //private static LoadView _instance;
    public Slider slider_progress;
    public Text text_progress;
    #endregion

    #region Unity回调
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        //if (_instance != null)
        //{
        //    throw new Exception("场景中存在多个LoadView");
        //}
        //_instance = this;
    }
    #endregion

    #region 方法
    public void UpdateProgress(float progress)
    {
        slider_progress.value = progress;
    }

    public void OnSliderProgressValueChange(float v)
    {
        text_progress.text = string.Format("{0}%", Mathf.Round(v * 100));
    }
    #endregion
}
