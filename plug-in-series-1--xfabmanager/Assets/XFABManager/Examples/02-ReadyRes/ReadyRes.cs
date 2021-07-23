using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;


public class ReadyRes : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;

        /*
        Prepare a module resource
        This method first determines whether there are resources in the local area,
        If there is a local resource, it will determine if it is in update mode,
        Detect the update if it is in update mode
        Ready to finish entering the game if not updated mode
        If there are no resources locally
        It determines if there are any built-in resources
        If there are built-in resources, release them
        If there are no built-in resources to determine whether update mode
        Download remote resources if it is in update mode
        If the update mode does not indicate a lack of resources, Error!
        If you are in Editor mode and the Assets are loaded from Assets, you do not need to have the actual AssetBundle.
        Will be ready to complete directly
         */
        ReadyResRequest resRequest = AssetBundleManager.ReadyRes("Examples");

        while (!resRequest.isDone)
        {
            yield return null;
            if (string.IsNullOrEmpty(resRequest.currentProjectName)) continue;
            Debug.LogFormat("Preparing resources:{0} progress:{1} type:{2}", resRequest.currentProjectName,resRequest.progress,resRequest.updateType);
        }
        Debug.Log("ReadyRes Complete！");
         
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
