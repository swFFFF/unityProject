using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using UnityEngine;

namespace XFABManager
{

    /// <summary>
    /// 检测资源 不包含依赖项目
    /// </summary>
    public class CheckResUpdateRequest : CustomAsyncOperation
    {
        //private CheckUpdateResult _result;
        public CheckUpdateResult result { get; private set; }



        public IEnumerator CheckResUpdate(string projectName)
        {
            // 初始化
            yield return AssetBundleManager.InitializeAsync();

            // 构建检测结果
            result = new CheckUpdateResult(projectName);
            // 根据更新模式 检测本地资源 判断本地是否 有资源 是否有内置资源 给出更新方案
            yield return CoroutineStarter.Start( CheckLocalRes(projectName));
            // 如果检测结果 需要更新或下载 再进行网络检测
            if (result.updateType == UpdateType.Update || result.updateType == UpdateType.Download)
            {
                // 获取项目版本
                GetProjectVersionRequest requestVersion = AssetBundleManager.GetProjectVersion(projectName);
                yield return requestVersion;

                if (!string.IsNullOrEmpty(requestVersion.error))
                {
                    // 检测出错
                    result.updateType = UpdateType.Error;
                    Completed(string.Format("获取版本出错:{0}", requestVersion.error));
                    yield break;
                }
                result.version = requestVersion.version;
                // 检测是否有压缩文件
                if (CheckServerZip(projectName, requestVersion.version))
                {
                    Completed();
                    yield break;
                }

                // 获取服务器的文件列表信息
                ProjectBuildInfo projectBuildInfo = null;

                GetFileFromServerRequest requestFile = AssetBundleManager.GetFileFromServer(projectName, requestVersion.version, XFABConst.project_build_info);
                yield return requestFile;

                if (!string.IsNullOrEmpty(requestFile.error))
                {
                    result.updateType = UpdateType.Error;
                    _error = string.Format("获取项目{0}文件列表出错:{1}", projectName, requestFile.error);
                    result.message = _error;
                    isCompleted = true;
                    yield break;
                }
                else
                {
                    projectBuildInfo = JsonUtility.FromJson<ProjectBuildInfo>(requestFile.text);
                }

                // 获取到服务端文件列表 与本地进行比较 找出需要更新的文件
                CompareWithLocal(projectName, projectBuildInfo.bundleInfos);

                // 获取更新内容
                result.message = projectBuildInfo.update_message;

            }
            isCompleted = true;
        }

        /// <summary>
        /// 检测服务端是否有zip文件
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        private bool CheckServerZip(string projectName, string version)
        {
            if (result.updateType == UpdateType.Download)
            {
                try
                {
                    string zipPath = XFABTools.ServerPath(AssetBundleManager.Profile.url, projectName, version, projectName, ".zip");
                    //创建根据网络地址的请求对象
                    HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.CreateDefault(new Uri(zipPath));
                    httpWebRequest.Method = "HEAD";
                    httpWebRequest.Timeout = 1000; //返回响应状态是否是成功比较的布尔值
                    HttpWebResponse webResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                    if (webResponse.StatusCode == HttpStatusCode.OK)
                    {
                        result.updateType = UpdateType.DownloadZip;
                        result.updateSize = webResponse.ContentLength;
                        return true;
                    }
                }
                catch { }

            }

            return false;
        }


        /// <summary>
        /// 与本地的文件比较 判断是否需要更新
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="files"></param>
        private void CompareWithLocal(string projectName, BundleInfo[] bundleInfos)
        {

            List<string> need_update_files = new List<string>();

            for (int i = 0; i < bundleInfos.Length; i++)
            {

                bool isNeedUpdate = false;
                // 判断本地是否存在
                string localFile = XFABTools.LocalResPath(projectName, bundleInfos[i].bundleName);
                if (File.Exists(localFile))
                {
                    // 判断md5值是否相同
                    isNeedUpdate = !XFABTools.md5file(localFile).Equals(bundleInfos[i].md5);
                }
                else
                {
                    // 本地没有 需要更新
                    isNeedUpdate = true;
                }

                if (isNeedUpdate)
                {
                    result.updateSize += bundleInfos[i].bundleSize;
                    need_update_files.Add(bundleInfos[i].bundleName);
                }
            }
            
            // 判断是否需要更新
            if (result.updateSize == 0 || need_update_files.Count == 0)
            {
                result.updateType = UpdateType.DontNeedUpdate;
            }
            else {
                // 需要更新 同时需要更新 project_build_info
                need_update_files.Add(XFABConst.project_build_info);
            }
            result.need_update_files = need_update_files.ToArray();
        }


        /// <summary>
        /// 检测本地资源
        /// </summary>
        private IEnumerator CheckLocalRes(string projectName)
        {
            bool isUpdate = AssetBundleManager.Profile.updateModel == UpdateMode.UPDATE;

            // 如果是编辑器  并且 从 Assets 加载资源 
            // 这种情况下 是 不需要AssetBundle的 
#if UNITY_EDITOR
            if ( AssetBundleManager.Profile.loadMode == LoadMode.Assets) {
                result.updateType = UpdateType.DontNeedUpdate;
                yield break;
            }
#endif

            // 判断本地是否资源 
            if (AssetBundleManager.IsHaveResOnLocal(projectName))
            {
                // 本地有资源 
                result.updateType = isUpdate ? UpdateType.Update : UpdateType.DontNeedUpdate;
            }
            else
            {
                IsHaveBuiltInResRequest isHaveBuiltIn = AssetBundleManager.IsHaveBuiltInRes(projectName);
                yield return isHaveBuiltIn;

                // 如果没有 判断有没有内置资源 如果有 就释放 如果没有 从服务器下载

                if (isHaveBuiltIn.isHave)
                {
                    // 判断是不是内置的压缩资源
                    result.updateType = (isHaveBuiltIn.buildInResType == BuildInResType.Zip ? UpdateType.ExtractLocalZip : UpdateType.ExtractLocal); // 释放资源
                }
                else
                {
                    result.updateType = isUpdate ? UpdateType.Download : UpdateType.Error;
                    if (!isUpdate)
                    {
                        result.message = string.Format("缺少{0}资源！", projectName);
                    }
                }
            }
        }

    }


    /// <summary>
    /// 更新类型 
    /// </summary>
    public enum UpdateType
    {
        /// <summary>
        /// 更新资源
        /// </summary>
        Update,
        /// <summary>
        /// 下载资源
        /// </summary>
        Download,

        /// <summary>
        /// 下载压缩文件
        /// </summary>
        DownloadZip,

        /// <summary>
        /// 释放内置资源
        /// </summary>
        ExtractLocal,

        /// <summary>
        /// 释放内置压缩资源
        /// </summary>
        ExtractLocalZip,

        /// <summary>
        /// 不需要更新
        /// </summary>
        DontNeedUpdate,
        /// <summary>
        /// 更新出错
        /// </summary>
        Error

    }



    

}

