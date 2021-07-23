using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace XFABManager
{

    /// <summary>
    /// 检测资源 包含依赖项目
    /// </summary>
    public class CheckResUpdatesRequest : CustomAsyncOperation
    {
        private CheckUpdateResult[] _results;

        public CheckUpdateResult[] results
        {
            get
            {
                return _results;
            }
        }

        // 检测资源更新
        public IEnumerator CheckResUpdates(string projectName) {

            // 初始化
            yield return AssetBundleManager.InitializeAsync();

            // 获取依赖项目
            GetProjectDependenciesRequest requestDenpend = AssetBundleManager.GetProjectDependencies(projectName);
            yield return requestDenpend;
            if (!string.IsNullOrEmpty(requestDenpend.error)) { 
                _error =string.Format("获取依赖项目出错:{0}", requestDenpend.error) ;
                isCompleted = true;
                yield break; 
            }
            string[] dependencies = requestDenpend.dependencies;

            // 需要检测的项目
            List<string> need_check_projects = new List<string>();
            need_check_projects.Add(projectName);      // 自己
            need_check_projects.AddRange(dependencies);// 依赖项目
                                                       // 检测的结果 
            _results = new CheckUpdateResult[need_check_projects.Count]; // 除了依赖项目还要检测自己

            // 检测
            for (int i = 0; i < need_check_projects.Count; i++)
            {
                CheckResUpdateRequest request = AssetBundleManager.CheckResUpdate(need_check_projects[i]);
                yield return request;
                if (!string.IsNullOrEmpty(request.error) ) {
                    _error = string.Format("检测{0}资源出错:{1}", need_check_projects[i], request.error  );
                    break;
                }
                _results[i] = request.result;
            }
            isCompleted = true;
        }
    }


    public class CheckUpdateResult
    {
        public UpdateType updateType;
        /// <summary>
        /// 需要更新的大小 单位 字节
        /// </summary>
        public long updateSize;
        /// <summary>
        /// 更新的内容
        /// </summary>
        public string message = string.Empty;
        /// <summary>
        ///  需要更新的文件列表
        /// </summary>
        public string[] need_update_files; 
        public string projectName;

        /// <summary>
        /// 当前的版本
        /// </summary>
        public string version;

        public CheckUpdateResult(string projectName)
        {
            this.projectName = projectName;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("projectName:").Append(projectName).Append("\n");
            stringBuilder.Append("UpdateType:").Append(updateType).Append("\n");
            stringBuilder.Append("updateSize:").Append((double)updateSize / 1024).Append("kb").Append("\n");
            stringBuilder.Append("message:").Append(message).Append("\n");
            stringBuilder.Append("version:").Append(version).Append("\n");

            stringBuilder.Append("需要更新的文件列表:").Append("\n");
            if (need_update_files != null)
            {

                for (int i = 0; i < need_update_files.Length; i++)
                {
                    stringBuilder.Append(need_update_files[i]).Append("\n");
                }
            }

            return stringBuilder.ToString();
        }

    }

}

