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
            // ������Ϣ
            Debug.Log( " ���ؽ���: " + request.progress);
        }

        Debug.Log("�������!");
        Debug.Log(Application.persistentDataPath);
    }

     

    // Update is called once per frame
    void Update()
    {
        
    }
}
