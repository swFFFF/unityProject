using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : ViewBase
{
    public AnnouncementPanel announcementPanel;
    public FriendPanel friendPanel;
    public TaskPanel taskPanel;
    public override void Show()
    {
        base.Show();
    }

    public override void Hide()
    {
        base.Hide();
    }

    public void OnBtnAnnouncementClick()
    {
        announcementPanel.Show();
    }

    public void OnBtnFriendClick()
    {
        friendPanel.Show();
    }

    public void OnBtnTaskClick()
    {
        taskPanel.Show();
    }
}
