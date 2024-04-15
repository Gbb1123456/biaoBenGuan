using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace ZXKFrameworkEditor
{
    public class TxtRemoveDuolicate : Editor
    {
        [MenuItem("ZXKFramework/Tools/TxtRemoveDuolicate")]                                                                                                                                                                                                          
        public static void Remove()
        {
            StringBuilder loStr = new StringBuilder();
            string path = Application.dataPath + "/Art/Font/TextMeshPro/";
            EditorTools.CreateDirectory(path);
            var files = new DirectoryInfo(path).GetFiles("*.*", SearchOption.AllDirectories).Where(s => s.FullName.EndsWith(".txt") || s.FullName.EndsWith(".xml"));
            foreach (var f in files)
            {
                loStr.Append(EditorTools.Read(f.FullName).Trim());
            }
            var strArray = loStr.ToString().Distinct().ToArray();
            EditorTools.Create(path + "TextProLab.txt", string.Join(string.Empty, strArray));
            AssetDatabase.Refresh();
        }
    }
}
