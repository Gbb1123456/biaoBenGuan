using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace ZXKFramework
{
    public class MoveObjItem
    {
        Tweener posTweener;
        Tweener roaTweener;
        Tweener locTweener;

        public void Init(Transform _base, Transform _target, float time)
        {
            posTweener = _base?.DOLocalMove(_target.localPosition, time).SetEase(Ease.Linear).SetLoops(-1);
            roaTweener = _base?.DOLocalRotate(_target.localEulerAngles, time).SetEase(Ease.Linear).SetLoops(-1);
            locTweener = _base?.DOScale(_target.localScale, time).SetEase(Ease.Linear).SetLoops(-1);
            posTweener.SetAutoKill(false);
            roaTweener.SetAutoKill(false);
            locTweener.SetAutoKill(false);
            Pause();
        }

        public void Move(bool move = false)
        {
            Pause();
            if (move)
            {
                posTweener?.PlayForward();
                roaTweener?.PlayForward();
                locTweener?.PlayForward();
            }
            else
            {
                posTweener?.PlayBackwards();
                roaTweener?.PlayBackwards();
                locTweener?.PlayBackwards();
            }
        }

        public void Pause()
        {
            posTweener?.Pause();
            roaTweener?.Pause();
            locTweener?.Pause();
        }
    }
}

