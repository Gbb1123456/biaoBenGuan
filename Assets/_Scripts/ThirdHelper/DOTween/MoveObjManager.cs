
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework
{
    public class MoveObjManager
    {
        private List<GameObject> allMoveObj = new List<GameObject>();
        private List<MoveObjItem> allMoveItem = new List<MoveObjItem>();
        private float speed = 1f;
        private bool isMoveEnd = false;
        public void Init(Transform transform, float _speed, bool _isMoveEnd = false)
        {
            speed = _speed;
            isMoveEnd = _isMoveEnd;
            if (transform.childCount == 0) return;
            allMoveObj.Clear();
            foreach (Transform loData in transform)
            {
                allMoveObj.Add(loData.gameObject);
            }
            if (allMoveObj.Count < 2) return;
            allMoveItem.Clear();
            for (int i = 0; i < allMoveObj.Count; i++)
            {
                if (i + 1 < allMoveObj.Count)
                {
                    MoveObjItem loMoveObjItem = new MoveObjItem();
                    loMoveObjItem.Init(allMoveObj[i].transform, allMoveObj[i + 1].transform, speed);
                    allMoveItem.Add(loMoveObjItem);
                }
                else
                {
                    if (isMoveEnd)
                    {
                        MoveObjItem loMoveObjItem = new MoveObjItem();
                        loMoveObjItem.Init(allMoveObj[i].transform, allMoveObj[0].transform, speed);
                        allMoveItem.Add(loMoveObjItem);
                    }
                }
            }
        }

        public void Move(bool isMoveBack = false)
        {
            foreach (MoveObjItem loData in allMoveItem)
            {
                loData.Move(isMoveBack);
            }
        }

        public void Pause()
        {
            foreach (MoveObjItem loData in allMoveItem)
            {
                loData.Pause();
            }
        }
    }
}