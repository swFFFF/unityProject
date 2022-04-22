using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MVP_RoleView : MonoBehaviour
{
    //1、找控件
    public Button btnClose;
    public Button btnLevUp;

    public Text txtLev;
    public Text txtHp;
    //2、提供面板更新的相关方法给外部 可以在P里访问控件修改
}
