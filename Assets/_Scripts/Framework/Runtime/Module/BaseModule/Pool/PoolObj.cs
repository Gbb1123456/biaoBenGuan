
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public class PoolObj
    {
        List<GameObject> pooledObjects = null;
        private GameObject gameObj = null;

        public PoolObj(GameObject obj)
        {
            pooledObjects = new List<GameObject>();
            gameObj = obj;
        }

        public GameObject GetObj()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                GameObject tempObj = pooledObjects[i];
                if (!tempObj.activeInHierarchy)
                {
                    tempObj.SetActive(true);
                    return tempObj.gameObject;
                }
            }
            if (gameObj != null)
            {
                GameObject newObj = GameObject.Instantiate(gameObj, gameObj.transform.parent);
                newObj.SetActive(false);
                pooledObjects.Add(newObj);//添加
                return GetObj();
            }
            WDebug.LogError("PoolObj Resources path == null ");
            return null;
        }

        public void CloseAll()
        {
            for (int i = 0; i < pooledObjects.Count; i++)
            {
                pooledObjects[i].SetActive(false);
            }
        }
    }
}