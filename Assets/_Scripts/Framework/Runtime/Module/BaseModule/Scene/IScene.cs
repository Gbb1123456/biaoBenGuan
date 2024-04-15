using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public interface IScene
    {
        void LoadLevel(int level);
        void LoadLevel(string level);
        void LoadABScene(string sceneName, Action<bool, string> callBack);
        void LoadABScene(string path,string sceneName, Action<bool, string> callBack);
    }
}
