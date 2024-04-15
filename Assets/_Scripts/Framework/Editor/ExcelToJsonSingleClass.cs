
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;
//using System.Data;
//using Excel;
//using System.IO;
//using LitJson;
//using System.Text;
//using System.Text.RegularExpressions;
//using System;
//using System.Reflection;
//using System.CodeDom;
//using System.CodeDom.Compiler;
//using ZXKFramework;

//namespace DragonFramework
//{
//    public class ExcelToJsonSingleClass : EditorWindow
//    {
//        private string JsonPath;
//        string CSharpPath;
//        string JsonName;
//        List<string> dataDoc = new List<string>();
//        List<string> dataType = new List<string>();
//        List<string> dataName = new List<string>();
//        List<string[]> ExcelDateList = new List<string[]>();
//        List<string> allDataClassName = new List<string>();//所有类名
//        StringBuilder allFindUnc = new StringBuilder();

//        [UnityEditor.MenuItem("ZXKFramework/ExcelToJsonSingleClass")]
//        static void ExceltoJson()
//        {
//            ExcelToJsonSingleClass toJson = (ExcelToJsonSingleClass)EditorWindow.GetWindow(typeof(ExcelToJsonSingleClass), true, "ExcelToJsonSingleClass");
//            toJson.Show();
//        }

//        private void OnGUI()
//        {
//            if (GUILayout.Button("选择需要转换的excel文件"))
//            {
//                GetAllExcelPath();
//            }
//        }

//        void GetAllExcelPath()
//        {
//            CreateBasePath();
//            string loPath = Application.dataPath + "/Resources/ExcelData/Excel";
//            DirectoryInfo dir = new DirectoryInfo(loPath);
//            FileInfo[] fil = dir.GetFiles();
//            allDataClassName.Clear();
//            foreach (FileInfo f in fil)
//            {
//                if (f.FullName.EndsWith(".meta"))
//                {
//                    continue;
//                }
//                //循环读表
//                ReadExcel(f.FullName.Replace("\\", "/"), Path.GetFileNameWithoutExtension(f.Name));
//            }
//            //生成CS数据
//            CreatCSharpExcelData();
//            AssetDatabase.Refresh();
//            Debug.Log("生成成功");
//        }

//        //创建基础文件夹
//        void CreateBasePath()
//        {
//            CreateDirectory(Application.dataPath + "/Resources/ExcelData/Excel");
//            CreateDirectory(Application.dataPath + "/Resources/ExcelData/CSharpPath");
//            CreateDirectory(Application.dataPath + "/Resources/ExcelData/ExcelToJson");
//        }

//        /// <summary>
//        /// 读取Excel
//        /// </summary>
//        /// <param name="path">excel路径</param>
//        /// <param name="columnNum">列</param>
//        /// <param name="rowNum">行</param>
//        void ReadExcel(string path, string fileName)
//        {
//            FileStream stream = File.Open(path, FileMode.Open, FileAccess.Read, FileShare.Read);
//            IExcelDataReader excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
//            DataSet data = excelReader.AsDataSet();

//            dataDoc.Clear();
//            dataName.Clear();
//            dataType.Clear();
//            ExcelDateList.Clear();

//            //读取Excel的所有页签
//            for (int i = 0; i < data.Tables.Count; i++)
//            {
//                //i代表每一页，一般只有一页，下面为每一页的每一行 每一列
//                DataRowCollection dataRow = data.Tables[i].Rows;            // 每行
//                DataColumnCollection dataColumn = data.Tables[i].Columns;   // 每列

//                string tableName = fileName;
//                JsonName = fileName + ".json";
//                JsonPath = UnityEngine.Application.dataPath + "/Resources/ExcelData/ExcelToJson/" + JsonName;

