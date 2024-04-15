
using UnityEngine;
using System;
using System.Collections.Generic;

namespace ZXKFramework
{
    public class TipManager : ITipManager
    {
        private TipTime tip;
        private TipTime tipUP;
        private TipTime tipDown;
        private TipBtnOne tipBtnOne;
        private TipBtnTwo tipBtnTwo;
        private TipLit tipLit;
        private Queue<TipData> tipsQue = new Queue<TipData>();
        private bool isTipsShow = false;
        ModelChildShowOne allTip = null;

        public void Init(Transform transform)
        {
            transform.gameObject.SetActiveSafe(true);
            tip = transform.FindFirst("Tip").GetOrAddComponent<TipTime>();
            tipUP = transform.FindFirst("TipUp").GetOrAddComponent<TipTime>();
            tipDown = transform.FindFirst("TipDown").GetOrAddComponent<TipTime>();
            tipBtnOne = transform.FindFirst("TipOneBtn").GetOrAddComponent<TipBtnOne>();
            tipBtnTwo = transform.FindFirst("TipTwoBtn").GetOrAddComponent<TipBtnTwo>();
            tipLit = transform.FindFirst("TipLit").GetOrAddComponent<TipLit>();
            allTip = transform.GetOrAddComponent<ModelChildShowOne>();
            tip.Init();
            tipUP.Init();
            tipDown.Init();
            tipBtnOne.Init();
            tipBtnTwo.Init();
            tipLit.Init();
            CloseAllTip();
        }

        public void Show(TipData data)
        {
            if (data.IsNull() || data.tip.IsNull()) return;
            if (data.tipType == TipTypeEnum.TipLit)
            {
                tipLit.ShowTip(data);
            }
            else
            {
                AddTips(data);
            }
        }

        public void ShowTip(string data, float time = 2, bool isShowBg = true)
        {
            Show(new TipData()
            {
                tipType = TipTypeEnum.Tip,
                tip = data,
                time = time,
                isShowBg = isShowBg
            });
        }

        public void ShowTipUP(string data, float time = 2, bool isShowBg = true)
        {
            Show(new TipData()
            {
                tipType = TipTypeEnum.TipUP,
                tip = data,
                time = time,
                isShowBg = isShowBg
            });
        }

        public void ShowTipDown(string data, float time = 2, bool isShowBg = true)
        {
            Show(new TipData()
            {
                tipType = TipTypeEnum.TipDown,
                tip = data,
                time = time,
                isShowBg = isShowBg
            });
        }

        public void ShowTipOneBtn(string data, Action btnEvent)
        {
            Show(new TipData()
            {
                tipType = TipTypeEnum.TipOneBtn,
                tip = data,
                btn1Event = btnEvent
            });
        }

        public void ShowTipTwoBtn(string data, Action btnEvent, Action btn2Event)
        {
            Show(new TipData()
            {
                tipType = TipTypeEnum.TipTwoBtn,
                tip = data,
                btn1Event = btnEvent,
                btn2Event = btn2Event
            });
        }

        public void ShowLit(string data, float time = 3)
        {
            Show(new TipData()
            {
                tipType = TipTypeEnum.TipLit,
                tip = data,
                time = time
            }); ;
        }

        public void AddTips(TipData tips)
        {
            lock (tipsQue)
            {
                tipsQue.Enqueue(tips);
            }
        }

        public void Update()
        {
            if (tipsQue.Count > 0 && isTipsShow == false)
            {
                lock (tipsQue)
                {
                    TipData tips = tipsQue.Dequeue();
                    isTipsShow = true;
                    ShowTip(tips);
                }
            }
        }

        void ShowTip(TipData tips)
        {
            switch (tips.tipType)
            {
                case TipTypeEnum.Tip:
                    tip.ShowTip(tips, () => isTipsShow = false);
                    break;
                case TipTypeEnum.TipOneBtn:
                    tipBtnOne.ShowTipEvent(tips, () => isTipsShow = false);
                    break;
                case TipTypeEnum.TipTwoBtn:
                    tipBtnTwo.ShowTipEvent(tips, () => isTipsShow = false);
                    break;
                case TipTypeEnum.TipUP:
                    tipUP.ShowTip(tips, () => isTipsShow = false);
                    break;
                case TipTypeEnum.TipDown:
                    tipDown.ShowTip(tips, () => isTipsShow = false);
                    break;
            }
        }
        public void CloseAllTip()
        {
            allTip.CloseAllModel();
            tipLit.gameObject.SetActiveSafe(true);
            tipsQue.Clear();
            isTipsShow = false;
        }
    }
}