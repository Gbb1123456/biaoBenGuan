using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using Object = UnityEngine.Object;

namespace ZXKFramework
{
    public class ResStreamingAssets : IRes
    {
        public void GetTexture<T>(string path, Action<T> action) where T : Object
        {
            try
            {
                Debug.Log(path);
                Game.Instance.StartCoroutine(TextureReader(path, action));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        IEnumerator TextureReader<T>(string path, Action<T> action) where T : Object
        {
            using (UnityWebRequest unityWebRequest = UnityWebRequestTexture.GetTexture(path))
            {
                yield return unityWebRequest.SendWebRequest();
                if (string.IsNullOrEmpty(unityWebRequest.error))
                {
                    action?.Invoke(UnityTools.ToSprite(DownloadHandlerTexture.GetContent(unityWebRequest)) as T);
                }
            };
        }

        public void GetAudioClip<T>(string path, Action<T> action) where T : Object
        {
            try
            {
                Debug.Log(path);
                Game.Instance.StartCoroutine(AudioClipReader(path, action));
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        IEnumerator AudioClipReader<T>(string path, Action<T> action) where T : Object
        {
            using (UnityWebRequest webRequest = UnityWebRequestMultimedia.GetAudioClip(path, AudioType.WAV))
            {
                yield return webRequest.SendWebRequest();
                if (string.IsNullOrEmpty(webRequest.error))
                {
                    action?.Invoke(DownloadHandlerAudioClip.GetContent(webRequest) as T);
                }
            };
        }

        public void Load<T>(string assetName, Action<T> action) where T : Object
        {
            string tipType = typeof(T).Name;
            switch (tipType)
            {
                case "Sprite":
                    GetTexture<T>(Application.streamingAssetsPath + "/" + assetName + ".png", action);
                    break;
                case "AudioClip":
                    GetAudioClip<T>(Application.streamingAssetsPath + "/" + assetName + ".wav", action);
                    break;
                case "GameObject":
                    break;
            }
        }

        public void UnLoadAll()
        {

        }
    }
}

