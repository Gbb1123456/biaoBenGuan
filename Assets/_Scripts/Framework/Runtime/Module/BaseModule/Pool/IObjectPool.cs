using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ZXKFramework
{
    public interface IObjectPool
    {
        void Init(string dirPath);
        /// <summary>
        /// 设置加载的方式
        /// </summary>
        /// <param name="res"></param>
        void SetResType(IRes res);
         
        //取出
        void Spawn(string name, Transform trans, Action<GameObject> callBack = null);

        //回收物体
        void Unspawn(GameObject go);
        
        //回收一组物体
        void Unspawn(string go);

        //回收所有
        void UnspawnAll();

        //清除所有
        void Clear();
    }
}