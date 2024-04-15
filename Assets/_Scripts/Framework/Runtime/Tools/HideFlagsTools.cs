using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class HideFlagsTools : MonoBehaviour
    {
        private void Awake()
        {
            TheHideFlags();
        }

        [ContextMenu("HideFlags")]
        public void ToHideFlags()
        {
            Awake();
            TheHideFlags();
        }

        void TheHideFlags()
        {
            gameObject.hideFlags = HideFlags.None;
        }
    }
}

