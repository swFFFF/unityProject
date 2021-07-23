using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class UpdateOrDownload : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        /*
         * Download resources
         */
        UpdateOrDownloadResRequest resRequest = AssetBundleManager.UpdateOrDownloadRes("Examples");

        while (!resRequest.isDone)
        {
            yield return null;
            Debug.Log("progress:"+resRequest.progress);
        }

        if (string.IsNullOrEmpty(resRequest.error))
        {
            Debug.Log("The download is complete!");
        }
        else {
            Debug.LogError("error:"+resRequest.error);
        }
        
    }

     
}
