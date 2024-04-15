
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Data;
using Excel;
using System.IO;
using LitJson;
using System.Text;
using System.Text.RegularExpressions;
using System;
using System.Reflection;
using System.CodeDom;
using System.CodeDom.Compiler;

namespace ZXKFrameworkEditor
{
    public class ExcelToJson : EditorWindow
    {
        private string JsonPath;
        string CSharpPath;
        string JsonName;
        List<string> dataDoc = new List<string>();
        List<string> dataType = new List<string>();
        List<string> dataName = new List<string>();
        List<string[]> ExcelDateList = new List<string[]>();
        List<string> allDataClassName = new List<string>();//所有类名
        StringBuilder allFindUnc = new StringBuilder();
        string nameSpace = "";

        [UnityEditor.MenuItem("ZXKFramework/ExcelToJson")]
        static void ExceltoJson()
        {
            ExcelToJson toJson = (ExcelToJson)EditorWindow.GetWindow(typeof(ExcelToJson), true, "ExcelToJson");
            toJson.Show();
        }

        private void OnGUI()
        {
            GUILayout.Label("---配表代码自动生成---", EditorStyles.boldLabel);
            GUILayout.BeginHorizontal();
            GUILayout.Label("命名空间:(选填，用于代码隔离)");
            nameSpace = GUILayout.TextField(nameSpace, GUILayout.Width(300));
            GUILayout.EndHorizontal();
            if (GUILayout.Button("选择需要转换的excel文件"))
            {
                GetAllExcelPath();
            }
        }

        #region Excel文件处理
        void GetAllExcelPath()
        {
            EditorTools.CreateDirectory(Application.dataPath + "/Resources/ExcelData/Excel");
            EditorTools.CreateDirectory(Application.dataPath + "/Resources/ExcelData/CSharpPath");
            EditorTools.CreateDirectory(Application.dataPath + "/Resources/ExcelData/ExcelToJson");

            string loPath = Application.dataPath + "/Resources/ExcelData/Excel";
            DirectoryInfo dir = new DirectoryInfo(loPath);
            FileInfo[] fil = dir.GetFiles();
            allDataClassName.Clear();
            foreach (FileInfo f in fil)
            {
                if (f.FullName.EndsWith(".meta"))
                {
                    continue;
                }
                Debug.Log(f.FullName);
                ReadExcel(f.FullName.Replace("\\", "/"), Path.GetFileNameWithoutExtension(f.Name));
            }
            CreatCSharpExcelData();
            AssetDatabase.Refresh();
            Debug.Log("生成成功");
        }

