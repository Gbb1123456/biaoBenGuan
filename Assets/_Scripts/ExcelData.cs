
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework 
{                                                 
    public class ExcelData
    {
        public List<LanguageData> allLanguageData  = null;
public List<MainData> allMainData  = null;

        public void Init()
        {  
            allLanguageData = ExcelDataTools.GetDataList<LanguageData>();
allMainData = ExcelDataTools.GetDataList<MainData>();

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

public MainData GetMainData(int id)
{
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (allMainData[i].id == id) 
        {
            return allMainData[i];
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



public MainData GetMainDataid(int id)
{
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (allMainData[i].id == id) 
        {
            return allMainData[i];
        }
    }
    Debug.LogError(id);
    return null;
}

public List<int> GetListMainDataid()
{
    List<int> res = new List<int>();
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (!res.Contains(allMainData[i].id)) 
        {
            res.Add(allMainData[i].id);
        }
    }
    return res;
}



public MainData GetMainDataTopTxt(string TopTxt)
{
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (allMainData[i].TopTxt == TopTxt) 
        {
            return allMainData[i];
        }
    }
    Debug.LogError(TopTxt);
    return null;
}

public List<string> GetListMainDataTopTxt()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (!res.Contains(allMainData[i].TopTxt)) 
        {
            res.Add(allMainData[i].TopTxt);
        }
    }
    return res;
}



public MainData GetMainDataName(string Name)
{
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (allMainData[i].Name == Name) 
        {
            return allMainData[i];
        }
    }
    Debug.LogError(Name);
    return null;
}

public List<string> GetListMainDataName()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (!res.Contains(allMainData[i].Name)) 
        {
            res.Add(allMainData[i].Name);
        }
    }
    return res;
}



public MainData GetMainDataTxt(string Txt)
{
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (allMainData[i].Txt == Txt) 
        {
            return allMainData[i];
        }
    }
    Debug.LogError(Txt);
    return null;
}

public List<string> GetListMainDataTxt()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (!res.Contains(allMainData[i].Txt)) 
        {
            res.Add(allMainData[i].Txt);
        }
    }
    return res;
}



public MainData GetMainDataSpritePath(string SpritePath)
{
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (allMainData[i].SpritePath == SpritePath) 
        {
            return allMainData[i];
        }
    }
    Debug.LogError(SpritePath);
    return null;
}

public List<string> GetListMainDataSpritePath()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (!res.Contains(allMainData[i].SpritePath)) 
        {
            res.Add(allMainData[i].SpritePath);
        }
    }
    return res;
}



public MainData GetMainDataSoundPath(string SoundPath)
{
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (allMainData[i].SoundPath == SoundPath) 
        {
            return allMainData[i];
        }
    }
    Debug.LogError(SoundPath);
    return null;
}

public List<string> GetListMainDataSoundPath()
{
    List<string> res = new List<string>();
    for (int i = 0; i < allMainData.Count; i++)
    {
        if (!res.Contains(allMainData[i].SoundPath)) 
        {
            res.Add(allMainData[i].SoundPath);
        }
    }
    return res;
}




    }
}
