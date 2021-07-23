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
            Debug.Log(" �ͷŽ���: " + request.progress );
        }

        if (string.IsNullOrEmpty(request.error))
        {

            Debug.Log("�ͷ����");
        }
        else {
            Debug.Log("�ͷų���:" + request.error);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
