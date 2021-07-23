using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


namespace XFABManager
{
    public class LoadAllAssetBundlesRequest : CustomAsyncOperation
    {
        //private AssetBundleManager bundleManager;

        //public LoadAllAssetBundlesRequest(AssetBundleManager bundleManager) {
        //    this.bundleManager = bundleManager;
        //}

        public IEnumerator LoadAllAssetBundles(string projectName )
        {

            // 读取这个模块所有的文件
            string project_build_info = XFABTools.LocalResPath(projectName, XFABConst.project_build_info);
            if (!File.Exists(project_build_info))
            {
                Completed(string.Format("LoadAllAssetBundles 失败!{0}文件不存在", project_build_info));
                yield break;
            }

            ProjectBuildInfo buildInfo = JsonUtility.FromJson<ProjectBuildInfo>( File.ReadAllText(project_build_info));
            //string suffix = buildInfo.suffix;
            for (int i = 0; i < buildInfo.bundleInfos.Length; i++)
            {
                yield return AssetBundleManager.LoadAssetBundleAsync(projectName, Path.GetFileNameWithoutExtension(buildInfo.bundleInfos[i].bundleName));
                _progress = (float)i / buildInfo.bundleInfos.Length;
            }

            Completed();
        }
    }
}


