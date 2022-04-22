using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MVP_MainView : MonoBehaviour
{
    //1、找控件
    public Button btnRole;
    public Button btnSkill;

    public Text txtName;
    public Text txtLev;
    public Text txtMoney;
    public Text txtGem;
    public Text txtPower;
    //2、提供面板更新的相关方法给外部 可以在P里访问控件修改
    //public void UpdateInfo(string name, int lev, int money, int gem, int power)
    //{
    //    txtName.text = name;
    //    txtLev.text = "LV." + lev;
    //    txtMoney.text = money.ToString();
    //}
}
