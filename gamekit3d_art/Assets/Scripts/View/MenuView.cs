using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuView : ViewBase
{
    public void OnStartButtonClick()
    {
        SceneController.Instance.LoadScene(1, null, null);
    }
}
