using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class CustomGetProjectVersion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        /*
        Sets the interface to get the project version
        The default way to get the project version is to get the current version of the module by getting the URL /versions/{projectName}.txt
        If custom fetching is required, the interface igetProjectVersion can be implemented
        Then call AssetBundleManager.SetGetProjectVersion() set up the instance can!
         */
        AssetBundleManager.SetGetProjectVersion(null);
    }

    
}
