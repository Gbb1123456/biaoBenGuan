using System.Collections;
using System.Collections.Generic;

namespace ZXKFramework
{
    /// <summary>
    /// 取出与回收的接口
    /// </summary>
    public interface IReusable
    {
        //取出时候调用
        void OnSpawn();

        //回收调用
        void OnUnSpawn();
    }
}