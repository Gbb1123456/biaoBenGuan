using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
namespace ZXKFramework
{
    //AB包加载
    public class AbLoad
    {
        AbPath abPath = new AbPath();
        AssetBundleCreateRequest bundleTemp;
        AsyncOperation async;

        //加载场景
        public void LoadABScene(string sceneName, Action<bool, string> callBack)
        {
            if (AbData.progectName.IsNull())
            {
                Debug.LogError("AbData.progectName is null");
                return;
            }
            string loPath = Application.streamingAssetsPath + abPath.GetScenePath(AbData.progectName) + sceneName.ToLower() + ".assetbundle";
            Game.Instance?.IEnumeratorManager.Run(IeLoadABScene(loPath, sceneName, callBack));
        }

        //场景加载
        IEnumerator IeLoadABScene(string path, string sceneName, Action<bool, string> callBack)
        {
            FileInfo info = new FileInfo(path);
            if (info.Exists)
            {
                bundleTemp = AssetBundle.LoadFromFileAsync(path);
                yield return bundleTemp;
                async = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);
                yield return async;
                callBack?.Invoke(true, "加载场景成功");
            }
            else
            {
                callBack?.Invoke(false, "AB加载场景 资源不存在 " + path);
            }
        }
    }
}