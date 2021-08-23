using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public LoadView loadView;
    public int sceneIndex;
    public void Change()
    {
        if (loadView != null)
        {
            loadView.gameObject.SetActive(true);
        }
        SceneController.Instance.LoadScene(sceneIndex, (progress) =>
        {
            if (loadView != null)
            {
                loadView.UpdateProgress(progress);
            }
        }, () =>
        {
            if (loadView != null)
            {
                SceneManager.MoveGameObjectToScene(loadView.gameObject, SceneManager.GetActiveScene());
                Destroy(loadView.gameObject);
            }
        });
    }
}
