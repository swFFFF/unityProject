using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XFABManager
{
    /// <summary>
    /// 下载多个文件的请求 
    /// 用来替换 DownloadManager TODO 
    /// </summary>
    public class DownloadFilesRequest : DownloadFileRequest
    {

        private Dictionary<string, string> files = new Dictionary<string, string>();

        /// <summary>
        /// 添加需要下载的文件
        /// </summary>
        /// <param name="file_url">文件的网络路径</param>
        /// <param name="localfile">本地存放的路径</param>
        public void Add(string file_url, string localfile) {

            if ( string.IsNullOrEmpty(file_url) || string.IsNullOrEmpty(localfile) ) {
                Debug.LogWarning("DownloadFilesRequest.Add 失败: 网络路径 或 本地路径 是空!");
                return;
            }

            if (!files.ContainsKey(file_url))
            {
                files.Add(file_url, localfile);
            }
            else {
                Debug.LogWarningFormat("DownloadFilesRequest.Add 失败: file_url:{0} 已存在!请勿重复添加!",file_url);
            }
        }

        /// <summary>
        /// 添加多个需要下载的文件
        /// </summary>
        /// <param name="files">key 文件的网络路径 value 本地存放的路径</param>
        public void AddRange(Dictionary<string, string> files) {
            if (files == null || files.Count == 0) {
                Debug.LogWarning("DownloadFilesRequest.AddRange 失败: files 是空!");
                return;
            }
            foreach (string key in files.Keys)
            {
                Add(key, files[key]);
            }
        }

        public IEnumerator Download()
        {

            if ( files == null || files.Count == 0) {

                Debug.LogWarning("需要下载的文件为空,请添加后重试!");
                Completed();
                yield break;
            }

            int index = 0;
            foreach (string key in files.Keys)
            {
                _file_url = key;
                string localfile = files[key];

                if (string.IsNullOrEmpty(_file_url) || string.IsNullOrEmpty(localfile)) { continue; }

                int retryCount = 0;
                do
                {
                    retryCount++;
                    if (!string.IsNullOrEmpty(downloadTool.error))
                    {
                        yield return new WaitForSeconds(2);
                    }

                    downloadTool.Start(file_url, localfile);

                    while (!downloadTool.isDone)
                    {
                        yield return null;
                        _progress = (float)(index + downloadTool.Progress) / files.Count;
                        _speed = (int)downloadTool.CurrentSpeed;
                        _speedStr = downloadTool.CurrentSpeedFormatStr;
                    }

                } while (!string.IsNullOrEmpty(downloadTool.error) && retryCount <= 10);

                if (!string.IsNullOrEmpty(downloadTool.error))
                {
                    _error = string.Format("下载文件失败:{0} url:{1}", downloadTool.error, _file_url);
                    Completed();
                    yield break;
                }

                index++;

            }
            Completed();
        }

        /// <summary>
        /// 清空待下载的文件
        /// </summary>
        public void Clear() {
            files.Clear();
            _progress = 0;
            isCompleted = false;
        }

    }

}

