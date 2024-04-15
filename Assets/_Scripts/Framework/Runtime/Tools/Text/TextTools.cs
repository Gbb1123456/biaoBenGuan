using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

namespace ZXKFramework
{
    public class TextTools
    {
        //覆盖写入
        public static void Create(string path, string info)
        {
            StreamWriter sw;
            FileInfo fi = new FileInfo(path);
            sw = fi.CreateText();
            sw.WriteLine(info);
            sw.Close();
            sw.Dispose();
        }

        //读取
        public static void Read(string sPath, Action<bool, string> callBack)
        {
            StreamReader sr = null;
            sr = File.OpenText(sPath);
            string t_Line;
            if ((t_Line = sr.ReadLine()) != null)
            {
                callBack?.Invoke(true, t_Line);
            }
            else
            {
                callBack?.Invoke(false, "读取数据失败获取数据为空:" + sPath);
            }
            sr.Close();
            sr.Dispose();
        }

        //读取所有内容
        public static string Read(string sPath)
        {
            if (File.Exists(sPath))
            {
                return File.ReadAllText(sPath, Encoding.UTF8);
            }
            return "";
        }

        //得到每行字符串
        public static string[] ReadAllLines(string path)
        {
            return File.ReadAllLines(path);
        }

        //得到Text中每一行数据，没有空内容
        public static List<string> ReadAllLinesNoNull(string path)
        {
            List<string> result = new List<string>();
            string[] res = File.ReadAllLines(path);
            for (int i = 0; i < res.Length; i++)
            {
                if (!String.IsNullOrEmpty(res[i]))
                {
                    result.Add(res[i]);
                }
            }
            return result;
        }

        //复制文件到沙河路径
        public static bool CopyTxt(string destFileName)
        {
            TextAsset text = Resources.Load<TextAsset>(destFileName);
            if (text == null)
            {
                WDebug.Log(false, "Resources 下不存在文件 " + destFileName);
                return false;
            }
            string path = Application.persistentDataPath + "/" + destFileName + ".txt";
            if (!File.Exists(path))
            {
                Create(path, text.text);
            }
            return true;
        }
    }
}