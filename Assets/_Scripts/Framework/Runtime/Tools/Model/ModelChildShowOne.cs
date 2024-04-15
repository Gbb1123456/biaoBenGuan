using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class ModelChildShowOne : MonoBehaviour
    {
        ModelChildShowOneBase modelChildShowOneBase = new ModelChildShowOneBase();
        public bool isInit = true;

        void Awake()
        {
            if (!isInit) return;
            modelChildShowOneBase.Init(transform);
        }

        public void Init()
        {
            modelChildShowOneBase.Init(transform);
        }

        public GameObject Next()
        {
            return modelChildShowOneBase.Next();
        } 

        public GameObject Last()
        {
            return modelChildShowOneBase.Last();
        }

        public GameObject GetModel(int index)
        {
            return modelChildShowOneBase.GetModel(index);
        }

        public GameObject GetModel(string objName)
        {
            return modelChildShowOneBase.GetModel(objName);
        }

        public GameObject ShowModel(int index)
        {
            return modelChildShowOneBase.ShowModel(index);
        }

        public GameObject ShowModel(string objName)
        {
            return modelChildShowOneBase.ShowModel(objName);
        }

        public GameObject ShowModelNoCloseOther(string objName, bool isShow = true)
        {
            return modelChildShowOneBase.ShowModelNoCloseOther(objName, isShow);
        }

        public void CloseAllModel()
        {
            modelChildShowOneBase.CloseAllModel();
        }

        public void ShowAllModel()
        {
            modelChildShowOneBase.ShowAllModel();
        }
    }
}