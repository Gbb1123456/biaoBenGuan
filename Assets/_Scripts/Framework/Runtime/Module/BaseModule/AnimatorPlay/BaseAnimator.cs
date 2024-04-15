using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class BaseAnimator : MonoBehaviour
    {
        public int animatorID = 0;
        public int nextAnimatorID = 0;
        public bool isPlayNext = false;

        public virtual void Play()
        {
            PlayStart();
        }

        public virtual void PlayStart()
        {

        }

        public virtual void PlayNext()
        {
            if (isPlayNext)
            {
                Game.Instance.animatorManager.Play(nextAnimatorID);
            }
        }
    }
}