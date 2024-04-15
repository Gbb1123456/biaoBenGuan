using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZXKFramework
{
    public class MouseMoveObj : MonoBehaviour
    {
        public Action<GameObject> down;
        public Action<GameObject> up;
        public bool isMove = false;

        Vector3 m_Offset;
        Vector3 m_TargetScreenVec;
        Camera camera;

        private void Awake()
        {
            camera = Camera.main;
        }

        private IEnumerator OnMouseDown()
        {        
            m_TargetScreenVec = camera.WorldToScreenPoint(transform.position);
            m_Offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3
            (Input.mousePosition.x, Input.mousePosition.y, m_TargetScreenVec.z));
            down?.Invoke(gameObject);
            if (!isMove) yield break;
            while (Input.GetMouseButton(0))
            {
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, m_TargetScreenVec.z)) + m_Offset;
                yield return new WaitForFixedUpdate();
            }
        }

        private IEnumerator OnMouseUp()
        {
            yield return new WaitForFixedUpdate();
            up?.Invoke(gameObject);
        }
    }
}