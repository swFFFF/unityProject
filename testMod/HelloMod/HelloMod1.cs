using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BepInEx;
using UnityEngine;
using HarmonyLib;

namespace HelloMod
{
    [BepInPlugin("me.Swift.plugin.HelloMod", "HelloWorld", "1.0.0")]
    public class HelloMod1 : BaseUnityPlugin
    {
        void Start()
        {
            Debug.Log("Unity:HelloMod");
            Logger.LogInfo("Unity:HelloMod1");
            Harmony.CreateAndPatchAll(typeof(HelloMod1));
        }

        void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha9))
            {
                AkLogger.Warning("test warnning");
            }
        }

        /// <summary>
        /// 前置补丁 返回值bool 是否去除原函数 方法参数名要和原方法一一对应
        /// </summary>
        /// <param name="message"></param>
        [HarmonyPrefix, HarmonyPatch(typeof(AkLogger), "Warning")]
        public static bool AkLogger_Warning_PrePatch(ref string message)
        {
            message = "hahaha";
            return true;
        }

        /// <summary>
        /// 后置补丁 AkLogger_Warning_PrePatch 返回false 也会执行
        /// </summary>
        /// <param name="__instance"></param>
        /// <param name="message"></param>
        [HarmonyPostfix, HarmonyPatch(typeof(AkLogger), "Warning")]
        public static void AkLogger_Warning_PostPatch(AkLogger __instance, string message)
        {
            Debug.Log($"AkLogger_Warning_PostPatch{ message }");
        }
    }
}
