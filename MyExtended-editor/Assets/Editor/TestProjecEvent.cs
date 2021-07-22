using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestProjecEvent : UnityEditor.AssetModificationProcessor
{
    //Unity 在即将创建未导入的资源（例如，.meta 文件）时调用此方法。
    public static void OnWillCreateAsset(string path)   
    {
        Debug.LogFormat("创建资源：{0}", path);
    }

    //Unity 即将向磁盘写入序列化资源或场景文件时会调用此方法。 如果此方法已实现，则可通过返回包含 Unity 已传递的路径名称子集的数组， 覆盖已写入的文件。请注意，以下函数是静态函数。
    public static string[] OnWillSaveAssets(string[] path)  
    {
        for(int i = 0; i < path.Length; i++)
        {
            Debug.LogFormat("保存资源：{0}", path[i]);
        }
        return path;
    }

    //Unity 即将在磁盘上移动资源时会调用此方法。实现此方法可自定义 Unity 在 Editor 中移动资源时执行的操作。此方法允许您自己移动资源，但如果您这样做，请记住返回正确的枚举。或者，您可以执行一些处理并让 Unity 移动文件。 通过返回 AssetMoveResult.FailedMove 可防止移动资源 您不应在此回调中调用任何 Unity AssetDatabase API，最好限制您自己对文件操作或 VCS API 的使用。
    public static AssetMoveResult OnWillMoveAsset(string oldPath, string newPath)
    {
        Debug.LogFormat("移动资源：从{0} 移动到 {1}", oldPath, newPath);
        return AssetMoveResult.DidNotMove;     //传递标记
    }

    //当 Unity 即将从磁盘中删除资源时，则会调用此方法。如果此方法已实现，则可通过此方法自行删除资源。 通过返回 AssetDeleteResult.FailedDelete 可防止删除文件 您不应在此回调内调用任何 Unity AssetDatabase API，最好保留文件操作或 VCS API。
    public static AssetDeleteResult OnWillDeleteAsset(string path, RemoveAssetOptions option)
    {
        Debug.LogFormat("删除资源：从{0} 删除  选项：{1}", path, option);
        return AssetDeleteResult.DidNotDelete;
    }

    [InitializeOnLoadMethod]
    static void InitOnLoad()
    {
        //为委托附上具体方法 监听资源改变
        EditorApplication.projectChanged += () =>
        {
            Debug.Log("Asset change");
        };
    }
}
