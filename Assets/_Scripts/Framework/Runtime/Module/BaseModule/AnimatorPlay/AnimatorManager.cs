using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class AnimatorManager : IAnimatorManager
    {
        public Dictionary<int, BaseAnimator> allAnimators = new Dictionary<int, BaseAnimator>();

        public void Init(GameObject animatorParent)
        {
            foreach (Transform trs in animatorParent.transform)
            {
                BaseAnimator loBaseAnimator = trs.GetComponent<BaseAnimator>();
                if (loBaseAnimator != null)
                {
                    if (!allAnimators.ContainsKey(loBaseAnimator.animatorID))
                    {
                        allAnimators.Add(loBaseAnimator.animatorID, loBaseAnimator);
                    }
                    else
                    {
                        Debug.LogError("已经包含动画ID 无法再次添加 " + loBaseAnimator.animatorID);
                    }
                }
            }
        }

        public void Play(int id)
        {
            if (allAnimators.ContainsKey(id))
            {
                allAnimators[id].Play();
            }
        }
    }
}