        /// <summary>
        /// 读取Excel
        /// </summary>
        /// <param name="path">excel路径</param>
        /// <param name="columnNum">列</param>
        /// <param name="rowNum">行</param>
        void ReadExcel(string path, string fileName)
        {
            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            DataSet data = excelReader.AsDataSet();

            dataDoc.Clear();
            dataName.Clear();
            dataType.Clear();
            ExcelDateList.Clear();

            //读取Excel的所有页签
            for (int i = 0; i < data.Tables.Count; i++)
            {
                //i代表每一页，一般只有一页，下面为每一页的每一行 每一列
                DataRowCollection dataRow = data.Tables[i].Rows;            // 每行
                DataColumnCollection dataColumn = data.Tables[i].Columns;   // 每列

                string tableName = fileName;
                JsonName = fileName + ".json";
                JsonPath = UnityEngine.Application.dataPath + "/Resources/ExcelData/ExcelToJson/" + JsonName;
                //循环，第一行
                for (int rowNum = 0; rowNum < data.Tables[i].Rows.Count; rowNum++)
                {
                    //第一行的每一列
                    string[] table = new string[data.Tables[i].Columns.Count];
                    for (int columnNum = 0; columnNum < data.Tables[i].Columns.Count; columnNum++)
                    {
                        if (rowNum == 0)
                        {
                            //第一行：解释说明
                        }
                        else if (rowNum == 1)
                        {
                            dataDoc.Add(data.Tables[i].Rows[rowNum][columnNum].ToString());
                        }
                        else if (rowNum == 2)
                        {
                            dataType.Add(data.Tables[i].Rows[rowNum][columnNum].ToString());
                        }
                        else if (rowNum == 3)
                        {
                            dataName.Add(data.Tables[i].Rows[rowNum][columnNum].ToString());
                        }
                        else
                        {
                            //Debug.Log(data.Tables[i].Rows[rowNum][columnNum].ToString() + "\n");
                            table[columnNum] = data.Tables[i].Rows[rowNum][columnNum].ToString();
                        }
                    }
                    if (rowNum > 3)
                    {
                        //将一行数据存入list
                        ExcelDateList.Add(table);
                    }
                }
                CreatJsonFile();
                //CreatCSharp(tableName);
                CreatCSharpString(tableName);
                allDataClassName.Add(tableName);

                for (int j = 0; j < dataName.Count; j++)
                {
                    string fangFaBase = @"
public # Get#*(% *)
{
    for (int i = 0; i < all#.Count; i++)
    {
        if (all#[i].* == *) 
        {
            return all#[i];
        }
    }
    Debug.LogError(*);
    return null;
}

public List<%> GetList#*()
{
    List<%> res = new List<%>();
    for (int i = 0; i < all#.Count; i++)
    {
        if (!res.Contains(all#[i].*)) 
        {
            res.Add(all#[i].*);
        }
    }
    return res;
}

";
                    fangFaBase = fangFaBase.Replace("#", tableName);
                    fangFaBase = fangFaBase.Replace("*", dataName[j]);
                    fangFaBase = fangFaBase.Replace("%", dataType[j]);
                    allFindUnc.AppendLine(fangFaBase);

                    List<string> res = new List<string>();

                }
            }
        }

        #endregion

        #region Excel转json
        void CreatJsonFile()
        {
            if (File.Exists(JsonPath))
            {
                File.Delete(JsonPath);
            }
            JsonData jsonDatas = new JsonData();
            jsonDatas.SetJsonType(JsonType.Array);
            for (int i = 0; i < ExcelDateList.Count; i++)
            {
                JsonData jsonData = new JsonData();
                for (int j = 0; j < dataName.Count; j++)
                {
                    string tempData = dataType[j].Trim();
                    string loData = ExcelDateList[i][j].Trim();
                    //Debug.Log("类型 " + tempData + "  " + loData);
                    try
                    {
                        switch (tempData)
                        {
                            case "int":
                                int resInt = 0;
                                if (!string.IsNullOrEmpty(loData))
                                {
                                    resInt = Int32.Parse(loData);
                                }
                                jsonData[dataName[j]] = resInt;
                                break;
                            case "float":
                                float resFloat = 0;
                                if (!string.IsNullOrEmpty(loData))
                                {
                                    resFloat = float.Parse(loData);
                                }
                                jsonData[dataName[j]] = resFloat;
                                break;
                            case "double":
                                double resDouble = 0;
                                if (!string.IsNullOrEmpty(loData))
                                {
                                    resDouble = double.Parse(loData);
                                }
                                jsonData[dataName[j]] = resDouble;
                                break;
                            case "bool":
                                bool resBool = false;
                                if (!string.IsNullOrEmpty(loData))
                                {
                                    resBool = bool.Parse(loData);
                                }
                                jsonData[dataName[j]] = resBool;
                                break;
                            default:
                                jsonData[dataName[j]] = loData;
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        Debug.LogError(e);
                        return;
                    }
                }
                jsonDatas.Add(jsonData);
            }
            string json = jsonDatas.ToJson();
            //防止中文乱码
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            StreamWriter writer = new StreamWriter(JsonPath, false, Encoding.GetEncoding("UTF-8"));
            writer.WriteLine(reg.Replace(json, delegate (Match m)
            {
                return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
            }));
            writer.Flush();
            writer.Close();
            //System.Diagnostics.Process.Start("explorer.exe", JsonPath.Replace("/", "\\"));
        }
        #endregion

        //#region 创建C#代码
        //void CreatCSharp(string name)
        //{
        //    CSharpPath = UnityEngine.Application.dataPath + "/Resources/ExcelData/CSharpPath/" + name + ".cs";
        //    if (File.Exists(CSharpPath)) File.Delete(CSharpPath);

        //    //CodeTypeDeclaration 代码类型声明类
        //    CodeTypeDeclaration CSharpClass = new CodeTypeDeclaration(name);
        //    CSharpClass.IsClass = true;
        //    CSharpClass.TypeAttributes = TypeAttributes.Public;

        //    //设置成员的自定义属性
        //    //CodeAttributeDeclaration代码属性声明
        //    //CodeTypeReference代码类型引用类
        //    //System.Serializable 给脚本打上[System.Serializable()]标签，将 成员变量 在Inspector中显示
        //    CSharpClass.CustomAttributes.Add(new CodeAttributeDeclaration(new CodeTypeReference("System.Serializable")));

        //    for (int i = 0; i < dataName.Count; i++)
        //    {
        //        // 创建字段
        //        //CodeMemberField 代码成员字段类 => (Type, string name)
        //        CodeMemberField member = new CodeMemberField(GetTypeForExcel(dataType[i]), dataName[i] + ";//" + dataDoc[i]);
        //        member.Attributes = MemberAttributes.Public;
        //        CSharpClass.Members.Add(member);
        //    }

        //    // 获取C#语言的实例
        //    CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
        //    //代码生成器选项类
        //    CodeGeneratorOptions options = new CodeGeneratorOptions();
        //    //设置支撑的样式
        //    options.BracingStyle = "C";
        //    //在成员之间插入空行
        //    options.BlankLinesBetweenMembers = true;
        //    StreamWriter writer = new StreamWriter(CSharpPath, false, Encoding.GetEncoding("UTF-8"));
        //    //生成最终代码
        //    provider.GenerateCodeFromType(CSharpClass, writer, options);
        //    writer.Flush();
        //    writer.Close();
        //    //System.Diagnostics.Process.Start("explorer.exe", CSharpPath.Replace("/", "\\"));
        //}

        StringBuilder str = new StringBuilder();
        void CreatCSharpString(string name)
        {
            string fangFaBase = @"
namespace ZXKFramework
{
    public class $
    {
        *
    }
}
";
            if (!string.IsNullOrEmpty(nameSpace))
            {
                fangFaBase = fangFaBase.Replace("ZXKFramework", nameSpace);
            }
            fangFaBase = fangFaBase.Replace("$", name);
            str.Length = 0;
            for (int i = 0; i < dataName.Count; i++)
            {
                str.Append("//" + dataDoc[i]);
                str.Append("\n\r");
                str.Append("public " + dataType[i] + " " + dataName[i] + ";");
                str.Append("\n\r");
            }
            fangFaBase = fangFaBase.Replace("*", str.ToString());
            string loPath = Application.dataPath + "/Resources/ExcelData/CSharpPath/" + name + ".cs";
            if (File.Exists(loPath)) File.Delete(loPath);
            File.WriteAllText(loPath, fangFaBase, Encoding.UTF8);
        }

        Type GetTypeForExcel(string Type)
        {
            if (Type == "int")
                return typeof(Int32);
            if (Type == "float")
                return typeof(Single);  //float关键字是System.Single的别名
            if (Type == "double")
                return typeof(Double);
            if (Type == "bool")
                return typeof(Boolean);
            return typeof(String);
        }

        public static string classPath = "_Scripts/ExcelData.cs";
        private static string classTemp =
    @"
using System.Collections.Generic;
using UnityEngine;
namespace ZXKFramework 
{                                                 
    public class ExcelData
    {
        //@ShuXing
        public void Init()
        {  
            //@FuZhi
        }
        //@FangFa
    }
}
";
        public void CreatCSharpExcelData()
        {
            StringBuilder sx = new StringBuilder();
            StringBuilder fz = new StringBuilder();
            StringBuilder ff = new StringBuilder();

            for (int i = 0; i < allDataClassName.Count; i++)
            {
                string dataName = allDataClassName[i];
                sx.AppendLine($"public List<{dataName}> all{dataName}  = null;");
                fz.AppendLine($"all{dataName} = ExcelDataTools.GetDataList<{dataName}>();");
                 
                string fangFaBase = @"
public # Get#(int id)
{
    for (int i = 0; i < all#.Count; i++)
    {
        if (all#[i].id == id) 
        {
            return all#[i];
        }
    }
    Debug.LogError(id);
    return null;
}";
                fangFaBase = fangFaBase.Replace("#", dataName);
                ff.AppendLine(fangFaBase);
            };

            ff.AppendLine(allFindUnc.ToString());
            allFindUnc.Length = 0;

            string codeClass = classTemp.Replace("//@ShuXing", sx.ToString());
            codeClass = codeClass.Replace("//@FuZhi", fz.ToString());
            codeClass = codeClass.Replace("//@FangFa", ff.ToString());
            if (!string.IsNullOrEmpty(nameSpace))
            {
                codeClass = codeClass.Replace("ZXKFramework", nameSpace);
            }
            string path = Path.Combine(Application.dataPath, classPath);
            File.WriteAllText(path, codeClass, Encoding.UTF8);
            Debug.Log($"ExcelData生成完毕");
        }

    }
}