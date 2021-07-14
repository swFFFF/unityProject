using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    #region 字段
    private int currentIndex;
    private Action<float> onProgressChange;
    private Action onFinish;
    private static SceneController _instance;
    #endregion

    #region 属性
    public static SceneController Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject obj = new GameObject("SceneController");
                obj.AddComponent<SceneController>();
            }
            return _instance;
        }
    }
    #endregion

    #region Unity生命周期
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (_instance != null)
        {
            throw new Exception("场景中存在多个SceneController");
        }
        _instance = this;

    }
    #endregion

    #region 方法
    public void LoadScene(int index, Action<float> onProgressChange, Action onFinish)
    {
        //SceneManager.LoadScene(index);//同步加载
        //AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(index);//同步加载
        this.currentIndex = index;
        this.onProgressChange = onProgressChange;
        this.onFinish = onFinish;

        StartCoroutine(LoadScenes());
    }

    private IEnumerator LoadScenes()
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(this.currentIndex);//同步加载
        while (!asyncOperation.isDone)
        {
            yield return null;
            onProgressChange?.Invoke(asyncOperation.progress);
            ////等价于
            //if(onProgressChange != null)
            //{
            //    onProgressChange(asyncOperation.progress);
            //}
        }
        yield return new WaitForSeconds(1f);
        onFinish?.Invoke();
    }
    #endregion
}
