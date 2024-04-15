using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace ZXKFramework
{
    /// <summary>
    /// 对象池中的池子
    /// </summary>
    public class SubPool
    {
        //集合
        List<GameObject> m_objecs = new List<GameObject>();

        //预设
        GameObject m_prefab;

        //父物体的位置
        Transform m_parent;

        //名字
        public string Name
        {
            get { return m_prefab.name; }
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="go"></param>
        public SubPool(Transform parent, GameObject go)
        {
            m_prefab = go;
            m_parent = parent;
        }

        //取出物体
        public GameObject Spawn()
        {
            GameObject go = null;

            foreach (var obj in m_objecs)
            {
                if (!obj.activeSelf)
                {
                    go = obj;
                    break;
                }
            }

            if (go == null)
            {
                go = GameObject.Instantiate<GameObject>(m_prefab, m_parent);
                m_objecs.Add(go);
            }

            go.SetActive(true);
            go.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
            return go;
        }

        //回收物体
        public void UnSpawn(GameObject go)
        {
            if (Contain(go))
            {
                go.SendMessage("OnUnSpawn", SendMessageOptions.DontRequireReceiver);
                go.SetActive(false);
            }
        }

        //回收所有
        public void UnspawnAll()
        {
            foreach (var obj in m_objecs)
            {
                if (obj.activeSelf)
                {
                    UnSpawn(obj);
                }
            }
        }

        //判断是否属于list里边
        public bool Contain(GameObject go)
        {
            if (go != null)
            {
                return m_objecs.Contains(go);
            }
            return false;
        }
    }
}