
using UnityEngine;
using System;
using System.Collections.Generic;

namespace ZXKFramework
{
    public class TipData
    {
        public TipTypeEnum tipType { get; set; } = TipTypeEnum.Tip;
        public string tip { get; set; } = "";
        public float time { get; set; } = 2;
        public bool isShowBg { get; set; } = true;
        public string btn1Text { get; set; } = "确定";
        public string btn2Text { get; set; } = "取消";
        public Action btn1Event { get; set; } = null;
        public Action btn2Event { get; set; } = null;
    }
}