using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Networking;
using Object = UnityEngine.Object;

namespace ZXKFramework
{
    public class ResAssetBundle : IRes
    {
        private Dictionary<string, AssetBundle> abDic = new Dictionary<string, AssetBundle>();

        //主包
        private AssetBundle mainAB = null;

        //ab包的存放路径，方便修改，web端不能放在streaming assets 文件夹下，因为会导致无法加载
        private string PathUrl
        {
            get
            {
#if UNITY_IOS
                    return Application.streamingAssetsPath+"/";
#elif UNITY_ANDROID
                    return Application.streamingAssetsPath+"/";
#elif UNITY_WEBGL
                    return Application.dataPath + "/AssetBundles/WebGL/";
#elif UNITY_EDITOR
                    return Application.streamingAssetsPath + "/";
#else
                    return Application.streamingAssetsPath+"/";
#endif
            }
        }

        private string mainABName
        {
            get
            {
#if UNITY_IOS
                    return "IOS";
#elif UNITY_ANDROID
                    return "Android";
#elif UNITY_WEBGL
                    return "WebGL";
#else
                    return "PC";
#endif
            }
        }

        ////web加载依赖
        //public IEnumerator LoadWebDependency(string abName)
        //{
        //    Loading.Instance.titleText.gameObject.SetActive(true);
        //    Loading.Instance.downloadText.gameObject.SetActive(true);
        //    if (mainAB == null)
        //    {
        //        //加载主包
        //        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(PathUrl + mainABName);
        //        Loading.Instance.downloadText.text = "正在下载主包";
        //        yield return request.SendWebRequest();
        //        mainAB = DownloadHandlerAssetBundle.GetContent(request);
        //        manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //        Loading.Instance.UpdateProgress(0.1f);
        //    }
        //    AssetBundle ab = null;
        //    //获取依赖包相关信息
        //    string[] strs = manifest.GetAllDependencies(abName);
        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        if (!abDic.ContainsKey(strs[i]))
        //        {
        //            //加载依赖包
        //            UnityWebRequest request1 = UnityWebRequestAssetBundle.GetAssetBundle(PathUrl + strs[i]);
        //            Loading.Instance.downloadText.text = "正在下载依赖包" + strs[i];
        //            yield return request1.SendWebRequest();
        //            ab = DownloadHandlerAssetBundle.GetContent(request1);
        //            abDic.Add(strs[i], ab);
        //            Loading.Instance.UpdateProgress(0.1f * (i + 1f));
        //        }
        //    }
        //}

        ////非web加载依赖
        //public void LoadDependency(string abName)
        //{
        //    if (mainAB == null)
        //    {
        //        Debug.Log(PathUrl + mainABName);
        //        //加载主包
        //        mainAB = AssetBundle.LoadFromFile(PathUrl + mainABName);
        //        //加载主包的Manifest
        //        manifest = mainAB.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
        //    }
        //    AssetBundle ab = null;
        //    //获取依赖包相关信息
        //    string[] strs = manifest.GetAllDependencies(abName);
        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        if (!abDic.ContainsKey(strs[i]))
        //        {
        //            //加载依赖包
        //            ab = AssetBundle.LoadFromFile(PathUrl + strs[i]);
        //            abDic.Add(strs[i], ab);
        //        }
        //    }
        //    //加载目标包
        //    if (!abDic.ContainsKey(abName))
        //    {
        //        ab = AssetBundle.LoadFromFile(PathUrl + abName);
        //        abDic.Add(abName, ab);
        //    }
        //}

        ////同步加载 泛型方式
        //public T LoadRes<T>(string abName, string resName) where T : Object
        //{
        //    LoadDependency(abName);
        //    return abDic[abName].LoadAsset<T>(resName);
        //}

        ////同步加载 字符串名称方式
        //public Object LoadRes(string abName, string resName)
        //{
        //    LoadDependency(abName);
        //    return abDic[abName].LoadAsset(resName);
        //}

        ////同步加载 Type方式
        //public Object LoadRes(string abName, string resName, System.Type type)
        //{
        //    LoadDependency(abName);
        //    return abDic[abName].LoadAsset(resName, type);
        //}

