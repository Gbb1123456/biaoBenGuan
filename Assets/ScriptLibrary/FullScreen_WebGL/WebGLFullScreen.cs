
/*****************************************************
* 版权声明：上海卓越睿新数码科技有限公司，保留所有版权
* 文件名称：WebGLFullScreen.cs
* 文件版本：1.0
* 创建时间：2021/08/25 10:10:50
* 作者名称：Xuxiaohao
* 文件描述：

*****************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System;

namespace WisdomTree.NoName.Function
{
    /// <summary>
    /// WebGL全屏/退出全屏接口
    /// </summary>
    public class WebGLFullScreen
    {
        /// <summary>
        /// 全屏Js引用
        /// </summary>
        [DllImport("__Internal")]
        private static extern void UnityFullScreen();

        /// <summary>
        /// 退出全屏Js引用
        /// </summary>
        [DllImport("__Internal")]
        private static extern void UnitySmallScreen();

        /// <summary>
        /// 全屏
        /// </summary>
        public static void FullScreen()
        {
            try
            {
                UnityFullScreen();
            }
            catch (EntryPointNotFoundException e)
            {
                Debug.LogError(e);
            }
        }

        /// <summary>
        /// 窗口化
        /// </summary>
        public static void Windowed()
        {
            try
            {
                UnitySmallScreen();
            }
            catch (EntryPointNotFoundException e)
            {
                Debug.LogError(e);
            }
        }
    }
}