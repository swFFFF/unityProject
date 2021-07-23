using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace XFABManager {

    public class LoadAssetsRequest : CustomAsyncOperation
    {
        //private AssetBundleManager bundleManager;

        private UnityEngine.Object[] _assets;

        public UnityEngine.Object[] assets {
            get {
                return _assets;
            }
        }

        //public LoadAssetsRequest(AssetBundleManager bundleManager) {
        //    this.bundleManager = bundleManager;
        //}

        /// <summary>
        /// 异步加载子资源
        /// </summary>
        public IEnumerator LoadAssetWithSubAssetsAsync<T>(string projectName, string bundleName, string assetName) where T : UnityEngine.Object
        {

#if UNITY_EDITOR
            if (AssetBundleManager.Profile.loadMode == LoadMode.Assets)
            {
                _assets = AssetBundleManager.LoadAssetWithSubAssets<T>(projectName, bundleName, assetName);
                Completed();
                yield break;
            }
#endif
            LoadAssetBundleRequest requestBundle = AssetBundleManager.LoadAssetBundleAsync(projectName, bundleName);
            yield return requestBundle;

            if (!string.IsNullOrEmpty(requestBundle.error))
            {
                _error = string.Format("加载AssetBundle:{0}/{1} 失败:{2}", projectName, bundleName, requestBundle.error);
                Completed();
                yield break;
            }

            AssetBundleRequest request = requestBundle.assetBundle.LoadAssetWithSubAssetsAsync<T>(assetName);
            yield return request;
            if (request != null && request.allAssets != null)
            {
                _assets = request.allAssets;
            }
            else
            {
                _error = string.Format("资源{0}/{1}加载失败!", projectName, bundleName);
            }
            Completed();
        }

        /// <summary>
        /// 异步加载子资源
        /// </summary>
        public IEnumerator LoadAssetWithSubAssetsAsync(string projectName, string bundleName, string assetName, Type type )
        {

#if UNITY_EDITOR
            if (AssetBundleManager.Profile.loadMode == LoadMode.Assets)
            {
                _assets = AssetBundleManager.LoadAssetWithSubAssets(projectName, bundleName, assetName,type);
                Completed();
                yield break;
            }
#endif
            LoadAssetBundleRequest requestBundle = AssetBundleManager.LoadAssetBundleAsync(projectName, bundleName);
            yield return requestBundle;
            if (!string.IsNullOrEmpty(requestBundle.error))
            {
                _error = string.Format("加载AssetBundle:{0}/{1} 失败:{2}", projectName, bundleName, requestBundle.error);
                Completed();
                yield break;
            }

            AssetBundleRequest request = requestBundle.assetBundle.LoadAssetWithSubAssetsAsync(assetName, type);
            yield return request;

            if (request != null && request.allAssets != null)
            {
                _assets = request.allAssets;
            }
            else
            {
                _error = string.Format("资源{0}/{1}加载失败!", projectName, bundleName);
            }
            
            Completed();

        }


        /// <summary>
        /// 异步加载所有资源
        /// </summary>
        /// <param name="projectName"></param>
        /// <param name="bundleName"></param>
        public IEnumerator LoadAllAssetsAsync(string projectName, string bundleName)
        {
#if UNITY_EDITOR
            if (AssetBundleManager.Profile.loadMode == LoadMode.Assets)
            {
                _assets = AssetBundleManager.LoadAllAssets(projectName, bundleName);
                Completed();
                yield break;
            }
#endif
            LoadAssetBundleRequest requestBundle = AssetBundleManager.LoadAssetBundleAsync(projectName, bundleName);
            yield return requestBundle;

            if (!string.IsNullOrEmpty(requestBundle.error))
            {
                _error = string.Format("加载AssetBundle:{0}/{1} 失败:{2}", projectName, bundleName, requestBundle.error);
                Completed();
                yield break;
            }

            AssetBundleRequest request = requestBundle.assetBundle.LoadAllAssetsAsync();
            yield return request;
            if (request != null && request.allAssets != null)
            {
                _assets = request.allAssets;
            }
            else {
                _error = string.Format("资源{0}/{1}加载失败!",projectName,bundleName);
            }

            Completed();


        }

        /// <summary>
        /// 异步加载某个类型的所有资源
        /// </summary>
        public IEnumerator LoadAllAssetsAsync<T>(string projectName, string bundleName ) where T : UnityEngine.Object
        {
#if UNITY_EDITOR
            if (AssetBundleManager.Profile.loadMode == LoadMode.Assets)
            {
                _assets = AssetBundleManager.LoadAllAssets<T>(projectName, bundleName);
                Completed();
                yield break;
            }
#endif
            LoadAssetBundleRequest requestBundle = AssetBundleManager.LoadAssetBundleAsync(projectName, bundleName);
            yield return requestBundle;

            if (!string.IsNullOrEmpty(requestBundle.error))
            {
                _error = string.Format("加载AssetBundle:{0}/{1} 失败:{2}", projectName, bundleName, requestBundle.error);
                Completed();
                yield break;
            }
            AssetBundleRequest request = requestBundle.assetBundle.LoadAllAssetsAsync<T>();

            if (request != null && request.allAssets != null)
            {
                _assets = request.allAssets;
            }
            else
            {
                _error = string.Format("资源{0}/{1}加载失败!", projectName, bundleName);
            }
            Completed();
        }

        /// <summary>
        /// 异步加载某个类型的所有资源
        /// </summary>
        public IEnumerator LoadAllAssetsAsync(string projectName, string bundleName, Type type )
        {

#if UNITY_EDITOR
            if (AssetBundleManager.Profile.loadMode == LoadMode.Assets)
            {
                _assets = AssetBundleManager.LoadAllAssets(projectName, bundleName,type);
                Completed();
                yield break;
            }
#endif
            LoadAssetBundleRequest requestBundle = AssetBundleManager.LoadAssetBundleAsync(projectName, bundleName);
            if (!string.IsNullOrEmpty(requestBundle.error))
            {
                _error = string.Format("加载AssetBundle:{0}/{1} 失败:{2}", projectName, bundleName, requestBundle.error);
                Completed();
                yield break;
            }

            AssetBundleRequest request = requestBundle.assetBundle.LoadAllAssetsAsync(type);

            if (request != null && request.allAssets != null)
            {
                _assets = request.allAssets;
            }
            else
            {
                _error = string.Format("资源{0}/{1}加载失败!", projectName, bundleName);
            }
            Completed();
        }

    }

}


