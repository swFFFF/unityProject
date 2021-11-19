using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ABTest : MonoBehaviour
{
    public Image img;
    void Start()
    {
        //第一步 加载 AB包
        AssetBundle ab = AssetBundle.LoadFromFile(Application.streamingAssetsPath + "/" + "model");
        //第二步 加载 AB包中的资源（同步）
        //只用名字加载会出现同名无法区分
        //泛型加载 或者 Type指定类型
        //同一个AB包不能重复加载
        //GameObject obj = ab.LoadAsset<GameObject>("Cube");
        //GameObject obj = ab.LoadAsset("Cube", typeof(GameObject)) as GameObject;
        //Instantiate(obj);

        //异步加载->协程
        StartCoroutine(LoadABRes("icon", "222"));
    }

    IEnumerator LoadABRes(string ABName, string resName)
    {
        AssetBundleCreateRequest abcr = AssetBundle.LoadFromFileAsync(Application.streamingAssetsPath + "/" + ABName);
        yield return abcr;
        AssetBundleRequest abq = abcr.assetBundle.LoadAssetAsync(resName, typeof(Sprite));
        yield return abq;
        //abq.asset as Sprite
        img.sprite = abq.asset as Sprite;
    }

    void Update()
    {
        
    }
}
