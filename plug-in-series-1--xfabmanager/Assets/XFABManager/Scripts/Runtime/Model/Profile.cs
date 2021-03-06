using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XFABManager
{

    [Serializable]
    public class Profile
    {

        public Profile() { }

        public Profile(string name)
        {
            this.name = name;
        }

        public string name = "Default";

        /// <summary>
        /// 资源更新地址
        /// </summary>
        public string url = string.Empty;

        /// <summary>
        /// 更新模式 默认 DEBUG
        /// </summary>
        public UpdateMode updateModel = UpdateMode.LOCAL;

        /// <summary>
        /// 加载模式 默认 Assets
        /// </summary>
        public LoadMode loadMode = LoadMode.Assets;

        /// <summary>
        /// 是否使用默认的 获取项目版本
        /// </summary>
        public bool useDefaultGetProjectVersion = true;

    }

}
