
using UnityEngine;
using System;
namespace ZXKFramework
{
    public interface IUIManager
    {
        void Init(GameObject canvas, string uiDirPath);
        void SetResType(IRes res);
        void AddUI<T>(Action callBack = null) where T : UIBase;
        void ShowUI<T>(Action<UIBase> callBack = null, params object[] obj) where T : UIBase;
        void ShowUIAndCloseOther<T>(Action<UIBase> callBack = null, params object[] obj) where T : UIBase;
        void CloseUI<T>() where T : UIBase;
        T GetUI<T>() where T : UIBase;
        void CloseGroup(string parentName);
        void CloseGroup();
        void Clean();
        void OnUpdate();
        void Destroy();
    }
}