using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class CheckRes : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        /*
        Check whether a module's resources need to be updated and released, etc
        It does not include its dependent items
         */
        CheckResUpdateRequest updateRequest = AssetBundleManager.CheckResUpdate("Examples");
        yield return updateRequest;
         
        Debug.LogFormat("project name:{0}", updateRequest.result.projectName);
        Debug.LogFormat("Size of content to download (in bytes):{0}", updateRequest.result.updateSize);
        Debug.LogFormat("Update the content:{0}", updateRequest.result.message);
        /*
        Checks whether a module and its dependent module resources need to be updated and released, etc
        */
        CheckResUpdatesRequest updatesRequest = AssetBundleManager.CheckResUpdates("Examples");
        yield return updatesRequest;
        
    }

     
}
