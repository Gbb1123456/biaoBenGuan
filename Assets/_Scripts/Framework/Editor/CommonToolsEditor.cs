using System.IO;
using UnityEditor;
using UnityEngine;

namespace ZXKFrameworkEditor
{
    /// <summary>
    /// 通用工具
    /// </summary>
    public class CommonToolsEditor : Editor
    {
        [MenuItem("ZXKFramework/Tools/CleanWithMissingScript")]
        static void CleanupMissingScripts()
        {
            //使用方法  选择物体，右键--去除无效脚本即可，
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                //删除当前选择的物体以及子物体、孙子物体等所有物体身上的空脚本
                var gameObject = Selection.gameObjects[i];
                GameObjectUtility.RemoveMonoBehavioursWithMissingScript(gameObject);
                Transform[] transforms = gameObject.GetComponentsInChildren<Transform>(true);
                for (int j = 0; j < transforms.Length; j++)
                {
                    GameObjectUtility.RemoveMonoBehavioursWithMissingScript(transforms[j].gameObject);
                }
            }
        }

        [MenuItem("ZXKFramework/Tools/RefreshAsset")]
        public static void Refresh()
        {
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/Tools/PlayerPrefsDeleteAll")]
        public static void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }

        [MenuItem("ZXKFramework/Tools/CreateCommonFile")]
        private static void CreateTextureFile()
        {
            CreateDirectory(Application.dataPath + "/_Scenes");
            CreateDirectory(Application.dataPath + "/_Scripts");
            CreateDirectory(Application.dataPath + "/Art");
            CreateDirectory(Application.dataPath + "/StreamingAssets");
            CreateDirectory(Application.dataPath + "/Resources");
            CreateDirectory(Application.dataPath + "/Third");
            CreateDirectory(Application.dataPath + "/Plugins");         
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/Tools/LogVersion")]
        private static void LogVersion()
        {
            Debug.Log(Application.version);
        }

        [MenuItem("ZXKFramework/Tools/LogPcSystem")]
        private static void LogPcSystem()
        {
            Debug.Log(systemInfo());
        }

        //创建文件夹
        public static void CreateDirectory(string destFileName)
        {
            if (!Directory.Exists(destFileName))
                Directory.CreateDirectory(destFileName);
        }

        /// <summary>
        /// 新建文本并且写入
        /// </summary>
        /// <param name="path"></param>
        /// <param name="info"></param>
        static void CreateTxt(string path, string info)
        {
            StreamWriter sw;
            FileInfo fi = new FileInfo(path);
            //直接重新写入，如果要在原文件后面追加内容，应用fi.AppendText()
            sw = fi.CreateText();
            sw.WriteLine(info);
            sw.Close();
            sw.Dispose();
        }

        public static string systemInfo()
        {
            string info =
                 "Title:当前系统基础信息：" +
                 "\n设备模型：" + SystemInfo.deviceModel +
                 "\n设备名称：" + SystemInfo.deviceName +
                 "\n设备类型：" + SystemInfo.deviceType +
                 "\n设备唯一标识符：" + SystemInfo.deviceUniqueIdentifier +
                 "\n显卡标识符：" + SystemInfo.graphicsDeviceID +
                 "\n显卡设备名称：" + SystemInfo.graphicsDeviceName +
                 "\n显卡厂商：" + SystemInfo.graphicsDeviceVendor +
                 "\n显卡厂商ID:" + SystemInfo.graphicsDeviceVendorID +
                 "\n显卡支持版本:" + SystemInfo.graphicsDeviceVersion +
                 "\n显存（M）：" + SystemInfo.graphicsMemorySize +
                 "\n显卡像素填充率(百万像素/秒)，-1未知填充率：" + SystemInfo.graphicsPixelFillrate +
                 "\n显卡支持Shader层级：" + SystemInfo.graphicsShaderLevel +
                 "\n支持最大图片尺寸：" + SystemInfo.maxTextureSize +
                 "\nnpotSupport：" + SystemInfo.npotSupport +
                 "\n操作系统：" + SystemInfo.operatingSystem +
                 "\nCPU处理核数：" + SystemInfo.processorCount +
                 "\nCPU类型：" + SystemInfo.processorType +
                 "\nsupportedRenderTargetCount：" + SystemInfo.supportedRenderTargetCount +
                 "\nsupports3DTextures：" + SystemInfo.supports3DTextures +
                 "\nsupportsAccelerometer：" + SystemInfo.supportsAccelerometer +
                 "\nsupportsComputeShaders：" + SystemInfo.supportsComputeShaders +
                 "\nsupportsGyroscope：" + SystemInfo.supportsGyroscope +
                 "\nsupportsImageEffects：" + SystemInfo.supportsImageEffects +
                 "\nsupportsInstancing：" + SystemInfo.supportsInstancing +
                 "\nsupportsLocationService：" + SystemInfo.supportsLocationService +
                 "\nsupportsRenderTextures：" + SystemInfo.supportsRenderTextures +
                 "\nsupportsRenderToCubemap：" + SystemInfo.supportsRenderToCubemap +
                 "\nsupportsShadows：" + SystemInfo.supportsShadows +
                 "\nsupportsSparseTextures：" + SystemInfo.supportsSparseTextures +
                 "\nsupportsStencil：" + SystemInfo.supportsStencil +
                 "\nsupportsVertexPrograms：" + SystemInfo.supportsVertexPrograms +
                 "\nsupportsVibration：" + SystemInfo.supportsVibration +
                 "\n内存大小：" + SystemInfo.systemMemorySize;
            return info;
        }
    }
}
