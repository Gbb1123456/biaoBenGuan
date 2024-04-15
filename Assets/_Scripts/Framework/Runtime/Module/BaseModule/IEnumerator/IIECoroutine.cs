using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public interface IIECoroutine
    {
        Coroutine Run(IEnumerator ie);
        Coroutine Run(Action callBack);
        Coroutine Run(float time, Action callBack);
        Coroutine RunWaitForEndOfFrame(Action callBack);
        void Stop(Coroutine con);
        void StopAll();
    }
}