        ////异步加载 （这里的加载是指的异步加载资源而非异步加载AB包 ） 字符串名称方式
        //public void LoadResAsync(string abName, string resName, UnityAction<Object> callBack)
        //{
        //    Game.Instance.StartCoroutine(RealLoadResAsync(abName, resName, callBack));
        //}

        //public IEnumerator RealLoadResAsync(string abName, string resName, UnityAction<Object> callBack)
        //{
        //    LoadDependency(abName);
        //    AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName);
        //    yield return abr;
        //    callBack(abr.asset);
        //}

        ////异步加载 （这里的加载是指的异步加载资源而非异步加载AB包） 泛型方式
        //public void LoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
        //{
        //    Game.Instance.StartCoroutine(RealLoadResAsync(abName, resName, callBack));
        //}

        //public IEnumerator RealLoadResAsync<T>(string abName, string resName, UnityAction<T> callBack) where T : Object
        //{
        //    LoadDependency(abName);
        //    AssetBundleRequest abr = abDic[abName].LoadAssetAsync<T>(resName);
        //    yield return abr;
        //    callBack(abr.asset as T);
        //}

        ////异步加载 （这里的加载是指的异步加载资源而非异步加载AB包） System.Type方式
        //public void LoadResAsync<T>(string abName, string resName, System.Type type, UnityAction<Object> callBack) where T : Object
        //{
        //    Game.Instance.StartCoroutine(RealLoadResAsync(abName, resName, type, callBack));
        //}

        //public IEnumerator RealLoadResAsync(string abName, string resName, System.Type type, UnityAction<Object> callBack)
        //{
        //    LoadDependency(abName);
        //    AssetBundleRequest abr = abDic[abName].LoadAssetAsync(resName, type);
        //    yield return abr;
        //    callBack(abr.asset);
        //}

        //public void LoadSceneFromWeb(string abName, string sceneName, UnityAction<string> callBack)
        //{
        //    Game.Instance.StartCoroutine(ILoadSceneFromWeb(abName, sceneName, callBack));
        //}

        ////web加载场景（非web也能用）
        //public IEnumerator ILoadSceneFromWeb(string abName, string sceneName, UnityAction<string> callBack)
        //{
        //    // 显示进度条
        //    Loading.Instance.Show();
        //    Loading.Instance.UpdateProgress(0);
        //    //加载依赖
        //    yield return Game.Instance.StartCoroutine(LoadWebDependency(abName));

        //    if (!abDic.ContainsKey(abName))
        //    {
        //        using (UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(PathUrl + abName))
        //        {
        //            AssetBundle ab;
        //            Loading.Instance.downloadText.text = "正在下载3D场景包" + abName;
        //            float temp = Loading.Instance.slider.value;
        //            //yield return request.SendWebRequest();
        //            request.SendWebRequest();
        //            while (!request.isDone)
        //            {
        //                Loading.Instance.UpdateProgress((request.downloadProgress * (1 - temp)) + temp);
        //                yield return null;
        //            }
        //            if (request.isDone)
        //            {
        //                Loading.Instance.UpdateProgress(1f);
        //                Loading.Instance.titleText.gameObject.SetActive(false);
        //                Loading.Instance.downloadText.gameObject.SetActive(false);
        //            }
        //            ab = DownloadHandlerAssetBundle.GetContent(request);
        //            abDic.Add(abName, ab);
        //        }
        //    }
        //    string[] strs = abDic[abName].GetAllScenePaths();
        //    for (int i = 0; i < strs.Length; i++)
        //    {
        //        if (sceneName == Path.GetFileNameWithoutExtension(strs[i]))
        //        {
        //            callBack(strs[i]);
        //        }
        //    }
        //}

        ////单个包卸载
        //public void UnLoad(string abName)
        //{
        //    if (abDic.ContainsKey(abName))
        //    {
        //        abDic[abName].Unload(false);
        //        abDic.Remove(abName);
        //    }
        //}

        ////所有包的卸载
        //public void ClearAB()
        //{
        //    AssetBundle.UnloadAllAssetBundles(false);
        //    abDic.Clear();
        //    mainAB = null;
        //    manifest = null;
        //}

        public void Load<T>(string assetName, Action<T> action) where T : UnityEngine.Object
        {
            
        }

        public void UnLoadAll()
        {

        }
    }
}

