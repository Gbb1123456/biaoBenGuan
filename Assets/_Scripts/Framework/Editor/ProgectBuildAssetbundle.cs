
using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;

namespace ZXKFrameworkEditor
{
    public class ProgectBuildAssetbundleData
    {
        public string progectName = "";
    }

    //工程打成AB包
    public class ProgectBuildAssetbundle : EditorWindow
    {
        ProgectBuildAssetbundleData loProgectBuildAssetbundleData = new ProgectBuildAssetbundleData();
        private bool isLoad = false;

        [UnityEditor.MenuItem("ZXKFramework/AssetBundle/ProgectBuildAssetbundle")]
        static void ExceltoJson()
        {
            ProgectBuildAssetbundle toJson = (ProgectBuildAssetbundle)EditorWindow.GetWindow(typeof(ProgectBuildAssetbundle), true, "ProgectBuildAssetbundle");
            toJson.Show();
        }

        private void OnGUI()
        {
            if (!isLoad)
            {
                isLoad = true;
                Load();
            }

            GUILayout.Label("------工程打成AB包", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            GUILayout.Label("项目名称（必填）");
            loProgectBuildAssetbundleData.progectName = GUILayout.TextField(loProgectBuildAssetbundleData.progectName, GUILayout.Width(300));
            GUILayout.EndHorizontal();

            GUILayout.Label("------打包到StreamingAssetsPath", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Windows/WebGL"))
            {
                string basePath = Application.streamingAssetsPath + "/Assetbundle/" + loProgectBuildAssetbundleData.progectName + "/Windows/";
                Build(basePath, BuildTarget.StandaloneWindows);
            }
            if (GUILayout.Button("Android"))
            {
                string basePath = Application.streamingAssetsPath + "/Assetbundle/" + loProgectBuildAssetbundleData.progectName + "/Android/";
                Build(basePath, BuildTarget.Android);
            }
            if (GUILayout.Button("IOS"))
            {
                string basePath = Application.streamingAssetsPath + "/Assetbundle/" + loProgectBuildAssetbundleData.progectName + "/IOS/";
                Build(basePath, BuildTarget.iOS);
            }
            GUILayout.EndHorizontal();

            GUILayout.Label("------打包到PersistentDataPath", EditorStyles.boldLabel);

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Windows/WebGL"))
            {
                string basePath = Application.persistentDataPath + "/Assetbundle/" + loProgectBuildAssetbundleData.progectName + "/Windows//";
                Build(basePath, BuildTarget.StandaloneWindows);
            }
            if (GUILayout.Button("Android"))
            {
                string basePath = Application.persistentDataPath + "/Assetbundle/" + loProgectBuildAssetbundleData.progectName + "/Android/";
                Build(basePath, BuildTarget.Android);
            }
            if (GUILayout.Button("IOS"))
            {
                string basePath = Application.persistentDataPath + "/Assetbundle/" + loProgectBuildAssetbundleData.progectName + "/IOS/";
                Build(basePath, BuildTarget.iOS);
            }
            GUILayout.EndHorizontal();
        }

        public void Build(string cPath, BuildTarget target)
        {
            if (string.IsNullOrEmpty(loProgectBuildAssetbundleData.progectName)) return;
            Save();

            //去掉名称
            string[] oldABNames = AssetDatabase.GetAllAssetBundleNames();
            for (int i = 0; i < oldABNames.Length; i++)
            {
                AssetDatabase.RemoveAssetBundleName(oldABNames[i], true);
            }

            EditorTools.DeleteAllFile(cPath);//清空资源
            EditorTools.CreateDirectory(cPath);

            //StreamingAssets 进行整体复制
            string streamingAssetsDataPath = cPath + "_StreamingAssets/";
            EditorTools.CreateDirectory(streamingAssetsDataPath);
            EditorTools.CopyDirIntoDestDirectory(Application.streamingAssetsPath, streamingAssetsDataPath, true);

            //设置特殊文件夹Ab包名_Scenes
            SetABNameDic(Application.dataPath + "/_Scenes/");
            SetABNameDic(Application.dataPath + "/Resources/");

            //开始打包
            BuildPipeline.BuildAssetBundles(cPath, BuildAssetBundleOptions.None, target);

            Debug.Log("打包成功：" + cPath);
            AssetDatabase.Refresh();
        }


        void SetABNameDic(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    if (NextFile.FullName.EndsWith(".meta") || NextFile.FullName.EndsWith(".cs") || NextFile.FullName.EndsWith(".xlsx")) continue;
                    string abName = Path.GetFileNameWithoutExtension(NextFile.Name).ToLower();
                    string loFullName = EditorTools.PathToNormal(NextFile.FullName);
                    string loAbPath = "Assets/" + loFullName.Replace(Application.dataPath, "");
                    abName = EditorTools.PathToNormal(Path.GetDirectoryName(loAbPath)).Replace("Assets/", "") + "/" + abName;
                    SetABName(abName, loAbPath);
                }
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    SetABNameDic(NextFolder.FullName);
                }
            }
        }

        void SetABName(string name, string path)
        {
            AssetImporter assetImporter = AssetImporter.GetAtPath(path);
            if (assetImporter == null)
            {
                Debug.LogError("不存在此路径文件：" + path);
            }
            else
            {
                assetImporter.assetBundleName = name + ".assetbundle";
            }
        }

        string saveName = "FrameworkAutoCreateCreateCSData";
        void Save()
        {
            EditorTools.Save(saveName, loProgectBuildAssetbundleData);
        }
        void Load()
        {
            loProgectBuildAssetbundleData = EditorTools.Load<ProgectBuildAssetbundleData>(saveName);
        }
    }
}