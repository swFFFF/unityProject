using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class TestReadyRes : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        ReadyResRequest request = AssetBundleManager.ReadyRes("Test1");

        while (!request.isDone)
        {
            yield return null;
            Debug.LogFormat(" project:{0} updateType:{1} progress:{2}", request.currentProjectName, request.updateType, request.progress);
        }

        if (string.IsNullOrEmpty(request.error))
        {
            Debug.Log("��Դ׼���ɹ�!");
        }
        else {
            Debug.LogErrorFormat("��Դ׼��ʧ��:{0}", request.error);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
