using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class ExtractRes : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        /*
        Releasing the resources of a module (not including its dependent modules)
        Built-in resources (Application. StreamingAssetsPath) is copied to the data directory (Application. PersistentDataPath)
         */

        ExtractResRequest extractRes = AssetBundleManager.ExtractRes("Examples");
        while (!extractRes.isDone)
        {
            yield return null;
            Debug.Log("progress:"+ extractRes.progress);
        }
        Debug.Log("Release the complete!");
    }

}
