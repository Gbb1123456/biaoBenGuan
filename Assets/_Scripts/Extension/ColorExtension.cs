/************************************************************
* FileName: ColorExtension.cs
* Author: 万剑飞
* Version : 1.0
* Date: 2021.01.07 16:59:51
* Description: 颜色的扩展方法
* ======================================
* ChangeLog：
************************************************************/

using UnityEngine;

public static class ColorExtension
{
    /// <summary>
    /// 改变颜色的透明度
    /// </summary>
    /// <param name="color"></param>
    /// <param name="alpha"></param>
    /// <returns></returns>
    public static Color Fade(this Color color, float alpha)
    {
        Color c = color;
        c.a = alpha;
        color = c;

        return color;
    }
}