using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataAdaptor : ILoopDataAdaptor
{
    //default 数值类型返回0 引用类型返回null
    #region 字段
    //保存所有数据
    public List<object> allData = new List<object>();
    //当前显示数据
    public LinkedList<object> currentShowData = new LinkedList<object>();
    #endregion

    #region 方法
    //添加currentShowData 第一个数据
    public object GetHeadData()
    {
        //总数据为空
        if(allData.Count == 0)
        {
            return null;
        }
        //当前没有显示数据 特殊情况
        if(currentShowData.Count == 0)
        {
            object head = allData[0];
            currentShowData.AddFirst(head);
            return head;
        }

        //获取当前显示第一个数据
        object t = currentShowData.First.Value;
        //找到数据的下标第一个数据在所有数据中的下标
        int index = allData.IndexOf(t);
        if(index != 0)
        {
            //获取头数据的上一个数据
            object header = allData[index - 1];
            //加入当前显示数据里
            currentShowData.AddFirst(header);
            return header;
        }
        return null;
    }

    //移除currentShowData 第一个数据
    public bool RemoveHeadData()
    {
        if(currentShowData.Count == 0 || currentShowData.Count == 1)
        {
            return false;
        }
        currentShowData.RemoveFirst();
        return true;
    }

    //添加currentShowData 最后一个数据
    public object GetLastData()
    {
        //总数据为空
        if (allData.Count == 0)
        {
            return null;
        }
        //当前没有显示数据 特殊情况
        if (currentShowData.Count == 0)
        {
            object las = allData[0];
            currentShowData.AddLast(las);
            return las;
        }

        //显示数据中的最后一个数据
        object last = currentShowData.Last.Value;
        //最后一个数据下标
        int index = allData.IndexOf(last);
        //还未显示到最后一个数据
        if (index != allData.Count - 1)
        {
            object now_last = allData[index + 1];
            currentShowData.AddLast(now_last);
            return now_last;
        }    
        return null;
    }

    //移除currentShowData 最后一个数据
    public bool RemoveLastData()
    {
        //移除currentShowData 移除当前最后一个数据
        if(currentShowData.Count == 0 || currentShowData.Count == 1)
        {
            return false;
        }
        currentShowData.RemoveLast();
        return true;
    }
    #endregion

    #region 数据管理
    public void InitData(object[] t)
    {
        //情况容器
        allData.Clear();
        currentShowData.Clear();
        //添加到数组尾
        allData.AddRange(t);
    }

    public void InitData(List<object> t)
    {
        InitData(t.ToArray());
    }

    public void AddData(object[] t)
    {
        allData.AddRange(t);
    }

    public void AddData(List<object> t)
    {
        AddData(t.ToArray());
    }
    #endregion
}
