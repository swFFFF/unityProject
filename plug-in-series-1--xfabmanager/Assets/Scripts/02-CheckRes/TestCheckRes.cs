using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class TestCheckRes : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        CheckResUpdateRequest requestCheck = AssetBundleManager.CheckResUpdate("Test1");
        yield return requestCheck;

        Debug.Log(requestCheck.result);

        if (!string.IsNullOrEmpty(requestCheck.error))
        {
            // ณ๖ดํมห 
            Debug.Log(requestCheck.error);
        }
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
