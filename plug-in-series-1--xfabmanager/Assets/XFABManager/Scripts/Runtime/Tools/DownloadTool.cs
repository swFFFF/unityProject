using UnityEngine;
using System.Collections;
using System.IO;
using System.Threading;
using System.Net;
using System.Diagnostics;
using System;
using System.Threading.Tasks;

namespace XFABManager
{

    /// <summary>
    /// 使用结束 请调用 Dispose 方法
    /// </summary>
    public class DownloadTool: IDisposable
    {

        #region 字段
        public const string tempSuffix = ".tempFile";


        private string url;                     // 需要下载的文件路径
        private string localfile;               // 下载之后本地存放的路径
        private string tempfile;                // 下载的临时文件路径
        private bool isRunning = false;
        private Task downloadTask;
        private bool isdone = false;

        private bool isDownloading = false;     // 是否正在下载
        private Stopwatch timer = new Stopwatch(); // 计时

        private HttpWebResponse downloadHttpRes; // http下载的回复
        private Stream downloadStream;
        private FileStream downloadWriter;

        private string _error;

        private long downloadByte = 0;
        private long startPos;                  // 开始下载的位置

        private long allByte;                  // 文件总大小
        private byte[] downloadBuffer = new byte[8192];    // 8k 的字节下载缓冲区

        private int readSize;

        private long currentSpeed;

        #endregion

        #region 属性

        /// <summary>
        /// 当下载完成之后 返回true
        /// </summary>
        public bool isDone
        {
            get {
                return isdone;
            }
        }

 
        public string error
        {
            get {
                return _error;
            }
        }

        /// <summary>
        /// 已下载的字节
        /// </summary>
        public long DownloadByte
        {
            get {
                return downloadByte;
            }
        }

        /// <summary>
        /// 总字节
        /// </summary>
        public long AllByte
        {
            get {
                return allByte;
            }
        }

        /// <summary>
        /// 当前下载速度 字节每秒
        /// </summary>
        public long CurrentSpeed{
            get {
                return currentSpeed;
            }
        }

        /// <summary>
        /// 当前下载速度 字符串
        /// </summary>
        public string CurrentSpeedFormatStr
        {
            get {

                if (CurrentSpeed > 1048576)// 大于 1 mb
                {
                    return string.Format("{0} mb/s", CurrentSpeed / 1024 / 1024);
                }
                else if (CurrentSpeed > 1024)// 大于 1 k
                {
                    return string.Format("{0} kb/s", CurrentSpeed / 1024);
                }
                else {
                    return string.Format("{0} byte/s", CurrentSpeed / 1024);
                }

            }
        }

        /// <summary>
        /// 下载的进度
        /// </summary>
        public float Progress
        {
            get {
                if ( AllByte == 0  ) { return 0; }
                return (float)((double)DownloadByte / AllByte);
            }
        }

        /// <summary>
        /// 下载使用的时间
        /// </summary>
        public double UsedTime
        {
            get {
                return timer.Elapsed.TotalSeconds;
            }
        }

        #endregion

        public DownloadTool() { }

        /// <summary>
        /// 下载完成之后 务必调用 Stop 来停止下载任务
        /// </summary>
        public void Start(string url,string localfile) {

            

            this.url = url;
            this.localfile = localfile;
            tempfile = string.Format("{0}{1}",localfile,tempSuffix);
            isdone = false;
            
            _error = string.Empty;

            timer.Reset();
            timer.Start();

            downloadByte = 0;
            // 判断是否有临时文件
            
            if (File.Exists(tempfile))
            {
                downloadWriter = File.OpenWrite(tempfile);
                startPos = downloadWriter.Length;
                downloadWriter.Seek(startPos, SeekOrigin.Current);
            }
            else
            {
                string path = Path.GetDirectoryName(tempfile);
                if ( !Directory.Exists( path ) ) {
                    Directory.CreateDirectory(path);
                }

                downloadWriter = new FileStream(tempfile, FileMode.Create);
                startPos = 0;
            }
            downloadByte += startPos;
            readSize = 0;
            isDownloading = true;

            // 开始下载
            isRunning = true;

            downloadTask = new Task(() =>
            {
                while (isRunning)
                {
                    Update();
                }
            });
            downloadTask.Start();

        }

        private void Update() {

            if ( isDownloading ) {
                // 下载文件 
                Download();
            }

        }

        private void Download() {

            try
            {
                if (downloadHttpRes == null)
                {
                    downloadHttpRes = CreateHttpRequest(url, (int)startPos);
                    downloadStream = downloadHttpRes.GetResponseStream();
                    allByte = downloadHttpRes.ContentLength;
                }
                readSize = downloadStream.Read(downloadBuffer, 0, downloadBuffer.Length);
                if (readSize > 0 || downloadByte < allByte)
                {
                    // 进行下载
                    downloadByte += readSize;
                    // 写入临时文件
                    downloadWriter.Write(downloadBuffer, 0, readSize);
                    // 计算速度
                    currentSpeed = (int)((downloadByte - startPos) / timer.Elapsed.TotalSeconds);
                }
                else
                {
                    MoveTempToFile();
                    // 停止下载
                    OnFinsh();
                }
            }
            catch (WebException e)
            {
                HttpWebResponse response = e.Response as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.RequestedRangeNotSatisfiable)
                {
                    // 触发这个异常说明文件已经下载完毕 但是没有转换 转换一下就可以了
                    MoveTempToFile();
                }
                else
                {
                    _error = e.Message;
                }
                //UnityEngine.Debug.LogError(_error);
                OnFinsh();
            }
            catch (Exception e)
            {
                //UnityEngine.Debug.LogError(e.ToString());
                _error = e.ToString();
                OnFinsh();
            }

        }

        private void OnFinsh() {
            //this.url = url;
            //this.localfile = localfile;
            isdone = true;
            isDownloading = false;
            isRunning = false;
            downloadHttpRes = null;
            if (downloadStream != null) {
                downloadStream.Close();
                downloadStream = null;
            }

            if (downloadWriter != null) {
                downloadWriter.Close();
                downloadWriter = null;
            }
            timer.Stop();
        }

        /// <summary>
        /// 下载完成 把临时文件转成正式文件
        /// </summary>
        private void MoveTempToFile() {
            // 先关掉
            if (downloadWriter != null)
            {
                downloadWriter.Close();
                downloadWriter = null;
            }
            // 说明已经下载完成 但是没有把临时文件转成正式文件
            if (File.Exists(localfile))
            {
                File.Delete(localfile);
            }
            File.Move(tempfile, localfile);
        }

        public static HttpWebResponse CreateHttpRequest(string url,int startPos = 0) {
            HttpWebRequest request = null;
            HttpWebResponse resp = null;

            request = (HttpWebRequest)HttpWebRequest.Create(url);
            request.Timeout = 3000; // 
            request.Headers.Add("Accept-Encoding", "gzip,deflate");
            request.AutomaticDecompression = DecompressionMethods.None;
            if ( startPos > 0 ) {
                request.AddRange(startPos);
            }
            resp = request.GetResponse() as HttpWebResponse;

            return resp;
        }

        /// <summary>
        /// 下载结束时调用 
        /// 调用之后该对象不能再执行下载任务 
        /// 如需下载 请重新创建对象
        /// </summary>
        public void Dispose()
        {
            isRunning = false;
        }
    }

}

