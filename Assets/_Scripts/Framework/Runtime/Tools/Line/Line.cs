using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZXKFramework
{
    public class Line : MonoBehaviour
    {
        public float size = 1f;
        LineRenderer linerenderer;
        EdgeCollider2D edgeCollider2d;
        List<Vector3> _list;

        void Awake()
        {
            linerenderer = transform.GetOrAddComponent<LineRenderer>();
            edgeCollider2d = transform.GetOrAddComponent<EdgeCollider2D>();
        }

        public List<Vector3> list
        {
            get { return _list; }
            set
            {
                _list = value;
                linerenderer.positionCount = _list.Count;
                Vector2[] vec2 = new Vector2[_list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    linerenderer.SetPosition(i, _list[i]);
                    linerenderer.startWidth = size;
                    linerenderer.endWidth = size;
                    vec2[i] = new Vector2(_list[i].x, _list[i].y);
                }
                edgeCollider2d.points = vec2;
            }
        }
    }
}
