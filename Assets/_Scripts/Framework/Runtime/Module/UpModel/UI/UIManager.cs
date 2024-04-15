
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace ZXKFramework
{
    public class UIManager : IUIManager
    {
        public GameObject canvas { get; set; }
        public string curPanelName { get; set; }
        private Dictionary<string, UIBase> allUI { get; set; } = new Dictionary<string, UIBase>();
        private string uiDir = "";

        IRes resManager;
        public void SetResType(IRes res)
        {
            resManager = res;
        }

        public void Init(GameObject canvas, string uiDirPath)
        {
            this.canvas = canvas;
            uiDir = uiDirPath;
        }

        public void AddUI<T>(Action callBack = null) where T : UIBase
        {
            string tipType = typeof(T).Name;
            if (canvas == null) return;
            if (!allUI.ContainsKey(tipType))
            {
                GameObject loUi = null;
                if (!Game.Instance.canvasTransformFindUI)
                {
                    string path = string.IsNullOrEmpty(uiDir) ? tipType : uiDir + "/" + tipType;
                    resManager?.Load<GameObject>(path, ui =>
                    {
                        if (null == ui) return;
                        loUi = GameObject.Instantiate<GameObject>(ui);
                        RectTransform rect = loUi.GetOrAddComponent<RectTransform>();
                        rect.anchoredPosition3D = Vector3.zero;
                        rect.anchoredPosition = Vector2.zero;
                        allUI.Add(tipType, AddUI<T>(loUi));
                        callBack?.Invoke();
                    });
                }
                else
                {
                    loUi = canvas.FindFirst(tipType);
                    if (null == loUi) return;
                    allUI.Add(tipType, AddUI<T>(loUi));
                    callBack?.Invoke();
                }
            }
            else
            {
                callBack?.Invoke();
            }
        }

        T AddUI<T>(GameObject loUi) where T : UIBase
        {
            loUi.transform.parent = canvas.transform;
            loUi.transform.localPosition = Vector3.zero;
            T uibase = loUi.GetOrAddComponent<T>();
            uibase.Init(this);
            uibase.SetActive(false);
            //注册到MVC
            MVC.RegisterView(uibase);
            return uibase;
        }

        public void ShowUI<T>(Action<UIBase> callBack = null, params object[] obj) where T : UIBase
        {
            string tipType = typeof(T).Name;
            if (canvas == null) return;
            AddUI<T>(() =>
            {
                if (!allUI.ContainsKey(tipType)) return;
                allUI[tipType].ShowData(obj);//保存展示数据
                allUI[tipType].SetActive(true);
                curPanelName = tipType;
                callBack?.Invoke(allUI[tipType]);
            });
        }

        public void ShowUIAndCloseOther<T>(Action<UIBase> callBack = null, params object[] obj) where T : UIBase
        {
            ShowUI<T>(loUIBase =>
            {
                if (loUIBase != null)
                {
                    foreach (UIBase uiTem in allUI.Values)
                    {
                        if (uiTem != loUIBase && uiTem.GroupName == loUIBase.GroupName)
                        {
                            uiTem.SetActive(false);
                        }
                    }
                    callBack?.Invoke(loUIBase);
                }
            }, obj);
        }

        public void CloseUI<T>() where T : UIBase
        {
            string tipType = typeof(T).Name;
            if (canvas == null || !allUI.ContainsKey(tipType)) return;
            Debug.Log("CloseUI " + tipType);
            allUI[tipType].SetActive(false);
        }

        public T GetUI<T>() where T : UIBase
        {
            string tipType = typeof(T).Name;
            if (canvas == null || !allUI.ContainsKey(tipType)) return null;
            return allUI[tipType] as T;
        }

        public void CloseGroup(string parentName)
        {
            foreach (UIBase uiTem in allUI.Values)
            {
                if (uiTem.GroupName == parentName && uiTem.gameObject.activeInHierarchy)
                {
                    uiTem.SetActive(false);
                }
            }
        }

        public void CloseGroup()
        {
            foreach (UIBase uiTem in allUI.Values)
            {
                if (uiTem.gameObject.activeInHierarchy)
                {
                    uiTem.SetActive(false);
                }
            }
        }

        public void Clean()
        {
            allUI.Clear();
            canvas = null;
        }

        public void OnUpdate()
        {
            foreach (var uiBase in allUI.Values)
            {
                uiBase?.OnUpdate();
            }
        }

        public void Destroy()
        {
            foreach (var uiBase in allUI.Values)
            {
                uiBase?.Destroy();
            }
            Clean();
        }
    }
}


