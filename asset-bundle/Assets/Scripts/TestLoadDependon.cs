using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestLoadDependon : MonoBehaviour
{
    // Start is called before the first frame update
    IEnumerator Start()
    {
        //获取依赖关系并加载依赖
        AssetBundle depenon = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/StreamingAssets");
        AssetBundleManifest manifest = depenon.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        string[] depenons =  manifest.GetAllDependencies("ui1.unity3d");

        AssetBundle texture = null;

        for(int i = 0; i < depenons.Length; i++)
        {
            //Debug.LogFormat("ui depenon : {0}", depenons[i]);
            AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + depenons[i]);
        }

        texture = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/ui1.unity3d");
        if (texture != null)
        {
            GameObject objectPrefab = texture.LoadAsset<GameObject>("Panel1");   //加载资源
            GameObject.Instantiate(objectPrefab, transform);    //实例化
        }

        //卸载assetBundle true 卸载资源和内存占用 false 卸载内存占用
        //assetBundle.Unload();

        yield return new WaitForSeconds(2);

        texture.Unload(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
