
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class ModelChildShowOneBase
    {
        List<GameObject> allModel { get; set; } = new List<GameObject>();
        public GameObject curGameObj { get; set; } = null;
        private int curObjIndex = -1;
        private Transform transform;

        public void Init(Transform _transform)
        {
            if (_transform == null) return;
            allModel.Clear();
            curGameObj = null;
            curObjIndex = -1;
            transform = _transform;
            foreach (Transform trs in transform)
            {
                allModel.Add(trs.gameObject);
                trs.gameObject.SetActiveSafe(false);
            }
        }

        public GameObject Next()
        {
            if (curObjIndex < allModel.Count - 1)
            {
                curObjIndex++;
            }
            else
            {
                curObjIndex = 0;
            }
            return ShowModel(curObjIndex);
        }

        public GameObject Last()
        {
            if (curObjIndex > 0)
            {
                curObjIndex--;
            }
            else
            {
                curObjIndex = allModel.Count - 1;
            }
            return ShowModel(curObjIndex);
        }

        public GameObject GetModel(int index)
        {
            if (index >= 0 && index <= allModel.Count - 1)
            {
                WDebug.Log("ModelChildShowOne index = " + index);
                return allModel[index];
            }
            else
            {
                WDebug.LogError("没有子物体 index = " + index);
                return null;
            }
        }

        public GameObject ShowModel(int index)
        {
            if (index >= 0 && index <= allModel.Count - 1)
            {
                WDebug.Log("ModelChildShowOne index = " + index);
                if (curGameObj != null)
                {
                    curGameObj.SetActiveSafe(false);
                }
                curGameObj = allModel[index];
                curGameObj.SetActiveSafe(true);
                return curGameObj;
            }
            else
            {
                WDebug.LogError("没有子物体 index = " + index);
                return null;
            }
        }

        public GameObject GetModel(string objName)
        {
            return transform.FindFirst(objName);
        }

        public GameObject ShowModel(string objName)
        {
            GameObject obj = transform.FindFirst(objName);
            if (obj != null)
            {
                WDebug.Log("ModelShow ShowModel objName = " + objName);
                if (curGameObj != null)
                {
                    curGameObj.SetActiveSafe(false);
                }
                curGameObj = obj;
                curGameObj.SetActiveSafe(true);
            }
            return obj;
        }

        public GameObject ShowModelNoCloseOther(string objName, bool isShow = true)
        {
            GameObject obj = transform.FindFirst(objName);
            if (obj != null)
            {
                WDebug.Log("ModelShow ShowModel objName = " + objName);
                curGameObj = obj;
                curGameObj.SetActiveSafe(isShow);
            }
            return obj;
        }

        public void CloseAllModel()
        {
            foreach (Transform trs in transform)
            {
                trs.gameObject.SetActiveSafe(false);
            }
            curObjIndex = -1;
            curGameObj = null;
        }

        public void ShowAllModel()
        {
            foreach (Transform trs in transform)
            {
                trs.gameObject.SetActiveSafe(true);
            }
            curObjIndex = -1;
            curGameObj = null;
        }
    }
}