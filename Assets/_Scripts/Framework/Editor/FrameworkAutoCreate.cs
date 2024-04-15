using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;
using System;

namespace ZXKFrameworkEditor
{
    public class CreateCSData
    {
        public string scriptsPath = "_Scripts/Application/Test";
        public string applicationName = "Test";
        public string author = "";
        public List<CsData> stateName = new List<CsData>();
        public List<CsData> uiName = new List<CsData>();
        public List<CsData> eventName = new List<CsData>();
    }

    public class CsData
    {
        public bool isCreate = true;
        public string name;
    }

    public class FrameworkAutoCreate : EditorWindow
    {
        public int width = 500;
        public CreateCSData loCreateCSData = new CreateCSData();
        private bool isLoad = false;
        static FrameworkAutoCreate toJson;
        [UnityEditor.MenuItem("ZXKFramework/CreateCSAuto")]
        static void ExceltoJson()
        {
            toJson = (FrameworkAutoCreate)EditorWindow.GetWindow(typeof(FrameworkAutoCreate), true, "FrameworkAutoCreate");
            toJson.Show();
        }

        public bool Cons(List<CsData> allData, CsData data)
        {
            if (allData.Count != 0)
            {
                foreach (var item in allData)
                {
                    if (data.name == item.name)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private void OnGUI()
        {
            if (!isLoad)
            {
                isLoad = true;
                Load();
            }

            GUILayout.BeginVertical();

            GUILayout.Label("---代码自动创建---", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("代码创建路径:(必填)");
            loCreateCSData.scriptsPath = GUILayout.TextField(loCreateCSData.scriptsPath, GUILayout.Width(width));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("工程名:(必填)");
            loCreateCSData.applicationName = GUILayout.TextField(loCreateCSData.applicationName, GUILayout.Width(width));
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("作者:（可不填）");
            loCreateCSData.author = GUILayout.TextField(loCreateCSData.author, GUILayout.Width(width));
            GUILayout.EndHorizontal();

            GUILayout.Label("", EditorStyles.boldLabel);
            GUILayout.Label("---状态---", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("添加状态"))
            {
                loCreateCSData.stateName.Add(new CsData());
            }
            if (GUILayout.Button("删除状态"))
            {
                if (loCreateCSData.stateName.Count == 0) return;
                loCreateCSData.stateName.Remove(loCreateCSData.stateName[loCreateCSData.stateName.Count - 1]);
            }
            GUILayout.EndHorizontal();

            for (int i = 0; i < loCreateCSData.stateName.Count; i++)
            {
                GUILayout.BeginHorizontal();
                loCreateCSData.stateName[i].isCreate = GUILayout.Toggle(loCreateCSData.stateName[i].isCreate, "是否创建脚本");
                loCreateCSData.stateName[i].name = GUILayout.TextField(loCreateCSData.stateName[i].name, GUILayout.Width(width));
                GUILayout.EndHorizontal();
            }

            GUILayout.Label("", EditorStyles.boldLabel);
            GUILayout.Label("---UI界面---", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("添加UI"))
            {
                loCreateCSData.uiName.Add(new CsData());
            }
            if (GUILayout.Button("删除UI"))
            {
                if (loCreateCSData.uiName.Count == 0) return;
                loCreateCSData.uiName.Remove(loCreateCSData.uiName[loCreateCSData.uiName.Count - 1]);
            }
            GUILayout.EndHorizontal();

            for (int i = 0; i < loCreateCSData.uiName.Count; i++)
            {
                GUILayout.BeginHorizontal();
                loCreateCSData.uiName[i].isCreate = GUILayout.Toggle(loCreateCSData.uiName[i].isCreate, "是否创建脚本");
                loCreateCSData.uiName[i].name = GUILayout.TextField(loCreateCSData.uiName[i].name, GUILayout.Width(width));
                GUILayout.EndHorizontal();
            }

            GUILayout.Label("", EditorStyles.boldLabel);
            GUILayout.Label("---事件---", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("添加事件"))
            {
                loCreateCSData.eventName.Add(new CsData());
            }
            if (GUILayout.Button("删除事件"))
            {
                if (loCreateCSData.eventName.Count == 0) return;
                loCreateCSData.eventName.Remove(loCreateCSData.eventName[loCreateCSData.eventName.Count - 1]);
            }
            GUILayout.EndHorizontal();

            for (int i = 0; i < loCreateCSData.eventName.Count; i++)
            {
                GUILayout.BeginHorizontal();
                loCreateCSData.eventName[i].isCreate = GUILayout.Toggle(loCreateCSData.eventName[i].isCreate, "是否创建脚本");
                loCreateCSData.eventName[i].name = GUILayout.TextField(loCreateCSData.eventName[i].name, GUILayout.Width(width));
                GUILayout.EndHorizontal();
            }

            GUILayout.Label("", EditorStyles.boldLabel);
            if (GUILayout.Button("创建并保存"))
            {
                Save();
                Create();
                toJson.Close();
                AssetDatabase.Refresh();
            }

            GUILayout.Label("", EditorStyles.boldLabel);
            GUILayout.Label("---保存---", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("保存")) Save();
            if (GUILayout.Button("读取")) Load();
            if (GUILayout.Button("刷新")) Resh();
            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        void Save()
        {
            BaseState();
            string json = EditorTools.GetJson(loCreateCSData);
            PlayerPrefs.SetString("FrameworkAutoCreateCreateCSData", json);
        }

        void Load()
        {
            string json = PlayerPrefs.GetString("FrameworkAutoCreateCreateCSData");
            if (json.IsNull()) return;
            try
            {
                loCreateCSData = LitJson.JsonMapper.ToObject<CreateCSData>(json);
            }
            catch (System.Exception)
            {
                PlayerPrefs.DeleteKey("FrameworkAutoCreateCreateCSData");
            }
        }

        void Resh()
        {
            Debug.Log("刷新界面");
            BaseState();
            OnGUI();
            Save();
        }

        void Create()
        {
            Debug.Log("Create");
            if (loCreateCSData.scriptsPath.IsNull() || loCreateCSData.applicationName.IsNull()) return;
            BaseState();
            if (loCreateCSData.uiName != null && loCreateCSData.uiName.Count != 0)
            {
                foreach (var item in loCreateCSData.uiName)
                {
                    if (item.IsNotNull() && item.isCreate)
                    {
                        CreateUI(item.name);
                    }
                }
            }
            if (loCreateCSData.stateName != null && loCreateCSData.stateName.Count != 0)
            {
                foreach (var item in loCreateCSData.stateName)
                {
                    if (item.IsNotNull() && item.isCreate)
                    {
                        CreateState(item.name);
                    }
                }
            }
            if (loCreateCSData.eventName != null && loCreateCSData.eventName.Count != 0)
            {
                foreach (var item in loCreateCSData.eventName)
                {
                    if (item.IsNotNull() && item.isCreate)
                    {
                        CreateEvent(item.name);
                    }
                }
            }
            CreateLauncher();
            CreateModel();
            CreateEvent("");
        }

        void BaseState()
        {
            CsData loCsData = new CsData() { name = "Start" };
            if (loCreateCSData.stateName != null && !Cons(loCreateCSData.stateName, loCsData))
            {
                loCreateCSData.stateName.Add(loCsData);
            }
            foreach (var item in loCreateCSData.stateName)
            {
                if (loCreateCSData.uiName != null && !Cons(loCreateCSData.uiName, item))
                {
                    loCreateCSData.uiName.Add(GetNew(item));
                }
                if (loCreateCSData.eventName != null && !Cons(loCreateCSData.eventName, item))
                {
                    loCreateCSData.eventName.Add(GetNew(item));
                }
            }
        }

        public CsData GetNew(CsData data)
        {
            CsData loCsData = new CsData();
            loCsData.isCreate = data.isCreate;
            loCsData.name = data.name;
            return loCsData;
        }



        string GetTitleDoc()
        {
            if (loCreateCSData.author.IsNull()) return "";
            string titleDoc = @"
//------------------------------------------------------
// 创建人: *		
// 创建时间: #	   
// 脚本作用: 
//------------------------------------------------------
";
            titleDoc = titleDoc.Replace("*", loCreateCSData.author);
            titleDoc = titleDoc.Replace("#", DateTime.Now.ToString());
            return titleDoc;
        }



        void CreateLauncher()
        {
            string fangFaBase = GetTitleDoc() + @"
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

namespace #
{
    public class #Launcher : MonoBehaviour
    {
        private #Model lo#Model;
        public static #Launcher Instance;
        protected void Start()
        {
            Instance = this;
            MVC.RegisterModel(new #Model());
            lo#Model = MVC.GetModel<#Model>();
            lo#Model.Init();
            Game.Instance.fsm.ChangeState<#StartState>();
        }
    }
}

";
            fangFaBase = fangFaBase.Replace("#", loCreateCSData.applicationName);
            string loPath = Application.dataPath + "/" + loCreateCSData.scriptsPath + "/";
            EditorTools.CreateDirectory(loPath);
            string classPath = loPath + loCreateCSData.applicationName + "Launcher.cs";
            if (File.Exists(classPath)) return;
            Debug.Log(classPath);
            File.WriteAllText(classPath, fangFaBase, Encoding.UTF8);
        }

        void CreateModel()
        {
            string fangFaBase = GetTitleDoc() + @"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

namespace #
{
    public class #Model : Model
    {
        public ExcelData excelData = new ExcelData();
        public override string Name => ""#Model"";

        public void Init()
        {
            excelData.Init();
        }

        public void ResetData()
        {

        }
    }
}

";
            fangFaBase = fangFaBase.Replace("#", loCreateCSData.applicationName);
            string loPath = Application.dataPath + "/" + loCreateCSData.scriptsPath + "/Model/";
            EditorTools.CreateDirectory(loPath);
            string classPath = loPath + loCreateCSData.applicationName + "Model.cs";
            if (File.Exists(classPath)) return;
            Debug.Log(classPath);
            File.WriteAllText(classPath, fangFaBase, Encoding.UTF8);
        }

        void CreateEvent(string dataName)
        {
            string fangFaBase = GetTitleDoc() + @"
using ZXKFramework;

namespace *
{
    public class #Event : IGameEvent
    {
        public string result;
    }
}

";
            fangFaBase = fangFaBase.Replace("#", loCreateCSData.applicationName + dataName);
            fangFaBase = fangFaBase.Replace("*", loCreateCSData.applicationName);
            string loPath = Application.dataPath + "/" + loCreateCSData.scriptsPath + "/Event/";
            EditorTools.CreateDirectory(loPath);
            string classPath = loPath + loCreateCSData.applicationName + dataName + "Event.cs";
            if (File.Exists(classPath)) return;
            Debug.Log(classPath);
            File.WriteAllText(classPath, fangFaBase, Encoding.UTF8);
        }

        void CreateUI(string uiName)
        {
            string fangFaBase = GetTitleDoc() + @"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

namespace *
{
    public class #UI : UIBase
    {
        private *Model lo*Model;

        public override string GroupName =>  UIGroup.Main.ToString();

        public override string Name => ""#UI"";

        public override void Init(IUIManager uictrl)
        {
            base.Init(uictrl);
            lo*Model = MVC.GetModel<*Model>();
        }
    }
}
";
            fangFaBase = fangFaBase.Replace("#", loCreateCSData.applicationName + uiName);
            fangFaBase = fangFaBase.Replace("*", loCreateCSData.applicationName);
            string loPath = Application.dataPath + "/" + loCreateCSData.scriptsPath + "/UI/";
            EditorTools.CreateDirectory(loPath);
            string classPath = loPath + loCreateCSData.applicationName + uiName + "UI.cs";
            if (File.Exists(classPath)) return;
            Debug.Log(classPath);
            File.WriteAllText(classPath, fangFaBase, Encoding.UTF8);
        }

        void CreateState(string dataName)
        {
            string fangFaBase = GetTitleDoc() + @"
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

namespace *
{
    public class #State : StateBase
    {
        private *Model lo*Model;
        public override void Init(IFSM stateMachine)
        {
            base.Init(stateMachine);
            lo*Model = MVC.GetModel<*Model>();
        }
        public override void OnEnter(params object[] obj)
        {
            base.OnEnter(obj);
            Game.Instance.uiManager.ShowUI<#UI>();
            Game.Instance.eventManager.AddListener<#Event>(on#Event);
            Debug.Log(""#State OnEnter"");
        }
        public override void OnUpdate()
        {
            base.OnUpdate();
        }
        public override void OnExit()
        {
            base.OnExit();
            Game.Instance.uiManager.CloseUI<#UI>();
            Game.Instance.eventManager.RemoveListener<#Event>(on#Event);
            Debug.Log(""#State OnExit"");
        }

        private void on#Event(#Event e)
        {
            switch (e.result)
            {
                case ""Test"":
                    break;
            default:
                    break;
            }
        }
    }
}
";
            fangFaBase = fangFaBase.Replace("#", loCreateCSData.applicationName + dataName);
            fangFaBase = fangFaBase.Replace("*", loCreateCSData.applicationName);
            string loPath = Application.dataPath + "/" + loCreateCSData.scriptsPath + "/State/";
            EditorTools.CreateDirectory(loPath);
            string classPath = loPath + loCreateCSData.applicationName + dataName + "State.cs";
            if (File.Exists(classPath)) return;
            Debug.Log(classPath);
            File.WriteAllText(classPath, fangFaBase, Encoding.UTF8);
        }
    }
}
