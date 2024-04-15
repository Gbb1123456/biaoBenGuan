using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public interface IAnimatorManager
    {
        void Init(GameObject animatorParent);
        void Play(int id);
    }
}
