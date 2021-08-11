using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public interface ILoopDataAdaptor
{
    //初始化数据
    void InitData(object[] t);

    //添加数据
    void AddData(object[] t);

    //获取头部数据
    object GetHeadData();

    //移除头部数据
    bool RemoveHeadData();

    //获取尾部数据
    object GetLastData();

    //移除尾部数据
    bool RemoveLastData();

}

