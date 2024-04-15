using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    /// <summary>
    /// 记录模型位置并且可以复位
    /// </summary>
    public class TransformHoming : AbstractTransformHoming
    {
        public override void RestTransform()
        {
            if (transform == null) return;
            transform.localPosition = pos;
            transform.localEulerAngles = roa;
            transform.localScale = scale;
        }
    }
}