//                //循环，第一行
//                for (int rowNum = 0; rowNum < data.Tables[i].Rows.Count; rowNum++)
//                {
//                    //第一行的每一列
//                    string[] table = new string[data.Tables[i].Columns.Count];
//                    for (int columnNum = 0; columnNum < data.Tables[i].Columns.Count; columnNum++)
//                    {
//                        if (rowNum == 0)
//                        {
//                            //第一行：解释说明
//                        }
//                        else if (rowNum == 1)
//                        {
//                            dataDoc.Add(data.Tables[i].Rows[rowNum][columnNum].ToString());
//                        }
//                        else if (rowNum == 2)
//                        {
//                            dataType.Add(data.Tables[i].Rows[rowNum][columnNum].ToString());
//                        }
//                        else if (rowNum == 3)
//                        {
//                            dataName.Add(data.Tables[i].Rows[rowNum][columnNum].ToString());
//                        }
//                        else
//                        {
//                            //Debug.Log(data.Tables[i].Rows[rowNum][columnNum].ToString() + "\n");
//                            table[columnNum] = data.Tables[i].Rows[rowNum][columnNum].ToString();
//                        }
//                    }
//                    if (rowNum > 3)
//                    {
//                        //将一行数据存入list
//                        ExcelDateList.Add(table);
//                    }
//                }

//                //每个表循环一次
//                CreatJsonFile();//创建Json数据
//                CreatCSharp(tableName);//创建基础类
//                allDataClassName.Add(tableName);//保存所有类名
//                CreateBaseCSharpCtrl(tableName);//数据类管理
//            }
//        }

    
//        void CreateBaseCSharpCtrl(string tableName)
//        {      
//            StringBuilder sx = new StringBuilder();
//            StringBuilder fz = new StringBuilder();
//            StringBuilder str = new StringBuilder();

//            string baseClass = @"
//public class #ExcelData
//{
//    //@ShuXing
//    public void Init()
//    {  
//        //@FuZhi
//    }
//    //@FangFa
//}
//";
//            sx.AppendLine($"public List<{tableName}> all{tableName}  = null;");
//            fz.AppendLine($"all{tableName} = ExcelDataTools.GetDataList<{tableName}>();");

//            for (int j = 0; j < dataName.Count; j++)
//            {
                
//                string fangFaBase = @"
//public # Get#*(% *)
//{
//    for (int i = 0; i < all#.Count; i++)
//    {
//        if (all#[i].* == *) 
//        {
//            return all#[i];
//        }
//    }
//    Debug.LogError(*);
//    return null;
//}

//public List<%> GetList#*()
//{
//    List<%> res = new List<%>();
//    for (int i = 0; i < all#.Count; i++)
//    {
//        if (!res.Contains(all#[i].*)) 
//        {
//            res.Add(all#[i].*);
//        }
//    }
//    return res;
//}

//";
//                fangFaBase = fangFaBase.Replace("#", tableName);
//                fangFaBase = fangFaBase.Replace("*", dataName[j]);
//                fangFaBase = fangFaBase.Replace("%", dataType[j]);
//                str.Append(fangFaBase);
//            }
//            baseClass = baseClass.Replace("#", tableName);
//            baseClass = baseClass.Replace("//@ShuXing", sx.ToString());
//            baseClass = baseClass.Replace("//@FuZhi", fz.ToString());
//            baseClass = baseClass.Replace("//@FangFa", str.ToString());
//            allFindUnc.AppendLine(baseClass);
//        }

//        void CreatJsonFile()
//        {
//            if (File.Exists(JsonPath)) File.Delete(JsonPath);//刷新
//            JsonData jsonDatas = new JsonData();
//            jsonDatas.SetJsonType(JsonType.Array);
//            for (int i = 0; i < ExcelDateList.Count; i++)
//            {
//                JsonData jsonData = new JsonData();
//                for (int j = 0; j < dataName.Count; j++)
//                {
//                    string tempData = dataType[j].Trim();
//                    string loData = ExcelDateList[i][j].Trim();
//                    try
//                    {
//                        switch (tempData)
//                        {
//                            case "int":
//                                int resInt = 0;
//                                if (!string.IsNullOrEmpty(loData))
//                                {
//                                    resInt = Int32.Parse(loData);
//                                }
//                                jsonData[dataName[j]] = resInt;
//                                break;
//                            case "float":
//                                float resFloat = 0;
//                                if (!string.IsNullOrEmpty(loData))
//                                {
//                                    resFloat = float.Parse(loData);
//                                }
//                                jsonData[dataName[j]] = resFloat;
//                                break;
//                            case "double":
//                                double resDouble = 0;
//                                if (!string.IsNullOrEmpty(loData))
//                                {
//                                    resDouble = double.Parse(loData);
//                                }
//                                jsonData[dataName[j]] = resDouble;
//                                break;
//                            case "bool":
//                                bool resBool = false;
//                                if (!string.IsNullOrEmpty(loData))
//                                {
//                                    resBool = bool.Parse(loData);
//                                }
//                                jsonData[dataName[j]] = resBool;
//                                break;
//                            default:
//                                jsonData[dataName[j]] = loData;
//                                break;
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        Debug.LogError(e);
//                        return;
//                    }
//                }
//                jsonDatas.Add(jsonData);
//            }
//            string json = jsonDatas.ToJson();
//            //防止中文乱码
//            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
//            StreamWriter writer = new StreamWriter(JsonPath, false, Encoding.GetEncoding("UTF-8"));
//            writer.WriteLine(reg.Replace(json, delegate (Match m)
//            {
//                return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
//            }));
//            writer.Flush();
//            writer.Close();
//        }

