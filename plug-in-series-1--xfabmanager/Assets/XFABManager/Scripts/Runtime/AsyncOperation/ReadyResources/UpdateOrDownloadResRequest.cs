using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace XFABManager
{
    public class UpdateOrDownloadResRequest : CustomAsyncOperation
    {

        public IEnumerator UpdateOrDownloadRes(string projectName) {
            // 初始化
            yield return AssetBundleManager.InitializeAsync();

            CheckResUpdateRequest requestUpdate = AssetBundleManager.CheckResUpdate(projectName);
            yield return requestUpdate;

            if ( !string.IsNullOrEmpty( requestUpdate.error ) ) {
                Completed(requestUpdate.error);
                yield break;
            }
            yield return CoroutineStarter.Start(UpdateOrDownloadRes(requestUpdate.result));
        }

        /// <summary>
        /// 更新或下载资源
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public IEnumerator UpdateOrDownloadRes(CheckUpdateResult result) {

            // 初始化
            yield return AssetBundleManager.InitializeAsync();

            // 判断是不是下载 压缩包
            if (result.updateType == UpdateType.DownloadZip)
            {
                string zipUrl = XFABTools.ServerPath(AssetBundleManager.Profile.url, result.projectName, result.version, result.projectName, ".zip");
                string localfile = XFABTools.LocalResPath(result.projectName, string.Format("{0}{1}", result.projectName, ".zip"));


                DownloadFileRequest requestZip = new DownloadFileRequest();
                CoroutineStarter.Start(requestZip.Download(zipUrl, localfile));
                while ( !requestZip.isDone ) {
                    yield return null;
                    _progress = requestZip.progress;
                }
                if (!string.IsNullOrEmpty(requestZip.error)) {
                    Completed(requestZip.error);
                    yield break; 
                }

                // 进行解压
                ZipTools.UnZipFile(localfile, XFABTools.DataPath(result.projectName));
                yield return null;
                // 解压完成之后 删除压缩包
                File.Delete(localfile);
            }
            else if(result.updateType == UpdateType.Update || result.updateType == UpdateType.Download)
            {
                // 更新下载
                string fileUrl = null;
                string localFile = null;

                DownloadFilesRequest files = new DownloadFilesRequest();

                for (int i = 0; i < result.need_update_files.Length; i++)
                {
                    fileUrl = XFABTools.ServerPath(AssetBundleManager.Profile.url, result.projectName, result.version, result.need_update_files[i], string.Empty);// 这个文件名是包含后缀的
                    localFile = XFABTools.LocalResPath(result.projectName, result.need_update_files[i]);

                    files.Add(fileUrl, localFile);
                }

                CoroutineStarter.Start(files.Download());
                while (  !files.isDone)
                {
                    yield return null;
                    _progress = files.progress;
                }

                if ( !string.IsNullOrEmpty(files.error) )
                {
                    Completed(string.Format("{0} url:{1}", files.error, files.file_url));
                    yield break;
                }
            }

            // 下载完成
            Completed();

        }

    }

}

