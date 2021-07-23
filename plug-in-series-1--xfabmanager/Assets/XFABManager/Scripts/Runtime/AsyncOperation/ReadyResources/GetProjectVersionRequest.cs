using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace XFABManager{

    public class GetProjectVersionRequest : CustomAsyncOperation 
    {
        private string _version;
        private IGetProjectVersion getProjectVersion;
        /// <summary>
        /// ProjectVersion
        /// </summary>
        public string version
        {
            get { return _version; }
        }

        public GetProjectVersionRequest(IGetProjectVersion  getProjectVersion){
            this.getProjectVersion = getProjectVersion;
            if (getProjectVersion == null)
            {
                throw new Exception(" 接口 IGetProjectVersion is null！请设置后重试! ");
            }
        }

        public virtual IEnumerator GetProjectVersion(string projectName , UpdateMode updateModel)
        {
            if (updateModel == UpdateMode.LOCAL)
            {
                _error = "测试模式下使用内置资源,无需版本信息!";
                isCompleted = true;
                yield break;
            }

            getProjectVersion.GetProjectVersion(projectName);
            while (!getProjectVersion.isDone())
            {
                yield return null;
            }

            if (string.IsNullOrEmpty(getProjectVersion.Error()))
            {
                _version = getProjectVersion.Result();
            }
            else
            {
                // 获取失败
                _error = getProjectVersion.Error();
            }

            isCompleted = true;
        }


    }

}

