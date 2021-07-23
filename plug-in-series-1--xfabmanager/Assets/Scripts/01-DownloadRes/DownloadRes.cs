using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class DownloadRes : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        UpdateOrDownloadResRequest request = AssetBundleManager.UpdateOrDownloadRes("Test1");

        while (!request.isDone)
        {
            yield return null;
            // 更新信息
            Debug.Log( " 下载进度: " + request.progress);
        }

        Debug.Log("下载完成!");
        Debug.Log(Application.persistentDataPath);
        // 
        

    }

     

    // Update is called once per frame
    void Update()
    {
        
    }
}
