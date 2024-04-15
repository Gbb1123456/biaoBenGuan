using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class Adsorb
    {
        public Dictionary<string, AdsorbItem> allAds = new Dictionary<string, AdsorbItem>();

        public void Init(Transform trs)
        {
            Add(trs);
        }

        void Add(Transform trs)
        {
            if (!allAds.ContainsKey(trs.name))
            {
                allAds.Add(trs.name, trs.GetOrAddComponent<AdsorbItem>());
            }
            if (trs.childCount != 0)
            {
                foreach (Transform tr in trs)
                {
                    Add(tr);
                }
            }
        }
    }
}