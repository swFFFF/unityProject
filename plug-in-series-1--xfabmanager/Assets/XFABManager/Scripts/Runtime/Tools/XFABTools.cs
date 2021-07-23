using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

namespace XFABManager
{
    // 工具方法
    public class XFABTools
    {

        /// <summary>
        /// 数据存放目录 
        /// </summary>
        /// <param name="projectName">资源Project 的 name</param>
        /// <returns></returns>
        public static string DataPath(string projectName)
        {
            return string.Format("{0}/{1}/{2}",Application.persistentDataPath, projectName,GetCurrentPlatformName());
        }


        /// <summary>
        /// 内置数据目录 (提前放入程序包内的数据的目录) 
        /// </summary>
        /// <param name="projectName">资源Project 的 name</param>
        /// <returns></returns>
        public static string BuildInDataPath(string projectName)
        {
            return string.Format("{0}/{1}/{2}/", Application.streamingAssetsPath, projectName, GetCurrentPlatformName());
        }
        /// <summary>
        /// 内置数据目录 (提前放入程序包内的数据的目录) 
        /// </summary>
        /// <param name="projectName">资源Project 的 name</param>
        /// <returns></returns>
        public static string BuildInDataPath(string projectName,string fileName)
        {
            return string.Format("{0}/{1}/{2}/{3}", Application.streamingAssetsPath, projectName, GetCurrentPlatformName(),fileName);
        }


        /// <summary>
        /// 资源的网络路径
        /// </summary>
        /// <param name="url">网络路径</param>
        /// <param name="projectName">项目名</param>
        /// <param name="version">版本</param>
        /// <param name="fileName">文件名</param>
        /// <param name="suffix">后缀 如果文件名包含后缀 后缀填空！</param>
        /// <returns></returns>
        public static string ServerPath(string url,string projectName,string version,string fileName,string suffix) {

            return string.Format("{0}/{1}/{2}/{3}/{4}{5}",url,projectName,version,GetCurrentPlatformName(),fileName,suffix);
        }

        /// <summary>
        /// 资源的网络路径
        /// </summary>
        /// <param name="url">网络路径</param>
        /// <param name="projectName">项目名</param>
        /// <param name="version">版本</param>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public static string ServerPath(string url, string projectName, string version, string fileName )
        {
            return string.Format("{0}/{1}/{2}/{3}/{4}", url, projectName, version, GetCurrentPlatformName(), fileName);
        }


        /// <summary>
        /// 本地资源文件路径
        /// </summary>
        /// <param name="projectName">项目名</param>
        /// <param name="fileName">文件名 需要包含后缀</param>
        /// <returns></returns>
        public static string LocalResPath(string projectName, string fileName)
        {
            return string.Format("{0}/{1}", DataPath(projectName) , fileName);
        }

        /// <summary>
        /// 获取当前平台 对应的 资源名称
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentPlatformName()
        {
#if UNITY_EDITOR
            return EditorUserBuildSettings.activeBuildTarget.ToString();
#else
            switch (Application.platform)
            {
                case RuntimePlatform.OSXEditor:
                case RuntimePlatform.OSXPlayer:
                    return "StandaloneOSX";
                case RuntimePlatform.WindowsPlayer:
                case RuntimePlatform.WindowsEditor:
#if UNITY_64
                    return "StandaloneWindows64";
#else
                    return "StandaloneWindows";
#endif
                case RuntimePlatform.IPhonePlayer:
                    return "iOS";

                case RuntimePlatform.LinuxPlayer:
                case RuntimePlatform.LinuxEditor:
                    return "StandaloneLinux64";

                case RuntimePlatform.WebGLPlayer:
                    return "WebGL";

                case RuntimePlatform.WSAPlayerX86:
                case RuntimePlatform.WSAPlayerX64:
                case RuntimePlatform.WSAPlayerARM:
                    return "WSAPlayer";

                case RuntimePlatform.Android:
                case RuntimePlatform.PS4:
                case RuntimePlatform.XboxOne:
                case RuntimePlatform.tvOS:
                case RuntimePlatform.Switch:
                //case RuntimePlatform.Lumin:
                //case RuntimePlatform.Stadia:
                    return Application.platform.ToString();
            }


            return Application.platform.ToString();
#endif

        }

        /// <summary>
        /// 计算字符串的MD5值
        /// </summary>
        public static string md5(string source)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] data = System.Text.Encoding.UTF8.GetBytes(source);
            byte[] md5Data = md5.ComputeHash(data, 0, data.Length);
            md5.Clear();

            string destString = "";
            for (int i = 0; i < md5Data.Length; i++)
            {
                destString += System.Convert.ToString(md5Data[i], 16).PadLeft(2, '0');
            }
            destString = destString.PadLeft(32, '0');
            return destString;
        }



        /// <summary>
        /// 计算文件的MD5值
        /// </summary>
        public static string md5file(string file)
        {
            try
            {
                FileStream fs = new FileStream(file, FileMode.Open);
                System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
                byte[] retVal = md5.ComputeHash(fs);
                fs.Close();

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < retVal.Length; i++)
                {
                    sb.Append(retVal[i].ToString("x2"));
                }
                return sb.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("md5file() fail, error:" + ex.Message);
            }
        }

       

    }

}

