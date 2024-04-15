using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZXKFrameworkEditor
{
    /// <summary>
    /// Assetbundle资源编辑器
    /// </summary>
    public class AssetbundleBunildEditor : Editor
    {
        [MenuItem("ZXKFramework/AssetBundle/Name/Set AssetBundle Name")]
        public static void SelectTexture()
        {
            string suffix = ".assetbundle";
            Object[] asset = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
            for (int i = 0; i < asset.Length; i++)
            {
                if (asset[i].GetType() != typeof(DefaultAsset))
                {
                    AssetImporter ai = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(asset[i]));
                    Debug.Log(asset[i].name+"   "+ AssetDatabase.GetAssetPath(asset[i]));
                    ai.assetBundleName = asset[i].name.ToLower() + suffix; //更改文件夹中的资源AB名称
                }
            }
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/AssetBundle/Name/AssetBundle Name Clear")]
        public static void SelectClear()
        {
            Object[] selectedAsset = Selection.GetFiltered<Object>(SelectionMode.DeepAssets);
            for (int i = 0; i < selectedAsset.Length; i++)
            {
                AssetImporter ai = AssetImporter.GetAtPath(AssetDatabase.GetAssetPath(selectedAsset[i]));
                ai.assetBundleName = string.Empty; //清空文件夹中的资源AB名称
            }
            AssetDatabase.RemoveUnusedAssetBundleNames();
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（PersistentDataPath）/Build All")]
        static void BuildAllAssetBundlesAll()
        {
            Debug.Log("打包所有版本资源");
            BuildAssetBundlesAndroid();
            BuildAssetBundlesIos();
            BuildAllAssetBundlesWindows();
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（PersistentDataPath）/Build by Android")]
        static void BuildAssetBundlesAndroid()
        {
            string dir = Application.persistentDataPath + "/Assetbundle/Android";
            DirectoryTool(dir);
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.Android);
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（PersistentDataPath）/Build by IOS")]
        static void BuildAssetBundlesIos()
        {
            string dir = Application.persistentDataPath + "/Assetbundle/IOS";
            DirectoryTool(dir);
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.iOS);
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（PersistentDataPath）/Build by Windows")]
        static void BuildAllAssetBundlesWindows()
        {
            string dir = Application.persistentDataPath + "/Assetbundle/Windows";
            DirectoryTool(dir);
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（StreamingAssetsPath）/Build All")]
        static void BuildAllAssetBundlesAllStreamingAssetsPath()
        {
            Debug.Log("打包所有版本资源");
            BuildAssetBundlesAndroidStreamingAssetsPath();
            BuildAssetBundlesIosStreamingAssetsPath();
            BuildAllAssetBundlesWindowsStreamingAssetsPath();   
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（StreamingAssetsPath）/Build by Android")]
        static void BuildAssetBundlesAndroidStreamingAssetsPath()
        {
            string dir = Application.streamingAssetsPath + "/Assetbundle/Android";
            DirectoryTool(dir);
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.Android);
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（StreamingAssetsPath）/Build by IOS")]
        static void BuildAssetBundlesIosStreamingAssetsPath()
        {
            string dir = Application.streamingAssetsPath + "/Assetbundle/IOS";
            DirectoryTool(dir);
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.iOS);
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/AssetBundle/Build（StreamingAssetsPath）/Build by Windows")]
        static void BuildAllAssetBundlesWindowsStreamingAssetsPath()
        {
            string dir = Application.streamingAssetsPath + "/Assetbundle/Windows";
            DirectoryTool(dir);
            BuildPipeline.BuildAssetBundles(dir, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
            AssetDatabase.Refresh();
        }

        static void DirectoryTool(string dir)
        {
            if (Directory.Exists(dir))
                EditorTools.DeleteAllFile(dir);
            Directory.CreateDirectory(dir);
        }
    }
}
