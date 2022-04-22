using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PureMVC.Patterns.Command;
using PureMVC.Interfaces;

public class StartUpCommand : SimpleCommand
{
    //1、继承Command相关脚本
    //2、重写里面的执行函数
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        //当命令被执行时 就会调用该方法
        //启动命令中 往往是做一些初始化操作

        if(!Facade.HasProxy(PlayerProxy.NAME))
        {
            Facade.RegisterProxy(new PlayerProxy());
        }
    }
}
