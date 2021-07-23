using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class TestUnload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetBundleManager.UnLoadAllAssetBundles("Test1");
        AssetBundleManager.UnLoadAssetBundle("Test1", "bundle1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
