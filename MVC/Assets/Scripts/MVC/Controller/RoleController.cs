using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleController : MonoBehaviour
{
    //能够在Controller中得到的界面才行
    private RoleView roleView;

    private static RoleController controller = null;

    public static RoleController Controller
    {
        get
        {
            return controller;
        }
    }

    public static void ShowMe()
    {
        if (controller == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象 为Canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            controller = obj.GetComponent<RoleController>();
        }
        controller.gameObject.SetActive(true);
    }

    public static void HideMe()
    {
        if (controller != null)
        {
            controller.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //获取同挂在一个对象上的view脚本
        roleView = this.GetComponent<RoleView>();
        //第一次 界面更新
        roleView.UpdateInfo(PlayerModel.Data);

        roleView.btnClose.onClick.AddListener(clickCloseBtn);
        roleView.btnLevUp.onClick.AddListener(ClickLevUpBtn);

        PlayerModel.Data.AddEventListener(UpdateInfo);
    }

    private void clickCloseBtn()
    {
        HideMe();
    }

    private void ClickLevUpBtn()
    {
        //通过数据模块 进行升级
        PlayerModel.Data.LevUp();
    }

    private void UpdateInfo(PlayerModel data)
    {
        if (roleView != null)
        {
            roleView.UpdateInfo(data);
        }
    }

    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }
}
