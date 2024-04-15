
using UnityEngine;
using System;
namespace ZXKFramework
{
    //一段时间自动关闭
    public class TipTime : TipBase
    {
        Coroutine con;
        public void ShowTip(TipData data, Action callBack)
        {
            ShowTip(data.tip);
            ShowBg(data.isShowBg);
            Game.Instance.IEnumeratorManager.Stop(con);
            con = Game.Instance.IEnumeratorManager.Run(data.time, () =>
            {
                gameObject.SetActiveSafe(false);
                Close();
                callBack?.Invoke();
            });
        }
        public override void Close()
        {
            base.Close();
            con = null;
        }
    }
}