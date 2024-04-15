using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class AdsorbItem : MonoBehaviour
    {
        public float distance = 1f;
        public float time = 1f;
        private TransformHoming loTransformHoming = new TransformHoming();

        void Awake()
        {
            loTransformHoming.Init(transform);
        }

        public void CheakHoming()
        {
            if (Vector3.Distance(transform.position, loTransformHoming.pos) < distance)
            {
                DOTweenTools.Move(transform, loTransformHoming, time);
            }
        }
    }
}