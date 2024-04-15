using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class BaseAnimation : MonoBehaviour
    {
        public int animationID = 0;
        public int nextAnimatorID = 0;
        public bool isPlayNext = false;

        public virtual void Play()
        {
            PlayStart();
        }

        public virtual void PlayStart()
        {

        }
        public virtual void StopAnim()
        { 
        
        }
        public virtual void PlayNext()
        {
            Game.Instance.animatorManager.Play(nextAnimatorID);
        }
    }
}