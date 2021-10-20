using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 缓存池模块
/// 1.Dictionary List
/// 2.GameObjeche 和 Resources 的API
/// </summary>
public class PoolManager : BaseManager<PoolManager>
{
    public Dictionary<string, List<GameObject>> poolDict = new Dictionary<string, List<GameObject>>();
    /// <summary>
    /// 取出
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>

    public GameObject GetObj(string name)
    {
        GameObject obj = null;
        if(poolDict.ContainsKey(name) && poolDict[name].Count > 0)
        {
            obj = poolDict[name][0];
            poolDict[name].RemoveAt(0);
        }
        else
        {
            obj = GameObject.Instantiate(Resources.Load<GameObject>(name));
            obj.name = name;
            PlayerPrefs.SetInt(obj.name, 1);
            Debug.Log(PlayerPrefs.GetInt(obj.name, 0));
        }
        obj.SetActive(true);
        return obj;
    }

    /// <summary>
    /// 还给
    /// </summary>
    /// <param name="name"></param> 抽屉
    /// <param name="obj"></param>  物体
    public void PushObj(string name, GameObject obj)
    {
        obj.SetActive(false);
        if( poolDict.ContainsKey(name))
        {
            poolDict[name].Add(obj);
        }
        else
        {
            poolDict.Add(name, new List<GameObject>());
        }
    }
}
