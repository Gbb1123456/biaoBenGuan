using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
namespace ZXKFramework
{
    public class TipBtn : TipBase
    {
        Action btnEvent = null;
        private Text text = null;
        public void Init()
        {
            text = transform.FindFirst<Text>("Text");
            GetComponent<Button>().onClick.AddListener(() => btnEvent?.Invoke());
        }
        public void SetBtn(string textData, Action cBtnEvent)
        {
            text.text = textData;
            btnEvent = cBtnEvent;
        }
        public void Clean()
        {
            btnEvent = null;
        }
    }
}
