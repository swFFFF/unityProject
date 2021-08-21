using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public bool isShowCursor;

    public ViewBase pauseView;
    private void Awake()
    {
        ShowCursor(isShowCursor);
    }

    private void Update()
    {
        if(PlayerInput.Instance != null && PlayerInput.Instance.Pause)
        {
            Pause(true);
        }
    }

    public void ShowCursor(bool isShow)
    {

        //隐藏鼠标
        Cursor.visible = isShow;
        Cursor.lockState = isShow ? CursorLockMode.None : CursorLockMode.Locked;

    }

    public void Pause(bool isPause)
    {
        //显示鼠标
        ShowCursor(isPause);
        //玩家失去控制
        if(isPause && PlayerInput.Instance != null)
        {
            PlayerInput.Instance.ReleaseControl();
        }
        else
        {
            PlayerInput.Instance.GainControl();
        }
        //停止游戏逻辑
        Time.timeScale = isPause ? 0 : 1;
        //显示暂停界面
        if(isPause)
        {
            pauseView.Show();
        }
        else
        {
            pauseView.Hide();
        }

    }
    //public void OnApplicationPause(bool pause)
    //{

    //}
    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else

    Application.Quit();
#endif    
    }

    public void Restart()
    {
        Pause(false);
        transform.GetComponent<SceneChange>().Change();
    }
}
