
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ZXKFramework
{
    /// <summary>
    /// 面板缩放动画
    /// </summary>
    public class PanelAnimatorScale : MonoBehaviour
    {
        private Vector3 objScale = new Vector3();
        public float time { get; set; } = 0.3f;
        public float baseScale = 0.5f;//刚出现的大小

        void Awake()
        {
            objScale = transform.localScale;
        }

        void OnEnable()
        {
            transform.localScale = new Vector3(baseScale, baseScale, baseScale);
            transform.DOScale(objScale, time);
        }
    }
}
