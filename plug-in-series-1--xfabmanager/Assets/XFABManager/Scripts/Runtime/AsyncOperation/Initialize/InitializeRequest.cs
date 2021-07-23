using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace XFABManager
{
    public class InitializeRequest : CustomAsyncOperation
    {

        public IEnumerator Initialize()
        {
            yield return null;
#if UNITY_EDITOR
            AssetBundleManager.Profile = XFABManagerSettings.Settings.CurrentProfile;
            AssetBundleManager.isInited = true;
#else
            string profilePath = string.Format("{0}/{1}", Application.streamingAssetsPath, XFABConst.profile_setting);
#if UNITY_ANDROID
            UnityWebRequest request = UnityWebRequest.Get(profilePath);
            yield return request.SendWebRequest();
            AssetBundleManager.Profile = JsonUtility.FromJson<Profile>(request.downloadHandler.text);
#else
                if (File.Exists(profilePath))
                {
                    AssetBundleManager.Profile = JsonUtility.FromJson<Profile>(File.ReadAllText(profilePath));
                }
                else
                {
                    throw new Exception(string.Format("配置文件:{0}不存在!", profilePath));
                }
#endif    

#endif
            // 初始化 获取项目版本接口
            if (AssetBundleManager.Profile.useDefaultGetProjectVersion)
            {
                GameObject gameObject = new GameObject("GetProjectVersionDefault");
                GetProjectVersionDefault defaultGetVersion = gameObject.AddComponent<GetProjectVersionDefault>();
                defaultGetVersion.url = AssetBundleManager.Profile.url;
                AssetBundleManager.SetGetProjectVersion(defaultGetVersion);
            }
            AssetBundleManager.isInited = true;
            Completed();
        }


    }
}


