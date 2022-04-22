using PureMVC.Interfaces;
using PureMVC.Patterns.Command;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevUpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        base.Execute(notification);
        //得到数据代理 调用升级 升级完通知更新数据
        PlayerProxy playerProxy = Facade.RetrieveMediator(PlayerProxy.NAME) as PlayerProxy;
        
        if(playerProxy!=null)
        {
            //升级
            playerProxy.LevUp();
            playerProxy.SaveDate();
            //通知更新
            SendNotification(PureNotification.UPDATE_PLAYER_INFO, playerProxy.Data);
        }
    }
}
