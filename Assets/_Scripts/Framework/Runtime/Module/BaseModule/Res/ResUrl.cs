using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace ZXKFramework
{
    public class ResUrl : IRes
    {
        public void Load<T>(string assetName, Action<T> action) where T : Object
        {
            
        }

        public void UnLoadAll()
        {
            
        }
    }
}

