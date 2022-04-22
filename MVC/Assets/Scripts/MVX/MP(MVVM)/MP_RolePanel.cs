using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MP_RolePanel : BasePanel
{
    void Start()
    {
        //第一次显示 更新面板
        UpdateInfo(PlayerModel.Data);

        PlayerModel.Data.AddEventListener(UpdateInfo);
    }

    protected override void OnClick(string btnName)
    {
        base.OnClick(btnName);
        switch(btnName)
        {
            case "btnClose":
                UIManager.GetInstance().HidePanel("RolePanel");
                break;
            case "btnLevUp":
                PlayerModel.Data.LevUp();
                break;
        }
    }

    public void UpdateInfo(PlayerModel data)
    {
        GetControl<Text>("txtLev").text = "LV." + data.Lev;
        GetControl<Text>("txtHp").text = data.Hp.ToString();
    }

    private void OnDestroy()
    {
        PlayerModel.Data.RemoveEventListener(UpdateInfo);
    }
}
