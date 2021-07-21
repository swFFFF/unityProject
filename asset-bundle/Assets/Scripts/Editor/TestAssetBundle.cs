using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TestAssetBundle : MonoBehaviour
{
    [MenuItem("AssetBundle/Build Windows")]
    static void BuildAssetBundle()
    {
        //Debug.Log(Application.streamingAssetsPath);
        string outputPath = Application.streamingAssetsPath; //获取streamingAssets路径
        if (!Directory.Exists(outputPath))
        {
            Directory.CreateDirectory(outputPath);  //创建streamingAssets文件
        }

        //通过指定资源信息构建AssetBundle 资源和场景不能放在一个AssetBundleBuild里
        //共同资源放在一个独立AssetBundle里可以减少空间占用
        List<AssetBundleBuild> builds = new List<AssetBundleBuild>();

        AssetBundleBuild build = new AssetBundleBuild();
        build.assetBundleName = "ui";
        build.assetBundleVariant = "unity3d";
        build.assetNames = new string[] {"Assets/Prefab/Panel.prefab"};

        AssetBundleBuild build1 = new AssetBundleBuild();
        build1.assetBundleName = "ui1";
        build1.assetBundleVariant = "unity3d";
        build1.assetNames = new string[] {"Assets/Prefab/Panel1.prefab"};

        AssetBundleBuild build2 = new AssetBundleBuild();
        build2.assetBundleName = "scenes";
        build2.assetBundleVariant = "unity3d";
        build2.assetNames = new string[] { "Assets/Scenes/TestScene.unity" };

        AssetBundleBuild build3 = new AssetBundleBuild();
        build3.assetBundleName = "ui2";
        build3.assetBundleVariant = "unity3d";
        build3.assetNames = new string[] {"Assets/Texture/logo_web.png","Assets/Texture/logo.png"};

        builds.Add(build);
        builds.Add(build1);
        builds.Add(build2);
        builds.Add(build3);

        BuildPipeline.BuildAssetBundles(outputPath, builds.ToArray(), BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);   //把资源构筑进AssetBundle
    }
}
