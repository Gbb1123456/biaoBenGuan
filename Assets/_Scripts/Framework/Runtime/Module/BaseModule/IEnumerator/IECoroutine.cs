
using System.Collections;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public class IECoroutine : IIECoroutine
    {
        public Coroutine Run(IEnumerator ie)
        {
            return Game.Instance?.StartCoroutine(ie);
        }

        public Coroutine Run(float time, Action callBack)
        {
            return Run(WaitingToDo(time, callBack));
        }

        public Coroutine Run(Action callBack)
        {
            return Run(IEFuncNull(callBack));
        }

        public Coroutine RunWaitForEndOfFrame(Action callBack)
        {
            return Run(IEFunc(callBack));
        }

        public void Stop(Coroutine con)
        {
            if (con != null)
            {
                Game.Instance?.StopCoroutine(con);
            }
        }

        public void StopAll()
        {
            Game.Instance?.StopAllCoroutines();
        }

        static IEnumerator WaitingToDo(float time, Action callBack)
        {
            yield return new WaitForSeconds(time);
            callBack?.Invoke();
        }

        public IEnumerator IEFunc(Action callBack)
        {
            yield return new WaitForEndOfFrame();
            callBack?.Invoke();
        }

        public IEnumerator IEFuncNull(Action callBack)
        {
            yield return null;
            callBack?.Invoke();
        }
    }

    public class IEnumeratorRestart
    {
        private Coroutine loCoroutine = null;
        public void RunRestart(Coroutine ie)
        {
            Stop();
            loCoroutine = ie;
        }
        public void Stop()
        {
            if (loCoroutine != null)
            {
                Game.Instance?.StopCoroutine(loCoroutine);
            }
        }
    }
}
