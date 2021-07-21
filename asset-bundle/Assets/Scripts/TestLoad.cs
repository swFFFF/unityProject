using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TestLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/ui.unity3d");    //加载AssetBundle

        //代替WWW，用来处理http请求，也拥有加载assetBundle功能 较少使用
        //UnityWebRequest unityWebRequest = UnityWebRequest.Get("http://www.baidu.com");

        //if(unityWebRequest != null)
        //{
        //    Debug.Log("web 加载成功");
        //}
        if(assetBundle != null)
        {
            GameObject objectPrefab = assetBundle.LoadAsset<GameObject>("Panel");   //加载资源
            //assetBundle.LoadAsset("", typeof(GameObject));

            //加载资源和对应子资源
            Sprite[] sprites = assetBundle.LoadAssetWithSubAssets<Sprite>("logo_web");

            //加载所有数据
            //Object[] objects = assetBundle.LoadAllAssets();
            for(int i = 0; i < sprites.Length; i++)
            {
                Debug.Log(sprites[i]);
            }

            GameObject.Instantiate(objectPrefab, transform);    //实例化

            //assetBundle.Unload(false);
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
