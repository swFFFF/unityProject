using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagPanel : ViewBase
{
    public MenuPanel menuPanel;
    public override void Hide()
    {
        base.Hide();
        menuPanel.Show();
    }

    public override void Show()
    {
        Invoke("ShowExc", 1.0f);
    }

    public void ShowExc()
    {
        base.Show();
    }
}
