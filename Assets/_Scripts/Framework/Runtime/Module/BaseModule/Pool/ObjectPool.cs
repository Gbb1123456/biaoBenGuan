using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace ZXKFramework
{
    /// <summary>
    /// 对象池管理器，管理所有的对象池
    /// </summary>
    public class ObjectPool : MonoBehaviour, IObjectPool
    {
        /// <summary>
        /// 资源目录
        /// </summary>
        public string poolDir = "";

        Dictionary<string, SubPool> m_pools = new Dictionary<string, SubPool>();

        IRes resManager;
        public void SetResType(IRes res)
        {
            resManager = res;
        }

        public void Init(string dirPath)
        {
            poolDir = dirPath;
        }

        //取出物体
        public void Spawn(string name, Transform trans, Action<GameObject> callBack = null)
        {
            SubPool pool = null;
            //没有的话，心间对象池并且取出物体
            if (!m_pools.ContainsKey(name))
            {
                RegieterNew(name, trans, () =>
                 {
                     pool = m_pools[name];
                     GameObject loObj = pool.Spawn();
                     callBack?.Invoke(loObj);
                 });
            }
            else
            {
                pool = m_pools[name];
                GameObject loObj = pool.Spawn();
                loObj.transform.SetParent(trans);
                callBack?.Invoke(loObj);
            }
        }

        //回收物体
        public void Unspawn(GameObject go)
        {
            SubPool pool = null;
            foreach (var p in m_pools.Values)
            {
                if (p.Contain(go))
                {
                    pool = p;
                    break;
                }
            }
            pool.UnSpawn(go);
        }

        //回收所有
        public void UnspawnAll()
        {
            foreach (var p in m_pools.Values)
            {
                p.UnspawnAll();
            }
        }

        //清除所有
        public void Clear()
        {
            m_pools.Clear();
        }

        //新建一个池子
        void RegieterNew(string names, Transform trans, Action callback)
        {
            //资源目录
            string path = string.IsNullOrEmpty(poolDir) ? names : poolDir + "/" + names;
            //生成预制体
            resManager?.Load<GameObject>(path, go =>
            {
                //新建一池子
                SubPool pool = new SubPool(trans, go);
                m_pools.Add(pool.Name, pool);
                callback?.Invoke();
            });
        }

        public void Unspawn(string go)
        {
            if (m_pools.ContainsKey(go))
            {
                m_pools[go].UnspawnAll();
            }
        }
    }
}