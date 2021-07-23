using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class TestGetProjectVersion : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //AssetBundleManager.SetGetProjectVersion(new MyGetProjectVersion());
        //AssetBundleManager.CheckResUpdate("Test1");
        //IsHaveBuiltInResRequest request = AssetBundleManager.IsHaveBuiltInRes("Test1");
        //yield return request;

        //Debug.Log(request.isHave);
        yield return null;
        Debug.Log(Application.persistentDataPath);
        Debug.Log( AssetBundleManager.IsHaveResOnLocal("Test1"));
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class MyGetProjectVersion : IGetProjectVersion
{
    public string Error()
    {
        return "";
    }

    public void GetProjectVersion(string projectName)
    {
        Debug.Log(" 获取项目版本: " + projectName);
        Debug.Log(" GetProjectVerson TODO! ");

        

    }

    public bool isDone()
    {
        return false;
    }

    public string Result()
    {
        return "1.0.0";
    }
}
