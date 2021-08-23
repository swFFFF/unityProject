using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanel : ViewBase
{
    public AnnouncementPanel announcementPanel;
    public FriendPanel friendPanel;
    public TaskPanel taskPanel;
    public BagPanel bagPanel;
    public override void Show()
    {
        //base.Show();
        transform.GetComponent<Animator>().SetBool("isShow", true);
    }

    public override void Hide()
    {
        //base.Hide();
        transform.GetComponent<Animator>().SetBool("isShow", false);
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

    public void OnBtnBagPanelClick()
    {
        bagPanel.Show();
        this.Hide();
    }
}
