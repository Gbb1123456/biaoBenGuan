using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ZXKFramework
{
    public interface ITipManager
    {
        void Init(Transform transform);
        void Show(TipData data);
        void ShowTip(string data, float time = 2, bool isShowBg = true);
        void ShowTipUP(string data, float time = 2, bool isShowBg = true);
        void ShowTipDown(string data, float time = 2, bool isShowBg = true);
        void ShowTipOneBtn(string data, Action btnEvent);
        void ShowTipTwoBtn(string data, Action btnEvent, Action btn2Event);
        void ShowLit(string data, float time = 3);
        void AddTips(TipData tips);
        void CloseAllTip();
        void Update();
    }
}