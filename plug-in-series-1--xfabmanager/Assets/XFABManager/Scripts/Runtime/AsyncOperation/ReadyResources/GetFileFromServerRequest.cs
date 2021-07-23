using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace XFABManager
{
    public class GetFileFromServerRequest : CustomAsyncOperation
    {

        private string url;
        
        private string _text;
        public string text
        {
            get {
                return _text;
            }
        }

        private string _request_url;

        public string request_url {
            get {
                return _request_url;
            }
        }

        public GetFileFromServerRequest(string url) {
            this.url = url;
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("url is null ! 请配置后重试!");
            }
        }

        public IEnumerator GetFileFromServer(string projectName, string version, string fileName)
        {
            _request_url = XFABTools.ServerPath(url, projectName, version, fileName,string.Empty);
            // 获取内容
            UnityWebRequest request = UnityWebRequest.Get(_request_url);
            yield return request.SendWebRequest();

#if UNITY_2020_1_OR_NEWER
            if (request.result != UnityWebRequest.Result.Success)
#else 
            if (request.isNetworkError || request.isHttpError)
#endif
            {
                _error = request.error;
                if ( request.responseCode == 404 ) 
                {
                    Debug.LogErrorFormat("网络路径:{0} 不存在!请检查 url 填写是否正确!",_request_url);
                }
            }
            else {
                _text = request.downloadHandler.text;
            }
            isCompleted = true;
        }
    }
}


