using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework
{
    public class SavePlayerPrefs : ISave
    {
        public void Init(string path)
        {

        }

        public string GetString(string titleName)
        {
            if (titleName.IsNull()) return "";
            return PlayerPrefs.GetString(titleName, "");
        }

        public void SaveString(string titleName, string data)
        {
            if (data.IsNull()) return;
            PlayerPrefs.SetString(titleName, data);
        }
    }
}