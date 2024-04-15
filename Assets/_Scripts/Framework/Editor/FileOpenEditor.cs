using UnityEditor;
using UnityEngine;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace ZXKFrameworkEditor
{
    /// <summary>
    /// 打开文件路径
    /// </summary>
    public class FileOpenEditor : Editor
    {
        [MenuItem("ZXKFramework/Open/PersistentDataPath")]
        public static void PersistentDataPath()
        {
            OpenDirectory(Application.persistentDataPath);
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/Open/StreamingAssetsPath")]
        public static void StreamingAssetsPath()
        {
            OpenDirectory(Application.streamingAssetsPath);
            AssetDatabase.Refresh();
        }

        [MenuItem("ZXKFramework/Open/DataPath")]
        public static void DataPath()
        {
            OpenDirectory(Application.dataPath);
            AssetDatabase.Refresh();
        }

        public static void OpenPersistentDataPathEnter(string _Path)
        {
            OpenFileName openFileName = new OpenFileName();
            openFileName.structSize = Marshal.SizeOf(openFileName);
            openFileName.filter = null; //"Excel文件(*.xlsx)\0*.xlsx";
            openFileName.file = new string(new char[256]);
            openFileName.maxFile = openFileName.file.Length;
            openFileName.fileTitle = new string(new char[64]);
            openFileName.maxFileTitle = openFileName.fileTitle.Length;
            openFileName.initialDir = _Path.Replace('/', '\\');//默认路径
            openFileName.flags = 0x00080000 | 0x00001000 | 0x00000800 | 0x00000008;
            openFileName.title = "窗口标题";
            WindowDll.GetOpenFileName(openFileName);
            if (WindowDll.GetOpenFileName(openFileName))
            {
                string url = "file:///" + openFileName.file;
                string[] strArray = url.Split('\\');
                string path = "";
                for (int i = 0; i < strArray.Length - 1; i++)
                {
                    path += strArray[i];
                    path += "\\";
                }
            }
        }

        public static void OpenDirectory(string path)
        {
            if (string.IsNullOrEmpty(path)) return;
            path = path.Replace("/", "\\");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            // 新开线程防止锁死
            Thread newThread = new Thread(new ParameterizedThreadStart(CmdOpenDirectory));
            newThread.Start(path);
            //可能360不信任
            //System.Diagnostics.Process.Start("explorer.exe", path);
        }

        private static void CmdOpenDirectory(object obj)
        {
            Process p = new Process();
            p.StartInfo.FileName = "cmd.exe";
            p.StartInfo.Arguments = "/c start " + obj.ToString();
            UnityEngine.Debug.Log(p.StartInfo.Arguments);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            p.Close();
        }
    }

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public class OpenFileName
    {
        public int structSize = 0;
        public IntPtr dlgOwner = IntPtr.Zero;
        public IntPtr instance = IntPtr.Zero;
        public String filter = null;
        public String customFilter = null;
        public int maxCustFilter = 0;
        public int filterIndex = 0;
        public String file = null;
        public int maxFile = 0;
        public String fileTitle = null;
        public int maxFileTitle = 0;
        public String initialDir = null;
        public String title = null;
        public int flags = 0;
        public short fileOffset = 0;
        public short fileExtension = 0;
        public String defExt = null;
        public IntPtr custData = IntPtr.Zero;
        public IntPtr hook = IntPtr.Zero;
        public String templateName = null;
        public IntPtr reservedPtr = IntPtr.Zero;
        public int reservedInt = 0;
        public int flagsEx = 0;
    }

    public static class WindowDll
    {
        [DllImport("Comdlg32.dll", SetLastError = true, ThrowOnUnmappableChar = true, CharSet = CharSet.Auto)]
        public static extern bool GetOpenFileName([In, Out] OpenFileName ofn);
    }
}