using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class TestExtractRes : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        ExtractResRequest request = AssetBundleManager.ExtractRes("Test1");

        while (!request.isDone)
        {
            yield return null;
            Debug.Log(" 释放进度: " + request.progress );
        }

        if (string.IsNullOrEmpty(request.error))
        {

            Debug.Log("释放完成");
        }
        else {
            Debug.Log("释放出错:" + request.error);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
