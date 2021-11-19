using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//继承monobehaviour的泛型单例
public class SingletonAutoMono<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T GetInstace()
    {
        if(instance)
        {
            GameObject obj = new GameObject();
            obj.name = typeof(T).ToString();
            DontDestroyOnLoad(obj);
            instance = obj.AddComponent<T>();
        }
        return instance;
    }
}
