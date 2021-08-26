using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class OutputSprite : MonoBehaviour
{
    //把切割图保存复制保存在对应目录下
    [MenuItem("Tool/导出图片精灵")]
    static void SaveSprite()
    {
        string resourcesPath = "Assets/Resources/";
        //再选中图中遍历
        foreach(Object obj in Selection.objects)
        {
            string selectionPath = AssetDatabase.GetAssetPath(obj);

            //判断selectionPath是否以resourcesPath开始
            if (selectionPath.StartsWith(resourcesPath))
            {
                //从尾部开始查找句点 获取拓展名
                string selectionExt = System.IO.Path.GetExtension(selectionPath);
                if(selectionExt.Length == 0)
                {
                    continue;
                }

                //去除后缀
                string loadPath = selectionPath.Remove(selectionPath.Length - selectionExt.Length);
                //去除资源路径resourcesPath
                loadPath = loadPath.Substring(resourcesPath.Length);

                //加载此文件下所有资源
                Sprite[] sprites = Resources.LoadAll<Sprite>(loadPath);

                if(sprites.Length > 0)
                {
                    //创建导出文件夹
                    string outPath = Application.dataPath + "/Resources/" + loadPath;
                    System.IO.Directory.CreateDirectory(outPath);

                    foreach(Sprite sprite in sprites)
                    {
                        //创建纹理
                        Texture2D tex = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height, TextureFormat.RGB24, false);
                        tex.SetPixels(sprite.texture.GetPixels((int)sprite.rect.xMin, (int)sprite.rect.yMin, (int)sprite.rect.width, (int)sprite.rect.height));
                        tex.Apply();

                        //写入PNG文件
                        System.IO.File.WriteAllBytes(outPath + "/" + sprite.name + ".png", tex.EncodeToPNG());
                    }
                    Debug.Log("Savesprite to" + outPath);
                }
            }
        }
        Debug.Log("Savesprite Finished");
    }
}
