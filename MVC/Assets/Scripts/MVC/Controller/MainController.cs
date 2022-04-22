using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 处理业务逻辑
/// </summary>
public class MainController : MonoBehaviour
{
    //能够在Controller中得到的界面才行
    private MainView mainView;

    private static MainController controller = null;

    public static MainController Controller
    {
        get
        {
            return controller;
        }
    }

    //1、页面效果
    public static void ShowMe()
    {
        if(controller == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/MainPanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象 为Canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            controller = obj.GetComponent<MainController>();
        }
        controller.gameObject.SetActive(true);
    }

    public static void HideMe()
    {
        if(controller != null)
        {
            controller.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //获取同挂在一个对象上的view脚本
        mainView = this.GetComponent<MainView>();
        //第一次 界面更新
        mainView.UpdateInfo(PlayerModel.Data);

        //2、界面 事件的监听 来处理对应的业务逻辑
        mainView.btnRole.onClick.AddListener(ClickRoleBtn);

        PlayerModel.Data.AddEventListener(UpdateInfo);
    }

    private void ClickRoleBtn()
    {
        Debug.Log("显示角色面板");
        RoleController.ShowMe();
    }
    //3、界面的更新
    private void UpdateInfo(PlayerModel data)
    {
        if(mainView != null)
        {
            mainView.UpdateInfo(data);
        }
    }

    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }
}
