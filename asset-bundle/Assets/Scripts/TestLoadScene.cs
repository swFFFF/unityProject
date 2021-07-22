using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestLoadScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //加载assetBundle资源到内存中
        AssetBundle assetBundle = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/scenes.unity3d");

        //加载场景
        SceneManager.LoadScene("TestScene");

        //卸载没有在使用的非托管资源  能放入场景的是非托管资源 不能放入是托管资源
        GameObject game =  Resources.Load<GameObject>("");
        Resources.UnloadAsset(game);
        //卸载没有引用资源
        Resources.UnloadUnusedAssets();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
