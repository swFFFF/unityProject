using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using PureMVC.Patterns.Mediator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanelCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        //隐藏目的
        //得到mediator 再得到 mediator中的view 然后删除或者隐藏
        //得到传入的mediator
        Mediator m = notification.Body as Mediator;

        if (m != null && m.ViewComponent != null) 
        {
            GameObject.Destroy((m.ViewComponent as MonoBehaviour).gameObject);
            //置空
            m.ViewComponent = null;
        }
    }
}
