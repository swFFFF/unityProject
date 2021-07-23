using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class LoadAsset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject cubePrefab = AssetBundleManager.LoadAsset<GameObject>("Examples", "cube","Cube");
        GameObject.Instantiate(cubePrefab);
        
    }

 
}
