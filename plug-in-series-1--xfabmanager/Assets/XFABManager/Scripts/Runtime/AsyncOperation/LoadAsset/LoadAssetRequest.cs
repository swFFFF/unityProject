using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XFABManager
{

    public class LoadAssetRequest : CustomAsyncOperation
    {

        //private AssetBundleManager bundleManager;

        private UnityEngine.Object _asset;

        public UnityEngine.Object asset
        {
            get {
                return _asset;
            }
        }

        //public LoadAssetRequest(AssetBundleManager bundleManager) {
        //    this.bundleManager = bundleManager;
        //}

        // 异步加载资源 
        public IEnumerator LoadAssetAsync<T>(string projectName, string bundleName, string assetName) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (AssetBundleManager.Profile.loadMode == LoadMode.Assets)
            {
                _asset = AssetBundleManager.LoadAsset<T>(projectName, bundleName, assetName);
                Completed();
                yield break;
            }
#endif
            LoadAssetBundleRequest requestBundle = AssetBundleManager.LoadAssetBundleAsync(projectName, bundleName);
            yield return requestBundle;

            if (!string.IsNullOrEmpty(requestBundle.error))
            {
                Completed(string.Format("加载AssetBundle:{0}/{1}出错:{2}", projectName, bundleName, requestBundle.error));
                yield break;
            }
            AssetBundleRequest requestAsset = requestBundle.assetBundle.LoadAssetAsync<T>(assetName);
            yield return requestAsset;
            if (requestAsset != null && requestAsset.asset != null)
            {
                _asset = requestAsset.asset;
            }
            else {
                _error = string.Format("资源{0}/{1}/{2}加载失败!",projectName,bundleName,assetName);
            }
            Completed();
        }

        public IEnumerator LoadAssetAsync(string projectName, string bundleName, string assetName, Type type )
        {

#if UNITY_EDITOR
            if (AssetBundleManager.Profile.loadMode == LoadMode.Assets)
            {
                _asset = AssetBundleManager.LoadAsset(projectName, bundleName, assetName,type);
                Completed();
                yield break;
            }
#endif
            LoadAssetBundleRequest requestBundle = AssetBundleManager.LoadAssetBundleAsync(projectName, bundleName);
            yield return requestBundle;
            if (!string.IsNullOrEmpty(requestBundle.error))
            {
                Completed(string.Format("加载AssetBundle:{0}/{1}出错:{2}", projectName, bundleName, requestBundle.error));
                yield break;
            }
            AssetBundleRequest requestAsset = requestBundle.assetBundle.LoadAssetAsync(assetName, type);
            yield return requestAsset;
            if (requestAsset != null && requestAsset.asset != null)
            {
                _asset = requestAsset.asset;
            }
            else
            {
                _error = string.Format("资源{0}/{1}/{2}加载失败!", projectName, bundleName, assetName);
            }
            Completed();

        }
    }
}

