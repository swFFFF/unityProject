using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 这是pureMVC中的 通知名类
/// 主要是用来申明各个通知的名字
/// 方便使用和管理
/// </summary>
public class PureNotification
{
    public const string START_UP = "startUp";
    public const string SHOW_PANEL = "showPanel";

    public const string HIDE_PANEL = "hidePanel";

    /// <summary>
    /// 代表玩家数据更新通知名
    /// </summary>
    public const string UPDATE_PLAYER_INFO = "updatePlayer";

    public const string LEV_UP = "levUp";
}
