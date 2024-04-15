using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZXKFramework
{
    public class AbPath
    {
        public string GetStreamingAssetsPath(string progectName)
        {
            if (progectName.IsNull()) return "";
            return "/Assetbundle/" + progectName + "/" + GetPlatform() + "/_StreamingAssets/";
        }

        public string GetScenePath(string progectName)
        {
            if (progectName.IsNull()) return "";
            return "/Assetbundle/" + progectName + "/" + GetPlatform() + "/_scenes/";
        }

        public string GetResourcesPath(string progectName)
        {
            if (progectName.IsNull()) return "";
            return "/Assetbundle/" + progectName + "/" + GetPlatform() + "/resources/";
        }

        string GetPlatform()
        {
            string pos = "";
#if UNITY_ANDROID
            pos = "Android";
#elif UNITY_IOS
            pos = "IOS";
#elif UNITY_EDITOR_WIN
            pos = "Windows";
#endif
            return pos;
        }

//        string GetPath(string theName)
//        {
//            string pos = "";
//#if UNITY_ANDROID
//            pos = "Android";
//#elif UNITY_IOS
//            pos = "IOS";
//#elif UNITY_EDITOR_WIN
//            pos = "Windows";
//#endif
//            if (string.IsNullOrEmpty(mData.path))
//            {
//                mData.path = Application.persistentDataPath;
//            }
//            string thePath = mData.path + "/Assetbundle/" + pos;
//            if (!Directory.Exists(thePath))
//                Directory.CreateDirectory(thePath);
//            thePath = thePath + "/" + theName.ToLower() + ".assetbundle";
//            return thePath;
//        }
    }
}