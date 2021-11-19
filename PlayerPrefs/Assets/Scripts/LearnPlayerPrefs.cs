using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LearnPlayerPrefs : MonoBehaviour
{
    void Start()
    {
        #region PlayerPrefs是啥
        //是Unity提供的可以用于存储读取玩家数据的公共类
        #endregion

        #region 存储相关
        //PlayerPrefs的数据存储 类似键值对存储
        //键：string类型
        //值：int string float对应3中API
        PlayerPrefs.SetInt("age", 18);
        PlayerPrefs.SetFloat("Height", 175.0f);
        PlayerPrefs.SetString("Name", "施文峰");

        //直接调用Set相关方法 只会把数据存到内存中
        //当游戏结束时 Unity会自动把数据存到硬盘
        //如果游戏不正常退出 数据会丢失
        //只要调用该方法 就会马上存储到硬盘
        PlayerPrefs.Save();

        //PlayerPrefs是局限性 只能存三中类型的数据
        //如果想存储别的类型数据 只能改变精度存储（）
        #endregion

        #region 读取相关
        //运行时 Set后即使没有Save本地也能读取出信息
        //int
        int height = PlayerPrefs.GetInt("Height");
        print(height);
        height = PlayerPrefs.GetInt("Height", 180);
        print(height);

        //第二参数 默认值
        //获取不到数据时的初始值

        //判断数据是否存在
        if(PlayerPrefs.HasKey("Name"))
        {
            print("存在Name数据");
        }
        #endregion

        #region 删除数据
        //删除指定键值对
        PlayerPrefs.DeleteKey("age");
        //删除所有存储信息
        PlayerPrefs.DeleteAll();
        #endregion

        #region PlayerPrefs存储位置
        //不同平台存储位置不同
        #region windows
        //HKCU\Software\[公司名称]\[产品名称] 项下的注册表中
        //公司名称和产品名称在“Project Settings”中设置

        //运行regedit
        //HKEY_CURRENT_USER
        //SOFTWARE
        //Unity
        //UnityEditor
        //公司名称
        //产品名称
        #endregion

        #region Android
        // /data/data/包名/shared_prefs/pkg-name.xml
        #endregion

        #region IOS
        // /Library/Preferences/[应用ID].plist
        #endregion
        #endregion

        #region PlayerPrefs数据唯一性
        //PlayerPrefs唯一性由key决定
        //使用同样的key会覆盖原有数据
        #endregion
    }
    void Update()
    {
        
    }
}
