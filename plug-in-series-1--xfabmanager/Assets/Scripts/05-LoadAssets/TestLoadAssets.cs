using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XFABManager;

public class TestLoadAssets : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //GameObject cubePre = AssetBundleManager.LoadAsset("Test1", "bundle1", "Cube", typeof(GameObject)) as GameObject;
        //GameObject cubePre = AssetBundleManager.LoadAsset<GameObject>("Test1", "bundle1", "Cube" );
        //GameObject.Instantiate(cubePre);

        //LoadAssetRequest request = AssetBundleManager.LoadAssetAsync<GameObject>("Test1", "bundle1", "Cube");
        //yield return request;
        //GameObject.Instantiate(request.asset);
        //Sprite[] sprites = AssetBundleManager.LoadAssetWithSubAssets<Sprite>("Test1", "item", "Items");
        //foreach (var item in sprites)
        //{
        //    Debug.Log(item);
        //}
        yield return new WaitForSeconds(1);
        //GameObject[] objects = AssetBundleManager.LoadAllAssets<GameObject>("Test1", "bundle1");
        //foreach (var item in objects)
        //{
        //    GameObject.Instantiate(item);
        //}

        AssetBundleManager.LoadScene("Test1", "scenes", "TestLoadScene", UnityEngine.SceneManagement.LoadSceneMode.Single);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
