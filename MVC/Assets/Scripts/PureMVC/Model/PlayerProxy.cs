using PureMVC.Patterns.Proxy;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 玩家数据代理对象
/// 主要处理玩家数据相关的逻辑
/// </summary>
public class PlayerProxy : Proxy
{
    public new const string NAME = "PlayerProxy";
    //1、继承proxy父类
    //2、写我们的构造函数

    //写构造函数
    //重要点
    //1、代理名字（重要）
    //2、代理相关的数据
    //public PlayerProxy(PlayerProxy data):base(PlayerProxy.NAME, data)
    //{
    //    
    //}
    public PlayerProxy() : base(PlayerProxy.NAME)
    {
        //在构造函数里 初始化一个数据 进行关联
        PlayerDataObj data = new PlayerDataObj();

        //初始化
        data.playerName = PlayerPrefs.GetString("PlayerName", "Swift");
        data.lev = PlayerPrefs.GetInt("PlayerLev", 22);
        data.money = PlayerPrefs.GetInt("PlayerMoney", 9999);
        data.gem = PlayerPrefs.GetInt("PlayerGem", 8888);
        data.power = PlayerPrefs.GetInt("PlayerPower", 99);

        data.hp = PlayerPrefs.GetInt("PlayerHp", 100);
        data.atk = PlayerPrefs.GetInt("PlayerAtk", 20);
        data.def = PlayerPrefs.GetInt("PlayerDef", 10);
        data.crit = PlayerPrefs.GetInt("PlayerCrit", 20);
        data.miss = PlayerPrefs.GetInt("PlayerMiss", 10);
        data.luck = PlayerPrefs.GetInt("PlayerLuck", 40);


        //赋值给自己的Data进行关联
        Data = data;
    }

    //数据更新
    public void LevUp()
    {
        PlayerDataObj data = Data as PlayerDataObj;
        //升级 更改内容
        data.lev += 1;
        data.hp += data.lev;
        data.atk += data.lev;
        data.def += data.lev;
        data.crit += data.lev;
        data.miss += data.lev;
        data.luck += data.lev;
    }

    //保存
    public void SaveDate()
    {
        PlayerDataObj data = Data as PlayerDataObj;
        //把这些数据内容保存到本地
        PlayerPrefs.SetString("PlayerName", data.playerName);
        PlayerPrefs.SetInt("PlayerLev", data.lev);
        PlayerPrefs.SetInt("PlayerMoney", data.money);
        PlayerPrefs.SetInt("PlayerGem", data.gem);
        PlayerPrefs.SetInt("PlayerPower", data.power);

        PlayerPrefs.SetInt("PlayerHp", data.hp);
        PlayerPrefs.SetInt("PlayerAtk", data.atk);
        PlayerPrefs.SetInt("PlayerDef", data.def);
        PlayerPrefs.SetInt("PlayerCrit", data.crit);
        PlayerPrefs.SetInt("PlayerMiss", data.miss);
        PlayerPrefs.SetInt("PlayerLuck", data.luck);
    }
}
