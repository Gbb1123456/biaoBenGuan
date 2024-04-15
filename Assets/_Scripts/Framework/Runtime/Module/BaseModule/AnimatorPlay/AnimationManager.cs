using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

public class AnimationManager : IAnimatorManager
{
    public Dictionary<int, BaseAnimation> allAnimations = new Dictionary<int, BaseAnimation>();

    public void Init(GameObject animationParent)
    {
        foreach (Transform trs in animationParent.transform)
        {
            BaseAnimation loBaseAnimation = trs.GetComponent<BaseAnimation>();
            if (loBaseAnimation != null)
            {
                if (!allAnimations.ContainsKey(loBaseAnimation.animationID))
                {
                    allAnimations.Add(loBaseAnimation.animationID, loBaseAnimation);
                }
                else
                {
                    Debug.LogError("已经包含动画ID 无法再次添加 " + loBaseAnimation.animationID);
                }
            }
        }
    }

    public void Play(int id)
    {
        if (allAnimations.ContainsKey(id))
        {
            allAnimations[id].Play();
        }
    }
}