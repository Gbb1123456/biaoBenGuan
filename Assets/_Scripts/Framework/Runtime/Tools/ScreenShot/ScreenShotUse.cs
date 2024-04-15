using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;
public class ScreenShotUse : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            string path = "ScreenShot";
            ScreenShot.ShotSaveToPersistentDataPath(m => { }, path);
            UnityTools.OpenDirectory(Application.persistentDataPath + "/" + path);
        }
    }
}
