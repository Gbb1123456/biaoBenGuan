
using UnityEngine.UI;
using System;
namespace ZXKFramework
{
    public class TipBtnTwo : TipBase
    {
        private TipBtn btn1;
        private TipBtn btn2;
        Action callBackEvent;
        public override void OnAwake()
        {
            base.OnAwake();
            btn1 = transform.FindFirst("Btn1").GetOrAddComponent<TipBtn>();
            btn2 = transform.FindFirst("Btn2").GetOrAddComponent<TipBtn>();
            btn1.Init();
            btn2.Init();
        }
        public void ShowTipEvent(TipData data, Action callBack)
        {
            ShowTip(data.tip);
            btn1.SetBtn(data.btn1Text, () =>
            {
                data.btn1Event?.Invoke();
                Close();
            });
            btn2.SetBtn(data.btn2Text, () =>
            {
                data.btn2Event?.Invoke();
                Close();
            });
            callBackEvent = callBack;
        }
        public override void Close()
        {
            base.Close();
            btn1.Clean();
            btn2.Clean();
            gameObject.SetActiveSafe(false);
            callBackEvent?.Invoke();
            callBackEvent = null;
        }
    }
}