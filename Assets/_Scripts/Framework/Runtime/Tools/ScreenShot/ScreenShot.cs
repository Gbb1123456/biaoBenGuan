using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Collections.Generic;

namespace ZXKFramework
{
    /// <summary>
    /// 屏幕截图
    /// </summary>
    public static class ScreenShot
    {
        public static void ShotSaveToPersistentDataPath(Action<Sprite> action_End = null, string savePath = "ScreenShot", List<GameObject> shouldHideObj = null)
        {
            savePath = Application.persistentDataPath + "/" + savePath + "/";
            Game.Instance.StartCoroutine(OnScreenShot(action_End, savePath, true, shouldHideObj));
        }

        public static void Shot(Action<Sprite> action_End = null, bool saveUseable = false, string savePath = "ScreenShot", List<GameObject> shouldHideObj = null)
        {
            ObjShow(shouldHideObj, false);
            Game.Instance.StartCoroutine(OnScreenShot(action_End, savePath, saveUseable, shouldHideObj));
        }

        static IEnumerator OnScreenShot(Action<Sprite> action_End, string savePath, bool saveUseable, List<GameObject> shouldHideObj)
        {
            Texture2D texture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
            yield return new WaitForEndOfFrame();
            texture.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
            texture.Apply();

            Sprite sprite = Sprite.Create(texture,
                new Rect(0, 0, texture.width, texture.height),
                new Vector2(0.5f, 0.5f));

            if (saveUseable)
            {
                byte[] bytes = texture.EncodeToPNG();
                if (!Directory.Exists((savePath)))
                {
                    Directory.CreateDirectory(savePath);
                }
                string currentTime = string.Format("{0:yyyyMMddHHmmssffff}", DateTime.Now);
                string path_save = savePath + "/IMG_" + currentTime + ".png";
                Debug.Log(path_save);
                File.WriteAllBytes(path_save, bytes);
            }

            ObjShow(shouldHideObj, true);

            action_End?.Invoke(sprite);
        }

        static void ObjShow(List<GameObject> hideObj, bool isShow)
        {
            if (hideObj != null && hideObj.Count > 0)
            {
                for (int i = 0; i < hideObj.Count; i++)
                {
                    hideObj[i].SetActiveSafe(isShow);
                }
            }
        }
    }
}