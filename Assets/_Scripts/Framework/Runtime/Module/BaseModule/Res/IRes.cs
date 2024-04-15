using UnityEngine;
using System;
using Object = UnityEngine.Object;
namespace ZXKFramework
{
    public interface IRes
    {
        void Load<T>(string assetName, Action<T> action = null) where T : Object;
        void UnLoadAll();
    }
}
