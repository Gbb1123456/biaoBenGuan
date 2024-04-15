using System;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace ZXKFramework
{
    public class ResResources : IRes
    {
        public void Load<T>(string assetName, Action<T> action) where T : Object
        {
            try
            {
                T t = Resources.Load<T>(assetName);
                if (t != null)
                {
                    action?.Invoke(t);
                }
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public void UnLoadAll()
        {

        }
    }
}

