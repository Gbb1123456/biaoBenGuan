using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework
{
    //连续出现的小提示
    public class TipLit : MonoBehaviour
    {
        PoolObj pool = null;
        public void Init()
        {
            GameObject loText = transform.FindFirst("ModelBase");
            loText.GetOrAddComponent<TipTime>().Init();
            loText.SetActive(false);
            pool = new PoolObj(loText);
        }
        public void ShowTip(TipData data)
        {
            TipTime loTipTime = pool.GetObj().GetOrAddComponent<TipTime>();
            loTipTime.Init();
            loTipTime.ShowTip(data, null); ;
        }
    }
}
