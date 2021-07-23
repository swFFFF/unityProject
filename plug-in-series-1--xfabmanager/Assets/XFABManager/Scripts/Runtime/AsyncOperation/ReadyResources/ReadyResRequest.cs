using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XFABManager
{
    public class ReadyResRequest : CustomAsyncOperation
    {

        /// <summary>
        /// 当前正在执行什么操作 比如:更新下载等等
        /// </summary>
        public UpdateType updateType { get; private set; }
        
        /// <summary>
        /// 当前正在 准备的模块的名称
        /// </summary>
        public string currentProjectName { get; private set; }

        public IEnumerator ReadyRes(string projectName) {

            // 初始化
            yield return AssetBundleManager.InitializeAsync();
            // 检测更新
            CheckResUpdatesRequest checkReq = AssetBundleManager.CheckResUpdates(projectName);
            yield return checkReq;
            if ( !string.IsNullOrEmpty(checkReq.error) ) {
                _error = string.Format("准备资源失败,检测更新出错:{0}",checkReq.error);
                Completed();
                yield break;
            }
            yield return CoroutineStarter.Start(ReadyRes(checkReq.results));
            Completed();
        }


        public IEnumerator ReadyRes(CheckUpdateResult[] results) {

            // 初始化
            yield return AssetBundleManager.InitializeAsync();

            // 具体操作
            for (int i = 0; i < results.Length; i++)
            {
                updateType =results[i].updateType;
                currentProjectName = results[i].projectName;

                switch (results[i].updateType)
                {
                    case UpdateType.Update:
                    case UpdateType.Download:
                    case UpdateType.DownloadZip:

                        // 更新或者下载资源
                        UpdateOrDownloadResRequest downloadReq = AssetBundleManager.UpdateOrDownloadRes(results[i]);

                        while (!downloadReq.isDone)
                        {
                            yield return null;
                            _progress = downloadReq.progress;
                        }

                        if (!string.IsNullOrEmpty(downloadReq.error))
                        {
                            _error = string.Format("准备资源失败,下载出错:{0}", downloadReq.error);
                            Completed();
                            yield break;
                        }

                        break;
                    case UpdateType.ExtractLocal:
                    case UpdateType.ExtractLocalZip:

                        // 释放资源
                        ExtractResRequest extractReq = AssetBundleManager.ExtractRes(results[i]);
                        while (!extractReq.isDone)
                        {
                            yield return null;
                            _progress = extractReq.progress;
                        }

                        if (!string.IsNullOrEmpty(extractReq.error))
                        {
                            _error = string.Format("准备资源失败,释放资源出错:{0}", extractReq.error);
                            Completed();
                            yield break;
                        }

                        // 释放完成之后 还需要再检测是否需要更新 
                        yield return CoroutineStarter.Start(ReadyRes(results[i].projectName));
                        break;
                    case UpdateType.Error:
                        // 出错
                        _error = string.Format("准备资源失败,{0}",results[i].message);
                        Completed();
                        yield break;
                }

                yield return null;
                _progress = 0;
                
            }

        }

    }

}

