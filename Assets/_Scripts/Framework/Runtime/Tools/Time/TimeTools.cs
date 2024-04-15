using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TimeTools  
{
    /// <summary>
    /// DateTime转时间戳
    /// </summary>
    /// <param name="targetDateTime">DateTime</param>
    /// <returns>时间戳（秒）</returns>
    public static long ConvertToTimeStamp(DateTime targetDateTime)
    {
        return (targetDateTime.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
    }

    /// <summary>
    /// 时间戳转DateTime
    /// </summary>
    /// <param name="timeStamp">时间戳（秒）</param>
    /// <returns>DateTime</returns>
    public static DateTime GetDateTimeFromTimeStamp(long timeStamp)
    {
        //获取到当前时区的开始时间戳
        DateTime startDateTime = TimeZoneInfo.ConvertTimeFromUtc(new DateTime(1970, 1, 1, 0, 0, 0), TimeZoneInfo.Local);
        long targetTimeStamp = ((long)timeStamp * 10000000);
        TimeSpan targetTS = new TimeSpan(targetTimeStamp);
        DateTime targetDateTime = startDateTime.Add(targetTS);
        return targetDateTime;
    }

    //一天总共的秒数
    private const int CN_ONE_DAY_SECONDS = 24 * 60 * 60;
    //一小时总共的秒数
    private const int CN_ONE_HOUR_SECONDS = 60 * 60;
    //一分钟总共的秒数
    private const int CN_ONE_MIN_SECONDS = 60;
    /// <summary>
    /// 把秒数转换成天时分秒
    /// </summary>
    /// <param name="totalSeconds">总的秒数</param>
    /// <returns></returns>
    public static string ConvertSecToDHMS(int totalSeconds)
    {
        int days = totalSeconds / CN_ONE_DAY_SECONDS;
        int hours = totalSeconds % CN_ONE_DAY_SECONDS / CN_ONE_HOUR_SECONDS;
        int minutes = totalSeconds % CN_ONE_HOUR_SECONDS / CN_ONE_MIN_SECONDS;
        int seconds = totalSeconds % CN_ONE_HOUR_SECONDS % CN_ONE_MIN_SECONDS;
        return string.Format("{0}天：{1}时：{2}分：{3}秒：", days, hours, minutes, seconds);
    }


    //获取第N天的0点的时间戳（游戏中常用作活动的刷新，可以做个计时器，获取到这个时间戳然后我当前时间戳相减做回调处理）
    public static long GetNextDayZeroTimeStamp(int day)
    {
        //当天0时0分0秒
        DateTime todayMidnight = DateTime.Now.Date;
        //第二天的0时0分0秒
        DateTime nextDayMidnight = todayMidnight.AddDays(day);
        return ConvertToTimeStamp(nextDayMidnight);
    }

    /// <summary>
    /// 当前的时间戳
    /// </summary>
    public static long TimeStampNow
    {
        get
        {
            return (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
        }
    }
}
