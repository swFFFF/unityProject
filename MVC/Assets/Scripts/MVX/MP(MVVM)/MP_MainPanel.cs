using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_MainPanel : BasePanel
{
    //1、找控件 通过集成小框架中的 UI基类实现了
    //2、逻辑处理 
    //3、数据更新

    private void Start()
    {
        UpdateInfo(PlayerModel.Data);

        PlayerModel.Data.AddEventListener(UpdateInfo);
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch (btnName)
        {
            case "btnRole":
                //处理角色面板打开
                UIManager.GetInstance().ShowPanel<MP_RolePanel>("RolePanel");
                break;
        }
    }

    public void UpdateInfo(PlayerModel data)
    {
        //直接在这获取控件 更新
        GetControl<Text>("txtName").text = data.PlayerName;
        GetControl<Text>("txtLev").text = "LV." + data.Lev.ToString();
    }

    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }
}
