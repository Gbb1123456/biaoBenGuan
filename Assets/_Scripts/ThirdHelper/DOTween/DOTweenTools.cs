using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace ZXKFramework
{
    public class DOTweenTools
    {
        public static void Move(Transform _base, Transform _target, float time, bool isLoop = false)
        {
            _base.DOLocalMove(_target.localPosition, time);
            _base.DOLocalRotate(_target.localEulerAngles, time);
            _base.DOScale(_target.localScale, time);
        }

        public static void MoveLoopAndLiner(Transform _base, Transform _target, float time)
        {
            _base.DOLocalMove(_target.localPosition, time).SetLoops(-1).SetEase(Ease.Linear);
            _base.DOLocalRotate(_target.localEulerAngles, time).SetLoops(-1).SetEase(Ease.Linear);
            _base.DOScale(_target.localScale, time).SetLoops(-1).SetEase(Ease.Linear);
        }

        public static void Move(TransformHoming _base, TransformHoming _target, float time)
        {
            _base.transform.DOLocalMove(_target.pos, time);
            _base.transform.DOLocalRotate(_target.roa, time);
            _base.transform.DOScale(_target.scale, time);
        }

        public static void Move(Transform _base, TransformHoming _target, float time)
        {
            _base.DOLocalMove(_target.pos, time);
            _base.DOLocalRotate(_target.roa, time);
            _base.DOScale(_target.scale, time);
        }

        public static void Move(TransformHoming _base, Transform _target, float time)
        {
            _base.transform.DOLocalMove(_target.transform.localPosition, time);
            _base.transform.DOLocalRotate(_target.transform.localEulerAngles, time);
            _base.transform.DOScale(_target.transform.localScale, time);
        }
    }
}

