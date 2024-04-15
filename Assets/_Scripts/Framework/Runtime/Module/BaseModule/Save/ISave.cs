using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZXKFramework
{
    public interface ISave
    {
        void Init(string path);
        void SaveString(string titleName,string data);
        string GetString(string titleName);
    }
}