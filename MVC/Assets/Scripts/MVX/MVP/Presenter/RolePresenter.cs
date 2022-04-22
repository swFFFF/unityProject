using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RolePresenter : MonoBehaviour
{
    //能够在Controller中得到的界面才行
    private MVP_RoleView roleView;

    private static RolePresenter presenter = null;

    public static RolePresenter Presenter
    {
        get
        {
            return presenter;
        }
    }

    public static void ShowMe()
    {
        if (presenter == null)
        {
            //实例化面板对象
            GameObject res = Resources.Load<GameObject>("UI/RolePanel");
            GameObject obj = Instantiate(res);
            //设置它的父对象 为Canvas
            obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
            presenter = obj.GetComponent<RolePresenter>();
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
        roleView = this.GetComponent<MVP_RoleView>();
        //第一次 界面更新
        //roleView.UpdateInfo(PlayerModel.Data);
        UpdateInfo(PlayerModel.Data);

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
            //roleView.UpdateInfo(data);
            roleView.txtLev.text = "LV." + data.Lev;
            roleView.txtHp.text = data.Hp.ToString();
        }
    }

    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }
}
