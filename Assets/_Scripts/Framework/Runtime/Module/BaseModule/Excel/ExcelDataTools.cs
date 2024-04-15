
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using LitJson;
using UnityEngine;

public class ExcelDataTools
{
    public static List<T> GetDataList<T>() where T : class
    {
        List<T> res = new List<T>();
        string loPath = typeof(T).Name;
        TextAsset text = Resources.Load<TextAsset>("ExcelData/ExcelToJson/" + loPath);
        JsonData data = JsonMapper.ToObject(text.text);
        foreach (JsonData jsonData in data)
        {
            string loRes = jsonData.ToJson();
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            loRes = reg.Replace(loRes, delegate (Match m)
            {
                return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
            });
            T loT = JsonMapper.ToObject<T>(loRes);
            res.Add(loT);
        }
        return res;
    }
}

