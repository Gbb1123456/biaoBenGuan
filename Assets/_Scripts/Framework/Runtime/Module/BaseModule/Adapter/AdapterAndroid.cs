using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class AdapterAndroid : IAdapter
    {
        public void Init()
        {
            Debug.Log("当前平台：Android");
        }
    }
}


