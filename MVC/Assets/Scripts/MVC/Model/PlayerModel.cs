using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerModel
{
    //数据内容
    private string playerName;
    public string PlayerName
    {
        get
        {
            return playerName;
        }
    }
    private int lev;
    public int Lev
    {
        get
        {
            return lev;
        }
    }
    private int money;
    public int Money
    {
        get
        {
            return money;
        }
    }
    private int power;

    private int hp;
    public int Hp
    {
        get
        {
            return hp;
        }
    }
    private int atk;
    private int def;
    private int crit;
    private int miss;
    private int luck;

    private event UnityAction<PlayerModel> updateEvent;

    //设置为单例模式保证数据唯一性
    private static PlayerModel data = null;

    public static PlayerModel Data
    {
        get
        {
            if(data == null)
            {
                data = new PlayerModel();
                data.Init();
            }
            return data;
        }
    }
    //数据相关操作
    //初始化
    public void Init()
    {
        playerName = PlayerPrefs.GetString("playerName", "swift");
        lev = PlayerPrefs.GetInt("lev", 1);
        money = PlayerPrefs.GetInt("money", 0);
        power = PlayerPrefs.GetInt("power", 1);
        hp = PlayerPrefs.GetInt("hp", 1);
        atk = PlayerPrefs.GetInt("atk", 1);
        def = PlayerPrefs.GetInt("def", 1);
        crit = PlayerPrefs.GetInt("crit", 1);
        miss = PlayerPrefs.GetInt("miss", 1);
        luck = PlayerPrefs.GetInt("luck", 1);
    }
    //数据更新
    public void LevUp()
    {
        //升级 更改内容
        lev += 1;
        hp += lev;
        atk += lev;
        def += lev;
        crit += lev;
        miss += lev;
        luck += lev;

        SaveDate();
    }
    //保存
    public void SaveDate()
    {
        //把这些数据内容保存到本地
        PlayerPrefs.SetString("playerName", playerName);
        PlayerPrefs.GetInt("lev", lev);
        PlayerPrefs.GetInt("money", money);
        PlayerPrefs.GetInt("power", power);
        PlayerPrefs.GetInt("hp", hp);
        PlayerPrefs.GetInt("atk", atk);
        PlayerPrefs.GetInt("def", def);
        PlayerPrefs.GetInt("crit", crit);
        PlayerPrefs.GetInt("miss", miss);
        PlayerPrefs.GetInt("luck", luck);
        UpdateInfo();
    }

    public void AddEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent += function;
    }

    public void RemoveEventListener(UnityAction<PlayerModel> function)
    {
        updateEvent -= function;
    }

    //通知外部更新数据的方法
    private void UpdateInfo()
    {
        //找到对应的 使用
        if(updateEvent != null)
        {
            updateEvent(this);
        }
    }
}
