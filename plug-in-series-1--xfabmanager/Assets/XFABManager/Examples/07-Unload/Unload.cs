using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class Unload : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Unload all AssetBundles loaded by a module without dependent modules
        AssetBundleManager.UnLoadAllAssetBundles("Examples");
        // Unload an AssetBundle for a module
        AssetBundleManager.UnLoadAssetBundle("Examples", "cube");
    }

}
