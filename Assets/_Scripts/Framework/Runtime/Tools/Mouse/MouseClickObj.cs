using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public class MouseClickObj : MonoBehaviour
    {
        public Action<GameObject> down;
        public Action<GameObject> up;

        private void OnMouseDown()
        {         
            down?.Invoke(gameObject);
        }

        private void OnMouseUp()
        {
            up?.Invoke(gameObject);
        }
    }
}
