using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace ZXKFramework
{
    public class PathTools
    {
        //获取路径文件名
        public static string GetFileName(string path)
        {
            return Path.GetFileNameWithoutExtension(path);
        }
       
        //获取路径文件的文件夹名
        public static string GetFileDicName(string path)
        {
            string loFullName = PathToNormal(path);
            string[] loDic = loFullName.Split('/');
            return loDic[loDic.Length - 2];
        }

        //修改路劲的符号 \改为/
        public static string PathToNormal(string path)
        {
            string loName = "";
            char[] loPath = path.ToCharArray();
            foreach (char item in loPath)
            {
                if (item == '\\')
                {
                    loName += '/';
                }
                else
                {
                    loName += item;
                }
            }
            return loName;
        }
    }
}