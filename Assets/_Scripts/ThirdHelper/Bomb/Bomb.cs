using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace ZXKFramework
{
    public class Bomb : MonoBehaviour
    {
        private Transform _baseObj;
        public Transform _targetObj;
        List<TransformHoming> baseAll = new List<TransformHoming>();
        List<TransformHoming> targetAll = new List<TransformHoming>();
        bool isMove = false;

        private void Awake()
        {
            _baseObj = transform;
            if (_targetObj == null || _baseObj == null) return;
            baseAll.Clear();
            AddObj(baseAll, _baseObj);
            _targetObj.gameObject.SetActive(false);
            targetAll.Clear();
            AddObj(targetAll, _targetObj);
        }

        void AddObj(List<TransformHoming> list, Transform tra)
        {
            TransformHoming loTransformHoming = new TransformHoming();
            loTransformHoming.Init(tra);
            list.Add(loTransformHoming);
            if (tra.childCount > 0)
            {
                foreach (Transform obj in tra.transform)
                {
                    AddObj(list, obj);
                }
            }
        }

        public void ToReset()
        {
            isMove = false;
            Back();
        }

        public void Move(float time = 1f)
        {
            if (_targetObj == null || _baseObj == null) return;
            for (int i = 0; i < baseAll.Count; i++)
            {
                if (baseAll[i].pos != targetAll[i].pos)
                {
                    DOTweenTools.Move(baseAll[i], targetAll[i], time);
                }
            }
        }

        public void Back(float time = 1f)
        {
            if (_targetObj == null || _baseObj == null) return;
            for (int i = 0; i < baseAll.Count; i++)
            {
                if (baseAll[i].transform.localPosition != baseAll[i].pos)
                {
                    DOTweenTools.Move(baseAll[i], baseAll[i], time);
                }
            }
        }

        public void MoveOrBack(float time = 1f)
        {
            isMove = !isMove;
            if (isMove)
            {
                Move(time);
            }
            else
            {
                Back(time);
            }
        }
    }
}
