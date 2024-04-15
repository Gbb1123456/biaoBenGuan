using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace ZXKFramework
{
    public class ResAssetBundleSimple : IRes
    {
        Dictionary<string, AbItem> allAbItems = new Dictionary<string, AbItem>();

        public void Load<T>(string assetName, Action<T> action = null) where T : Object
        {
            var loRes = Load<T>(assetName);
            action?.Invoke(loRes);
        }

        private T Load<T>(string data) where T : Object
        {
            if (!allAbItems.ContainsKey(data))
            {
                AbItem temp = new AbItem();
                temp.Init(data);
                allAbItems.Add(data, temp);
                return temp.LoadAsset<T>(data);
            }
            else
            {
                return allAbItems[data].LoadAsset<T>(data);
            }
        }

        public void UnLoadAll()
        {
            foreach (var o in allAbItems.Values)
            {
                o.asset.Unload(true);
            }
            allAbItems.Clear();
        }
    }

    public class AbItem
    {
        AbPath abPath = new AbPath();
        public AssetBundle asset { get; set; }

        public void Init(string data)
        {
            if (AbData.progectName.IsNull()) 
            {
                Debug.LogError("AbData.progectName is null");
                return;
            }
            string path = GetPath(AbData.progectName, data);
            if (!File.Exists(path))
            {
                Debug.Log("加载AB物体不存在：" + path);
                return;
            }
            asset = AssetBundle.LoadFromFile(path);
            asset.Unload(false);         
        }

        public T LoadAsset<T>(string data) where T : Object
        {
            return asset.LoadAsset<T>(Path.GetFileNameWithoutExtension(data).ToLower());
        }

        string GetPath(string progectName, string theName)
        {
            return abPath.GetResourcesPath(progectName) + theName;
        }
    }
}