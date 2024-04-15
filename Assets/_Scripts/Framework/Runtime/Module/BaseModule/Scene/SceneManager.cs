using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public class SceneManager : IScene
    {
        AssetBundleCreateRequest bundleTemp;
        AsyncOperation async;

        public void LoadLevel(int level)
        {
            FrameworkScenesArgs e = new FrameworkScenesArgs()
            {
                scnesIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex
            };
            Game.Instance?.SendEvent(FrameworkConsts.E_ExitScenes, e);

            UnityEngine.SceneManagement.SceneManager.LoadScene(level, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        public void LoadLevel(string level)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level, UnityEngine.SceneManagement.LoadSceneMode.Single);
        }

        public void LoadABScene(string path, string sceneName, Action<bool, string> callBack)
        {
            Game.Instance?.IEnumeratorManager.Run(IeLoadABScene(path, sceneName, callBack));
        }

        public void LoadABScene(string sceneName, Action<bool, string> callBack)
        {
            string path = Application.streamingAssetsPath + "/" + sceneName.ToLower() + ".assetbundle";
            Game.Instance?.IEnumeratorManager.Run(IeLoadABScene(path, sceneName, callBack));
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
