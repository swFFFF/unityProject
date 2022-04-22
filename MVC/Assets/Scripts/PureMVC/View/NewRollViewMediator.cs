using PureMVC.Interfaces;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewRollViewMediator : Mediator
{
    public static new string NAME = "NewRoleViewMediator";
    //套路写法
    //1、继承PureMVC中的Mediator脚本
    //2、写构造函数
    public NewRollViewMediator() : base(NewRollViewMediator.NAME)
    {
        //这里面可以去创建界面与预设体等逻辑
        //但是界面显示应该是触发的控制的
        //而且创建界面的代发 重复性比较高
    }

    public void SetView(NewRollView view)
    {
        ViewComponent = view;
        view.btnClose.onClick.AddListener(() =>
        {
            SendNotification(PureNotification.HIDE_PANEL, this);
        });

        //升级按钮监听
        view.btnLevUp.onClick.AddListener(()=>
        {
            //去升级
            //去通知升级
            SendNotification(PureNotification.LEV_UP);
        });
    }

    //3、重写监听通知的方法
    public override string[] ListNotificationInterests()
    {
        //这里是PureMVC的规则
        //需要监听哪些通知 就在这吧通知们通过字符串数组的形式返回出去
        //PureMVC就会帮助我们监听这些通知
        //类似于 通过事件名 注册事件监听
        return new string[]
        {
            PureNotification.UPDATE_PLAYER_INFO,
            //关心别的通知写在下面
        };
    }
    //4、重写处理通知的方法
    public override void HandleNotification(INotification notification)
    {
        //INotification 对象里面包含两个对我们来说重要的参数
        //1、通知名 我们根据这个名字 来做对应的处理
        //2、通知包含的信息
        switch (notification.Name)
        {
            case PureNotification.UPDATE_PLAYER_INFO:
                //收到更新通知时做处理
                if (ViewComponent != null)
                {
                    (ViewComponent as NewRollView).UpdateInfo(notification.Body as PlayerDataObj);
                }
                break;
            default:
                break;
        }
    }
    //5、可选：重写注册时的方法
    public override void OnRegister()
    {
        base.OnRegister();
    }
}
