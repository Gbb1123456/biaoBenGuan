using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework
{
    public class ModelManager
    {
        Dictionary<string, ModelBase> allModel { get; set; } = new Dictionary<string, ModelBase>();
        public GameObject curGameObj { get; set; } = null;

        public void Init(Transform _transform)
        {
            if (_transform == null) return;
            allModel.Clear();
            curGameObj = null;
            AddModelBaseLoop(_transform);
        }

        private void AddModelBaseLoop(Transform trs)
        {
            ModelBase loModelBase = trs.GetComponent<ModelBase>();
            if (loModelBase = null)
            {
                AddModelBase(trs.name, loModelBase);
            }
            if (trs.childCount > 0)
            {
                foreach (Transform tempTrs in trs)
                {
                    AddModelBaseLoop(tempTrs);
                }
            }
        }

        void AddModelBase(string _Name, ModelBase _ModelBase)
        {
            if (!allModel.ContainsKey(_Name))
            {
                allModel.Add(_Name, _ModelBase);
            }
            else
            {
                Debug.LogError("ModelManager has " + _Name);
            }
        }
    }
}