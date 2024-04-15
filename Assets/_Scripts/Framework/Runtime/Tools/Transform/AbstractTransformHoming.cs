using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    /// <summary>
    /// 归位抽象类
    /// </summary>
    public abstract class AbstractTransformHoming
    {
        public Vector3 pos = new Vector3();
        public Vector3 roa = new Vector3();
        public Vector3 scale = new Vector3();
        public Transform transform;

        public virtual void Init(Transform _transform)
        {
            transform = _transform;
            if (transform == null) return;
            pos = transform.localPosition;
            roa = transform.localEulerAngles;
            scale = transform.localScale;
        }

        public virtual void RestTransform()
        {

        }
    }
}

