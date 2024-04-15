
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework 
{                                                 
    public class ExcelData
    {
        public List<LanguageData> allLanguageData  = null;

        public void Init()
        {  
            allLanguageData = ExcelDataTools.GetDataList<LanguageData>();

        }
        
public LanguageData GetLanguageData(int id)
{
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (allLanguageData[i].id == id) 
        {
            return allLanguageData[i];
        }
    }
    Debug.LogError(id);
    return null;
}

public LanguageData GetLanguageDataid(int id)
{
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (allLanguageData[i].id == id) 
        {
            return allLanguageData[i];
        }
    }
    Debug.LogError(id);
    return null;
}

public List<int> GetListLanguageDataid()
{
    List<int> res = new List<int>();
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (!res.Contains(allLanguageData[i].id)) 
        {
            res.Add(allLanguageData[i].id);
        }
    }
    return res;
}



public LanguageData GetLanguageDataChinese(string Chinese)
{
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (allLanguageData[i].Chinese == Chinese) 
        {
            return allLanguageData[i];
        }
    }
    Debug.LogError(Chinese);
    return null;
}

public List<string> GetListLanguageDataChinese()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (!res.Contains(allLanguageData[i].Chinese)) 
        {
            res.Add(allLanguageData[i].Chinese);
        }
    }
    return res;
}



public LanguageData GetLanguageDataEnglish(string English)
{
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (allLanguageData[i].English == English) 
        {
            return allLanguageData[i];
        }
    }
    Debug.LogError(English);
    return null;
}

public List<string> GetListLanguageDataEnglish()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (!res.Contains(allLanguageData[i].English)) 
        {
            res.Add(allLanguageData[i].English);
        }
    }
    return res;
}



public LanguageData GetLanguageDataJapanese(string Japanese)
{
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (allLanguageData[i].Japanese == Japanese) 
        {
            return allLanguageData[i];
        }
    }
    Debug.LogError(Japanese);
    return null;
}

public List<string> GetListLanguageDataJapanese()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (!res.Contains(allLanguageData[i].Japanese)) 
        {
            res.Add(allLanguageData[i].Japanese);
        }
    }
    return res;
}



public LanguageData GetLanguageDataFrench(string French)
{
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (allLanguageData[i].French == French) 
        {
            return allLanguageData[i];
        }
    }
    Debug.LogError(French);
    return null;
}

public List<string> GetListLanguageDataFrench()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allLanguageData.Count; i++)
    {
        if (!res.Contains(allLanguageData[i].French)) 
        {
            res.Add(allLanguageData[i].French);
        }
    }
    return res;
}




    }
}
