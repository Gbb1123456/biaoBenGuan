/*****************************************************
* 版权声明：上海卓越睿新数码科技有限公司，保留所有版权
* 文件名称：ButtonExtension.cs
* 文件版本：1.0
* 创建时间：2022/07/12 09:21:14
* 作者名称：TangZhenYu
* 文件描述：

*****************************************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

/// <summary>
/// 
/// </summary>
public static class ButtonExtension
{
    public static T AddListener<T>(this T self, UnityAction unityAction) where T : Button
    {
        self.onClick.RemoveAllListeners();
        self.onClick.AddListener(unityAction);
        return self;
    }

    public static T RemoveListener<T>(this T self, UnityAction unityAction) where T : Button
    {
        self.onClick.RemoveListener(unityAction);
        return self;
    }

    public static T RemoveAllListener<T>(this T self) where T : Button
    {
        self.onClick.RemoveAllListeners();
        return self;
    }
}