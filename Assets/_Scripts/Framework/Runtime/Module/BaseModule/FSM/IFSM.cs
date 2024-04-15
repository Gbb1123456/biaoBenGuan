using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public interface IFSM
    {
        void AddState<T>() where T : StateBase, new();

        void ChangeState<T>(params object[] obj) where T : StateBase, new();

        void Update();

        void Destroy();
    }
}