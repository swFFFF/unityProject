using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace XFABManager
{
    public class ExtractResRequest : CustomAsyncOperation
    {
         
        public string CurrentExtractFile { get; private set; }

        public IEnumerator ExtractRes(string projectName) {

            CheckResUpdateRequest checkUpdate = AssetBundleManager.CheckResUpdate(projectName);
            yield return checkUpdate;

            if ( !string.IsNullOrEmpty(checkUpdate.error) ) {
                Completed(checkUpdate.error);
                yield break;
            }

            yield return CoroutineStarter.Start(ExtractRes(checkUpdate.result));

        }


        public IEnumerator ExtractRes( CheckUpdateResult result)
        {

            // 判断本地是否有这个模块资源 如果有了就不用释放了
            if (AssetBundleManager.IsHaveResOnLocal(result.projectName) || result.updateType == UpdateType.DontNeedUpdate )
            {
                //Debug.LogWarningFormat("模块{0}资源已经释放了!",result.projectName);
                Completed();
                yield break;
            }

            // 判断是否有内置资源
            IsHaveBuiltInResRequest isHaveBuiltInRes = AssetBundleManager.IsHaveBuiltInRes(result.projectName);
            yield return isHaveBuiltInRes;
            if (!isHaveBuiltInRes.isHave)
            {
                Completed(string.Format("释放资源失败!未查询到内置资源{0}!", result.projectName));
                yield break;
            }

            // 释放压缩资源
            string buildInRes = null;
            string localRes = null;


            switch (result.updateType)
            {
                case UpdateType.ExtractLocal:
                    string project_build_info = XFABTools.BuildInDataPath(result.projectName, XFABConst.project_build_info);

#if UNITY_ANDROID && !UNITY_EDITOR
                    UnityWebRequest requestBuildInfo = UnityWebRequest.Get(project_build_info);
                    yield return requestBuildInfo.SendWebRequest();
                    ProjectBuildInfo buildInfo = JsonUtility.FromJson<ProjectBuildInfo>(requestBuildInfo.downloadHandler.text);
#else
                    ProjectBuildInfo buildInfo = JsonUtility.FromJson<ProjectBuildInfo>(File.ReadAllText(project_build_info));
#endif
                    for (int i = 0; i < buildInfo.bundleInfos.Length; i++)
                    {
                        BundleInfo bundleInfo = buildInfo.bundleInfos[i];

                        buildInRes = XFABTools.BuildInDataPath(result.projectName, bundleInfo.bundleName);
                        localRes = XFABTools.LocalResPath(result.projectName, bundleInfo.bundleName);

 
                        yield return CoroutineStarter.Start( FileTools.Copy(buildInRes, localRes ));
                        CurrentExtractFile = bundleInfo.bundleName;
                        _progress = (float)(i+1) / buildInfo.bundleInfos.Length;
                        yield return null;
                    }
                    // 释放 files 文件列表文件 
                    yield return CoroutineStarter.Start( FileTools.Copy(project_build_info, XFABTools.LocalResPath(result.projectName, XFABConst.project_build_info)) );

                    break;
                case UpdateType.ExtractLocalZip:
                    string fileName = string.Format("{0}{1}", result.projectName, ".zip");
                    // 释放压缩资源
                    buildInRes = XFABTools.BuildInDataPath(result.projectName, fileName);
                    localRes = XFABTools.LocalResPath(result.projectName, fileName);

                    yield return CoroutineStarter.Start( FileTools.Copy(buildInRes, localRes ) );
                    CurrentExtractFile = "正在解压资源...";
                    _progress = 1;
                    // 解压 
                    ZipTools.UnZipFile(localRes, XFABTools.DataPath(result.projectName));
                    yield return null;
                    // 解压完成 删除压缩包
                    File.Delete(localRes);
                    //buildInfo = JsonUtility.FromJson<ProjectBuildInfo>(File.ReadAllText(project_build_info));

                    break;
                default:
                    Debug.LogErrorFormat("释放资源出错,未知类型{0}", result.updateType);
                    break;
            }

            Completed();
        }
    }

}

