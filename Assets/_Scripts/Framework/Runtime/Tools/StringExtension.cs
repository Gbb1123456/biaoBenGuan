using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public static class StringExtension
    {
        public static string GetFirstStr(this string str, int num)
        {
            if (str.Length > num)
            {
                str = str.Substring(0, num);
            }
            return str;
        }

        public static string GetLastStr(this string str, int num)
        {
            int count = 0;
            if (str.Length > num)
            {
                count = str.Length - num;
                str = str.Substring(count, num);
            }
            return str;
        }

        public static string RemoveFirstStr(this string str, int num)
        {
            if (str.Length > num)
            {
                str = str.Remove(0, num);
            }
            return str;
        }

        public static string RemoveLastStr(this string str, int num)
        {
            int count = 0;
            if (str.Length > num)
            {
                count = str.Length - num;
                str = str.Remove(count, num);
            }
            return str;
        }

        public static bool IsNull(this string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return true;
            }
            return false;
        }

        public static bool IsNotNull(this string data)
        {
            if (!string.IsNullOrEmpty(data))
            {
                return true;
            }
            return false;
        }

        public static string LogInfo(this string data)
        {
            Debug.Log(data);
            return data;
        }

        public static string FontColor(this string data, string color)
        {
            return "<color=" + color + ">" + data + "</color>";
        }

        public static string FontSize(this string data, string size)
        {
            return "<size=" + size + ">" + data + "</size>";
        }

        public static string FontSize(this string data, int size)
        {
            return "<size=" + size + ">" + data + "</size>";
        }

        //一个有空格字符串，合成一个没有空格字符串
        public static string StringNoSpace(this string data)
        {
            string temp = "";
            foreach (string res in StringDecomposedBySpaces(data))
            {
                temp += res;
            }
            return temp;
        }

        //字符串被空格分解为数组
        public static List<string> StringDecomposedBySpaces(string data)
        {
            List<string> res = new List<string>();
            char[] chs = { ' ' };
            string[] tempData = data.Split(chs, options: StringSplitOptions.RemoveEmptyEntries);//省略空返回数组
            foreach (string s in tempData)
            {
                res.Add(s);
            }
            return res;
        }

        //集合中所有字符去掉空格
        public static List<string> DeleteSpace(List<string> data)
        {
            List<string> temp = new List<string>();
            foreach (string res in temp)
            {
                if (!String.IsNullOrEmpty(res))
                {
                    temp.Add(res.Trim());
                }
            }
            return temp;
        }
    }
}
