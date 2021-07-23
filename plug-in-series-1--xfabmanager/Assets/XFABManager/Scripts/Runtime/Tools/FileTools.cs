using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace XFABManager
{
    public class FileTools
    {

        /// <summary>
        /// 复制一个文件夹
        /// </summary>
        /// <param name="sourceDir">源文件夹</param>
        /// <param name="destDir">目标文件夹</param>
        /// <param name="progress">进度改变的回调 第一个参数是正在复制的文件名称 第二个是复制的进度</param>
        public static bool CopyDirectory(string sourceDir, string destDir,Action<string,float> progress)
        {
            // If the destination directory doesn't exist, create it.
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            if (!Directory.Exists(sourceDir)) {
                Debug.LogError( string.Format( " 复制失败 ! 源目录{0}不存在! ",sourceDir) );
                return false;
            }
             
            foreach (string folderPath in Directory.GetDirectories(sourceDir, "*", SearchOption.AllDirectories))
            {
                if (!Directory.Exists(folderPath.Replace(sourceDir, destDir)))
                    Directory.CreateDirectory(folderPath.Replace(sourceDir, destDir));
            }
            string[] files = Directory.GetFiles(sourceDir, "*.*", SearchOption.AllDirectories);
            //string filePath = null;
            for (int i = 0; i < files.Length; i++)
            {
                //filePath = files[i];
                var fileDirName = Path.GetDirectoryName(files[i]).Replace("\\", "/");
                var fileName = Path.GetFileName(files[i]);
                string newFilePath = Path.Combine(fileDirName.Replace(sourceDir, destDir), fileName);

                progress?.Invoke(fileName, (float)(i + 1) / (float)files.Length);

                File.Copy(files[i], newFilePath, true);
            }

            return true;
        }

        /// <summary>
        /// 复制一个文件
        /// </summary>
        /// <returns></returns>
        public static IEnumerator Copy(string source,string des) {
            yield return null;
            string path = Path.GetDirectoryName(des);

            if (! Directory.Exists(path) ) {
                Directory.CreateDirectory(path);
            }
#if UNITY_ANDROID && !UNITY_EDITOR
            UnityWebRequest copyFile = UnityWebRequest.Get(source);
            yield return copyFile.SendWebRequest();
            File.WriteAllBytes(des, copyFile.downloadHandler.data);
#else
            File.Copy(source, des, true);
#endif
        }

 

    }

}

