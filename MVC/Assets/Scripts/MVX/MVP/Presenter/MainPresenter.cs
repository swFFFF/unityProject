using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainPresenter : MonoBehaviour
{
    //能够在Presenter中得到的界面才行
    private MVP_MainView mainView;

    private static MainPresenter presenter = null;

    public static MainPresenter Presenter
    {
        get
        {
            return presenter;
        }
    }

    //1、页面效果
    public static void ShowMe()
    {
        if (presenter == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/MainPanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象 为Canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            presenter = obj.GetComponent<MainPresenter>();
        }
        presenter.gameObject.SetActive(true);
    }

    public static void HideMe()
    {
        if (presenter != null)
        {
            presenter.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        //获取同挂在一个对象上的view脚本
        mainView = this.GetComponent<MVP_MainView>();
        //第一次 界面更新
        //mainView.UpdateInfo(PlayerModel.Data);
        //通过P自己的更新方法更新数据
        UpdateInfo(PlayerModel.Data);

        //2、界面 事件的监听 来处理对应的业务逻辑
        mainView.btnRole.onClick.AddListener(ClickRoleBtn);

        PlayerModel.Data.AddEventListener(UpdateInfo);
    }

    private void ClickRoleBtn()
    {
        Debug.Log("显示角色面板");
        //RoleController.ShowMe();
        RolePresenter.ShowMe();
    }
    //3、界面的更新
    private void UpdateInfo(PlayerModel data)
    {
        if (mainView != null)
        {
            //mainView.UpdateInfo(data);
            //MVC中是把数据M传到V中更新 现在全部由P来做
            mainView.txtName.text = data.PlayerName;
            mainView.txtLev.text = data.Lev.ToString();
            mainView.txtMoney.text = data.Money.ToString();
        }
    }

    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }
}
