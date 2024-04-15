using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ZXKFramework
{
    public class DrawLine : MonoBehaviour
    {
        public GameObject line;
        private List<Vector3> list;  //链表存放所用新的位置
        bool isMouseDown = false;    //是否可以开始画线
        GameObject tempLine;         //临时的线 每次都要建立一条新的线段
        private List<GameObject> allLine = new List<GameObject>();
        public bool isUse = false;

        void Start()
        {
            if (line == null) return;
            line.SetActiveSafe(false);
        }

        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (line == null || !isUse) return;
                isMouseDown = true;
                tempLine = Instantiate(line);
                allLine.Add(tempLine);
                list = new List<Vector3>();
            }
            if (Input.GetMouseButtonUp(0))
            {
                if (line == null || !isUse) return;
                isMouseDown = false;
            }
            if (isMouseDown)
            {
                Vector3 vec3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
                vec3 = Camera.main.ScreenToWorldPoint(vec3);
                //vec3.Set(vec3.x, vec3.y, 0f);
                list.Add(vec3);
                if (list.Count >= 2)
                {
                    tempLine.SetActiveSafe(true);
                    tempLine.GetOrAddComponent<Line>().list = list;
                }
            }
        }

        public void CleanAll()
        {
            if (allLine.Count != 0)
            {
                for (int i = 0; i < allLine.Count; i++)
                {
                    Debug.Log(allLine[i]);
                    DestroyImmediate(allLine[i]);
                }
                allLine.Clear();
            }
        }

        public void CleanLast()
        {
            if (allLine.Count != 0)
            {
                GameObject loObj = allLine[allLine.Count - 1];
                allLine.Remove(loObj);
                DestroyImmediate(loObj);
            }
        }
    }
}