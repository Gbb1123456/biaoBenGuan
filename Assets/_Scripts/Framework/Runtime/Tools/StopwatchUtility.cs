using System;
using UnityEngine.Events;
using System.Diagnostics;
namespace ZXKFramework
{
    /// <summary>
    /// Stopwatch类的工具类。用于计算运行一段代码所用的时间。
    /// </summary>
    public static class StopwatchUtility
    {
        /// <summary>
        /// 获取执行一段代码所用的时间的信息。
        /// </summary>
        /// <param name="call">要执行的代码</param>
        public static TimeSpan GetTime(UnityAction call)
        {
            Stopwatch timer = Stopwatch.StartNew();//声明计时器
            timer.Start();//开启计时器
            call?.Invoke();//执行一段代码
            timer.Stop();//停止计时器
            return timer.Elapsed;//返回时间信息
        }

        /// <summary>
        /// 在控制台打印执行一段代码所用的时间，单位是秒。
        /// </summary>
        /// <param name="call">要执行的代码</param>
        /// <param name="executionNumber">执行的次数。应保证它为正整数，否则本方法无效，并会在控制台显示黄色的警告信息。</param>
        public static void PrintTime(UnityAction call, int executionNumber = 1)
        {
            //保证执行次数为正整数。
            if (executionNumber <= 0)
            {
                UnityEngine.Debug.LogWarning("Stopwatch性能测试失败！执行次数应为正整数。");
                return;
            }

            //用于记录所用的总时间，单位是毫秒。
            double totalMilliseconds = 0;

            //执行一段代码一定的次数，并记录执行时间。
            for (int i = 0; i < executionNumber; i++)
            {
                totalMilliseconds += GetTime(call).TotalMilliseconds;
            }

            //在控制台打印执行的总时间
            UnityEngine.Debug.Log($"执行这段代码{executionNumber}次所需的时间是{totalMilliseconds / 1000}秒");
        }
    }
}