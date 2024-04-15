using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework
{
    public class SaveText : ISave
    {
        private string dicPath;

        public void Init(string path)
        {
            dicPath = path.IsNull() ? "" : dicPath + "/";
        }

        public string GetString(string titleName)
        {
            if (titleName.IsNull()) return "";
            return TextTools.Read(GetPath(titleName));
        }

        public void SaveString(string titleName, string data)
        {
            if (data.IsNull()) return;
            string loPath = Application.persistentDataPath + dicPath + "/" + titleName + ".txt";
            TextTools.Create(GetPath(titleName), UnityTools.StringToUTF8(data));
        }

        string GetPath(string titleName)
        {          
            string loPath = Application.persistentDataPath + "/" + dicPath;
            UnityTools.CreateDirectory(loPath);
            loPath = loPath + titleName + ".txt";
            //Debug.Log("SaveText : " + loPath);
            return loPath;
        }
    }
}