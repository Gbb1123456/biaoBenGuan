using LitJson;
using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

namespace ZXKFrameworkEditor
{
    public static class EditorTools
    {
        //创建文件夹
        public static void CreateDirectory(string destFileName)
        {
            if (!Directory.Exists(destFileName))
                Directory.CreateDirectory(destFileName);
        }

        public static string GetJson(object self)
        {
            string loRes = JsonMapper.ToJson(self);
            return StringToUTF8(loRes);
        }

        public static string StringToUTF8(string self)
        {
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            self = reg.Replace(self, delegate (Match m)
            {
                return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
            });
            return self;
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

        public static bool IsNull<T>(this T selfObj) where T : class
        {
            return null == selfObj;
        }

        public static bool IsNotNull<T>(this T selfObj) where T : class
        {
            return null != selfObj;
        }


        public static string Read(string sPath)
        {
            if (File.Exists(sPath))
            {
                return File.ReadAllText(sPath, Encoding.UTF8);
            }
            return "";
        }

        public static void Create(string path, string info)
        {
            StreamWriter sw;
            FileInfo fi = new FileInfo(path);
            sw = fi.CreateText();
            sw.WriteLine(info);
            sw.Close();
            sw.Dispose();
        }

        public static bool DeleteAllFile(string fullPath)
        {
            //获取指定路径下面的所有资源文件  然后进行删除
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo direction = new DirectoryInfo(fullPath);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    string FilePath = fullPath + "/" + files[i].Name;
                    File.Delete(FilePath);
                }
                return true;
            }
            return false;
        }


        //复制目录文件到指定目录
        public static void CopyDirIntoDestDirectory(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!Directory.Exists(destFileName)) Directory.CreateDirectory(destFileName);
            foreach (var file in Directory.GetFiles(sourceFileName))
            {
                File.Copy(file, Path.Combine(destFileName, Path.GetFileName(file)), overwrite);
            }
            foreach (var d in Directory.GetDirectories(sourceFileName))
            {
                if (Path.GetFileName(d) != "Assetbundle")
                {
                    Debug.Log(Path.GetFileName(d));
                    CopyDirIntoDestDirectory(d, Path.Combine(destFileName, Path.GetFileName(d)), overwrite);
                }
            }
        }


        //获取路径文件的文件夹名
        public static string GetFileDicName(string path)
        {
            string loFullName = PathToNormal(path);
            string[] loDic = loFullName.Split('/');
            return loDic[loDic.Length - 2];
        }


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

        public static void Save<T>(string dataName,T t) where T : class, new()
        {
            string json = EditorTools.GetJson(t);
            PlayerPrefs.SetString(dataName, json);
        }

        public static T Load<T>(string dataName) where T : class, new()
        {
            T t = new T();
            string json = PlayerPrefs.GetString(dataName);
            if (json.IsNotNull())
            {
                try
                {
                    t = LitJson.JsonMapper.ToObject<T>(json);
                }
                catch (System.Exception)
                {
                    PlayerPrefs.DeleteKey(dataName);
                }
            }
            return t;
        }
    }
}