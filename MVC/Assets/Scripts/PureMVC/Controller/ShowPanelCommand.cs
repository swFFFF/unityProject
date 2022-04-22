using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);

        //写面板创建的逻辑
        string panelName = notification.Body.ToString();

        switch (panelName)
        {
            case "MainPanel":
                //显示面板相关内容
                //如果要使用Mediator 一定也要在Facade中注册
                //command、proxy都一样 要使用就要先注册
                //可以在命令中 直接使用Facade代表的就是唯一的Facade

                //判断如果没有mediator就去new一个
                if(!Facade.HasMediator(NewMainViewMediator.NAME))
                {
                    Facade.RegisterMediator(new NewMainViewMediator());
                }
                //有Mediator后 再去创建预设体（界面）

                //Facade中得到Mediator的方法
                NewMainViewMediator mm = Facade.RetrieveMediator(NewMainViewMediator.NAME) as NewMainViewMediator;
                if(mm.ViewComponent == null)
                {
                    //实例化面板对象
                    GameObject res = Resources.Load<GameObject>("UI/MainPanel");
                    GameObject obj = GameObject.Instantiate(res);
                    //设置它的父对象 为Canvas
                    obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    //得到预设体上的view脚本关联到mediator上
                    mm.SetView(obj.GetComponent<NewMainView>());
                }
                //往往显示面板后 做一次界面更新
                SendNotification(PureNotification.UPDATE_PLAYER_INFO, Facade.RetrieveProxy(PlayerProxy.NAME).Data);

                break;
            case "RolePanel":
                //显示面板相关内容
                if (!Facade.HasMediator(NewRollViewMediator.NAME))
                {
                    Facade.RegisterMediator(new NewRollViewMediator());
                }
                //有Mediator后 再去创建预设体（界面）

                //Facade中得到Mediator的方法
                NewRollViewMediator mm1 = Facade.RetrieveMediator(NewRollViewMediator.NAME) as NewRollViewMediator;
                if (mm1.ViewComponent == null)
                {
                    //实例化面板对象
                    GameObject res = Resources.Load<GameObject>("UI/RolePanel");
                    GameObject obj = GameObject.Instantiate(res);
                    //设置它的父对象 为Canvas
                    obj.transform.SetParent(GameObject.Find("Canvas").transform, false);
                    //得到预设体上的view脚本关联到mediator上
                    mm1.SetView(obj.GetComponent<NewRollView>());
                }
                //往往显示面板后 做一次界面更新
                SendNotification(PureNotification.UPDATE_PLAYER_INFO, Facade.RetrieveProxy(PlayerProxy.NAME).Data);

                break;
            default:
                break;
        }
    }
}
