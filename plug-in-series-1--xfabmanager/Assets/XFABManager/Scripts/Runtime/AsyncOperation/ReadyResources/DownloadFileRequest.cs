using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFABManager
{

    /// <summary>
    /// 下载文件的请求
    /// </summary>
    public class DownloadFileRequest : CustomAsyncOperation
    {

        protected int _speed;
        protected string _speedStr;

        /// <summary>
        /// 当前的下载速度 单位 字节
        /// </summary>
        public int CurrentSpeed {
            get {
                return _speed;
            }
        }
        /// <summary>
        /// 格式化之后的下载速度
        /// </summary>
        public string CurrentSpeedFormatStr
        {
            get {
                return _speedStr;
            }
        }

        protected string _file_url;
        public string file_url {
            get {
                return _file_url;
            }
        }

        protected DownloadTool downloadTool;

        public DownloadFileRequest() {
            downloadTool = new DownloadTool();
        }

        public IEnumerator Download(string file_url, string localfile )
        {
            _file_url = file_url;
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
                    _progress = downloadTool.Progress;
                    _speed = (int)downloadTool.CurrentSpeed;
                    _speedStr = downloadTool.CurrentSpeedFormatStr;
                }

            } while (!string.IsNullOrEmpty(downloadTool.error) && retryCount <= 10);

            if (!string.IsNullOrEmpty(downloadTool.error))
            {
                _error = string.Format("下载文件失败:{0} url:{1}", downloadTool.error,_file_url);
            }

            Completed();
        }


        public static DownloadFileRequest DownloadFile(string file_url, string localfile)
        {
            DownloadFileRequest downloadFileRequest = new DownloadFileRequest();
            CoroutineStarter.Start(downloadFileRequest.Download(file_url,localfile));
            return downloadFileRequest;
        }


    }

}

