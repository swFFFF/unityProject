using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewRollView : MonoBehaviour
{
    //1、找控件
    public Button btnClose;
    public Button btnLevUp;

    public Text txtLev;
    public Text txtHp;

    //2、提供面板更新的方法给外部
    public void UpdateInfo(PlayerDataObj data)
    {
        txtLev.text = "LV." + data.lev;
        txtHp.text = data.hp.ToString();
    }
}