//        void CreatCSharp(string name)
//        {
//            CSharpPath = UnityEngine.Application.dataPath + "/Resources/ExcelData/CSharpPath/" + name + ".cs";
//            if (File.Exists(CSharpPath)) File.Delete(CSharpPath);

//            CodeTypeDeclaration CSharpClass = new CodeTypeDeclaration(name);
//            CSharpClass.IsClass = true;
//            CSharpClass.TypeAttributes = TypeAttributes.Public;

//            for (int i = 0; i < dataName.Count; i++)
//            {
//                CodeMemberField member = new CodeMemberField(GetTypeForExcel(dataType[i]), dataName[i] + ";//" + dataDoc[i]);
//                member.Attributes = MemberAttributes.Public;
//                CSharpClass.Members.Add(member);
//            }

//            // 获取C#语言的实例
//            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
//            //代码生成器选项类
//            CodeGeneratorOptions options = new CodeGeneratorOptions();
//            //设置支撑的样式
//            options.BracingStyle = "C";
//            //在成员之间插入空行
//            options.BlankLinesBetweenMembers = true;
//            StreamWriter writer = new StreamWriter(CSharpPath, false, Encoding.GetEncoding("UTF-8"));
//            //生成最终代码
//            provider.GenerateCodeFromType(CSharpClass, writer, options);
//            writer.Flush();
//            writer.Close();
//        }

//        Type GetTypeForExcel(string Type)
//        {
//            if (Type == "int")
//                return typeof(Int32);
//            if (Type == "float")
//                return typeof(Single);  //float关键字是System.Single的别名
//            if (Type == "double")
//                return typeof(Double);
//            if (Type == "bool")
//                return typeof(Boolean);
//            return typeof(String);
//        }

//        public static string classPath = "_Scripts/ExcelDataSingleClass.cs";
//        private static string classTemp =
//    @"
//using System.Collections.Generic;
//using UnityEngine;
//public class ExcelDataSingleClass
//{
//    //@ShuXing
//    public void Init()
//    {  
//        //@FuZhi
//    } 
//}
////@FangFa
//";

//        public void CreatCSharpExcelData()
//        {
//            StringBuilder sx = new StringBuilder();
//            StringBuilder fz = new StringBuilder();
//            StringBuilder ff = new StringBuilder();

//            for (int i = 0; i < allDataClassName.Count; i++)
//            {
//                string dataName = allDataClassName[i];
//                sx.AppendLine($"public List<{dataName}> all{dataName}  = null;");
//                fz.AppendLine($"all{dataName} = ExcelDataTools.GetDataList<{dataName}>();");

////                string fangFaBase = @"
////public # Get#(int id)
////{
////    for (int i = 0; i < all#.Count; i++)
////    {
////        if (all#[i].id == id) 
////        {
////            return all#[i];
////        }
////    }
////    Debug.LogError(id);
////    return null;
////}";
////                fangFaBase = fangFaBase.Replace("#", dataName);
////                ff.AppendLine(fangFaBase);
//            };

//            ff.AppendLine(allFindUnc.ToString());
//            allFindUnc.Length = 0;

//            string codeClass = classTemp.Replace("//@ShuXing", sx.ToString());
//            codeClass = codeClass.Replace("//@FuZhi", fz.ToString());
//            codeClass = codeClass.Replace("//@FangFa", ff.ToString());

//            string path = Path.Combine(Application.dataPath, classPath);
//            File.WriteAllText(path, codeClass, Encoding.UTF8);
//            Debug.Log($"ExcelData生成完毕");
//        }

//        //创建文件夹
//        public void CreateDirectory(string destFileName)
//        {
//            if (!Directory.Exists(destFileName))
//                Directory.CreateDirectory(destFileName);
//        }
//    }
//}