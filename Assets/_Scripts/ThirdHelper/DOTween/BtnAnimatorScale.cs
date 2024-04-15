
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace ZXKFramework
{
    //按钮缩放动画动画
    public class BtnAnimatorScale : MonoBehaviour
    {
        //记录原始大小
        private Vector3 objScale = new Vector3();
        private Button btn = null;
        public float time { get; set; } = 0.1f;
        public float litScale { get; set; } = 0.8f;

        void Awake()
        {
            objScale = transform.localScale;
            btn = GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(() =>
                {
                    transform.DOScale(objScale * litScale, time).OnComplete(() =>
                    {
                        transform.DOScale(objScale, time);
                    });
                });
            }
        }

        void OnEnable()
        {
            transform.localScale = objScale;
        }
    }
}

