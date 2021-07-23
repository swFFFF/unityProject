using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;



namespace XFABManager
{
    /// <summary>
    /// 下载某个项目中的某一个AssetBundle
    /// </summary>
    /// 
    public class DownloadOneAssetBundleRequest : CustomAsyncOperation
    {
        public IEnumerator DownloadOneAssetBundle(string projectName, string bundleName)
        {
            // 获取版本
            GetProjectVersionRequest requestVer = AssetBundleManager.GetProjectVersion(projectName);
            yield return requestVer;

            if (!string.IsNullOrEmpty(requestVer.error))
            {
                Completed( string.Format("获取{0}版本出错:{1}",projectName, requestVer.error ));
                yield break;
            }

            // 下载 project_build_info 文件
            string project_build_server = XFABTools.ServerPath(AssetBundleManager.Profile.url, projectName, requestVer.version,XFABConst.project_build_info);
            string project_build_local = XFABTools.LocalResPath(projectName, XFABConst.project_build_info);
            DownloadFileRequest requestBuild = DownloadFileRequest.DownloadFile(project_build_server, project_build_local);

            yield return requestBuild;

            if (!string.IsNullOrEmpty(requestBuild.error))
            {
                Completed(string.Format("获取{0}: project_build_info.json 出错:", projectName, requestVer.error));
                yield break;
            }
            ProjectBuildInfo projectBuildInfo = JsonUtility.FromJson<ProjectBuildInfo>(File.ReadAllText(project_build_local));

            // 下载 bundle 依赖信息 bundle
            string bundle_dependon_file_server = XFABTools.ServerPath(AssetBundleManager.Profile.url, projectName, requestVer.version, XFABTools.GetCurrentPlatformName());
            string bundle_dependon_file_local = XFABTools.LocalResPath(projectName, XFABTools.GetCurrentPlatformName());

            string bundle_dependon_file_md5 = null;

            for (int i = 0; i < projectBuildInfo.bundleInfos.Length; i++)
            {
                if (projectBuildInfo.bundleInfos[i].bundleName.Equals(XFABTools.GetCurrentPlatformName())) {
                    bundle_dependon_file_md5 = projectBuildInfo.bundleInfos[i].md5;
                    break;
                }
            }

            bool isNeedDownload = IsNeedUpdate(bundle_dependon_file_local, bundle_dependon_file_md5);

            if (isNeedDownload) {
                DownloadFileRequest requestDependonBundle = DownloadFileRequest.DownloadFile(bundle_dependon_file_server, bundle_dependon_file_local);
                yield return requestDependonBundle;
                if (!string.IsNullOrEmpty(requestDependonBundle.error))
                {
                    Completed(string.Format("下载{0}:出错:", bundle_dependon_file_server));
                    yield break;
                }
            }

            DownloadFilesRequest downloadFiles = new DownloadFilesRequest();

            // 获取到Bundle的依赖
            string[] dependeces = AssetBundleManager.GetAssetBundleDependences(projectName, bundleName);
            List<string> need_download_bundles = new List<string>(dependeces.Length+1);


            need_download_bundles.Add( string.Format("{0}{1}", bundleName,projectBuildInfo.suffix));
            need_download_bundles.AddRange(dependeces);

            for (int i = 0; i < need_download_bundles.Count; i++)
            {

                string localfile = XFABTools.LocalResPath(projectName, need_download_bundles[i]);
                string server_url = XFABTools.ServerPath(AssetBundleManager.Profile.url, projectName, requestVer.version, need_download_bundles[i]);

                for (int j = 0; j < projectBuildInfo.bundleInfos.Length; j++)
                {
                    if (need_download_bundles[i].Equals(projectBuildInfo.bundleInfos[j].bundleName))
                    {
                        if (IsNeedUpdate(localfile, projectBuildInfo.bundleInfos[j].md5))
                        {
                            downloadFiles.Add(server_url, localfile);
                        }
                        break;
                    }
                }
            }

            CoroutineStarter.Start(downloadFiles.Download());
            yield return downloadFiles;
            if (  !string.IsNullOrEmpty(downloadFiles.error) )
            {
                Completed(downloadFiles.error);
                yield break;
            }

            Completed();
        }

        private bool IsNeedUpdate(string localfile, string server_md5)
        {

            bool isUpdate = false;
            if (File.Exists(localfile))
            {
                if (!server_md5.Equals(XFABTools.md5file(localfile)))
                {
                    // 进行更新
                    isUpdate = true;
                    File.Delete(localfile);
                }
            }
            else
            {
                isUpdate = true;
            }
            return isUpdate;
        }

    }

}

