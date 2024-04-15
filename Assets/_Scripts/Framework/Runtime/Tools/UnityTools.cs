
using UnityEngine;
using LitJson;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Debug = UnityEngine.Debug;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using UnityEngine.EventSystems;

namespace ZXKFramework
{
    public class UnityTools : MonoBehaviour
    {
        public static Sprite ToSprite(Texture2D self)
        {
            var rect = new Rect(0, 0, self.width, self.height);
            var pivot = Vector2.one * 0.5f;
            var newSprite = Sprite.Create(self, rect, pivot);
            return newSprite;
        }

        public static string GetJson(object self)
        {
            string loRes = JsonMapper.ToJson(self);
            return StringToUTF8(loRes);
        }

        public static string StringToUTF8(string self)
        {
            Regex reg = new Regex(@"(?i)\\[uU]([0-9a-f]{4})");
            self = reg.Replace(self, delegate (Match m)
            {
                return ((char)Convert.ToInt32(m.Groups[1].Value, 16)).ToString();
            });
            return self;
        }

        /// <summary>
        /// 存入字典更新位置角度
        /// </summary>
        /// <param name="DicPosAndRotation">存入的字典</param>
        /// <param name="key">实例化出来的模型名称</param>
        /// <param name="vectorPos">位置</param>
        /// <param name="vectorRotation">角度</param>
        /// <param name="obj"></param>
        public static void AddPosAndRotation(Dictionary<string, List<Vector3>> DicPosAndRotation, string key, Vector3 vectorPos, Vector3 vectorRotation, GameObject gameObj)
        {
            List<Vector3> listVec = new List<Vector3>();
            listVec.Add(vectorPos);
            listVec.Add(vectorRotation);
            if (!DicPosAndRotation.ContainsKey(key))
            {
                DicPosAndRotation.Add(key, listVec);
            }
            else
            {
                if (gameObj != null)
                {
                    gameObj.transform.localPosition = DicPosAndRotation[key][0];
                    gameObj.transform.localEulerAngles = DicPosAndRotation[key][1];
                }
            }
        }

        //复制目录文件到指定目录
        public static void CopyDirIntoDestDirectory(string sourceFileName, string destFileName, bool overwrite)
        {
            if (!Directory.Exists(destFileName))
                Directory.CreateDirectory(destFileName);
            foreach (var file in Directory.GetFiles(sourceFileName))
                File.Copy(file, Path.Combine(destFileName, Path.GetFileName(file)), overwrite);
            foreach (var d in Directory.GetDirectories(sourceFileName))
                CopyDirIntoDestDirectory(d, Path.Combine(destFileName, Path.GetFileName(d)), overwrite);
        }

        //创建文件夹
        public static void CreateDirectory(string destFileName)
        {
            if (!Directory.Exists(destFileName))
                Directory.CreateDirectory(destFileName);
        }

        //获取所有文件名字
        public static List<string> GetFile(string path, List<string> FileList)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            FileInfo[] fil = dir.GetFiles();
            DirectoryInfo[] dii = dir.GetDirectories();
            foreach (FileInfo f in fil)
            {
                long size = f.Length;
                FileList.Add(Path.GetFileNameWithoutExtension(f.Name));
            }
            foreach (DirectoryInfo d in dii)
            {
                GetFile(d.Name, FileList);
            }
            return FileList;
        }

        //获取指定路径下面的所有资源文件  然后进行删除
        public static bool DeleteAllFile(string fullPath)
        {
            if (Directory.Exists(fullPath))
            {
                DirectoryInfo direction = new DirectoryInfo(fullPath);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    string FilePath = fullPath + "/" + files[i].Name;
                    File.Delete(FilePath);
                }
                return true;
            }
            return false;
        }

        //删除文件
        public static void DeleteFile(string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        //创建文件
        public static void CreateFile(string path)
        {
            if (!File.Exists(path))
            {
                File.Create(path);
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
            Debug.Log(p.StartInfo.Arguments);
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardError = true;
            p.StartInfo.CreateNoWindow = true;
            p.Start();
            p.WaitForExit();
            p.Close();
        }

        public void FullSceneSet()
        {
            Screen.fullScreen = !Screen.fullScreen;
            if (Screen.fullScreen) Screen.SetResolution(1920, 1080, false);
            else Screen.SetResolution(1280, 720, false);
        }
        /// <summary>
        /// 获取一个随机打乱顺序的数组
        /// </summary>
        /// <param name="rangNum">取出的数组里面要有多少个数</param>
        /// <param name="endNum">随机数组的范围(最后的一个数)</param>
        /// <param name="startNum">随机数组的范围(第一个数)</param>
        public static int[] GetRandomSequence(int rangNum, int endNum, int startNum = 0)
        {
            try
            {
                //随机总数组
                int[] sequence = new int[endNum - startNum + 1];
                //取到的不重复数字的数组长度
                int[] output = new int[rangNum];
                for (int i = startNum; i < endNum + 1; i++)
                {
                    sequence[i - startNum] = i;
                }
                for (int i = 0; i < rangNum; i++)
                {
                    //随机一个数，每随机一次，随机区间-1
                    int Num = UnityEngine.Random.Range(startNum, endNum + 1);
                    output[i] = sequence[Num - startNum];

                    //将区间最后一个数赋值到取到数上
                    sequence[Num - startNum] = sequence[endNum - startNum];

                    endNum--;
                    //执行一次效果如：1，2，3，4，5 取到2
                    //则下次随机区间变为1,5,3,4;          
                }
                return output;
            }
            catch (Exception e)
            {
                Debug.LogError($"GetRandomSequence is Error:{e.ToString()}");
                return null;
            }
        }
        /// <summary>
        /// 获取时间戳13位
        /// </summary>
        public static string GetTimeStamp()
        {
            return DateTimeOffset.Now.ToUnixTimeSeconds() + "000";
        }
        /// <summary>
        /// 两个时间戳相差多少秒
        /// </summary>
        public static int TimeStampDiffSeconds(string t1, string t2)
        {
            return Mathf.Abs(Convert.ToInt32((ConvertToTime(t1) - ConvertToTime(t2)).TotalSeconds));
        }
        /// <summary>
        /// 时间戳转换成标准时间
        /// </summary>
        public static DateTime ConvertToTime(string timeStamp)
        {
            string ID = TimeZoneInfo.Local.Id;
            DateTime start = new DateTime(1970, 1, 1) + TimeZoneInfo.Local.GetUtcOffset(DateTime.Now);
            DateTime dtStart = TimeZoneInfo.ConvertTime(start, TimeZoneInfo.FindSystemTimeZoneById(ID));
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            DateTime time = dtStart.Add(toNow);
            return time;
        }
        /// <summary>
        /// JSON字符串格式化
        /// </summary>
        public static string JsonTree(string json)
        {
            int level = 0;
            var jsonArr = json.ToArray();  // Using System.Linq;
            string jsonTree = string.Empty;
            for (int i = 0; i < json.Length; i++)
            {
                char c = jsonArr[i];
                if (level > 0 && '\n' == jsonTree.ToArray()[jsonTree.Length - 1])
                {
                    jsonTree += TreeLevel(level);
                }
                switch (c)
                {
                    case '[':
                        jsonTree += c + "\n";
                        level++;
                        break;
                    case ',':
                        jsonTree += c + "\n";
                        break;
                    case ']':
                        jsonTree += "\n";
                        level--;
                        jsonTree += TreeLevel(level);
                        jsonTree += c;
                        break;
                    default:
                        jsonTree += c;
                        break;
                }
            }
            return jsonTree;
        }
        /// <summary>
        /// 树等级
        /// </summary>
        private static string TreeLevel(int level)
        {
            string leaf = string.Empty;
            for (int t = 0; t < level; t++)
            {
                leaf += "\t";
            }
            return leaf;
        }
        /// <summary>
        /// 解析URL中的参数
        /// </summary>
        public static string GetURLParameters(string url)
        {
            if (string.IsNullOrEmpty(url)) return null;
            var paramStr = url.Split('?')[1];
            var paramList = paramStr.Split('&');
            if (paramList == null || paramList.Count() <= 0) return null;
            return paramList.ToDictionary(e => Regex.Split(e, "=")[0], e => Regex.Split(e, "=")[1])["ticket"];
        }
        /// <summary>
        /// MD5 32位 大写加密
        /// </summary>
        public static string MD5Encrypt32Big(string encryptContent)
        {
            string content = encryptContent;
            //创建一个MD5CryptoServiceProvider对象的新实例。
            MD5 md5 = MD5.Create();
            //将输入的字符串转换为一个字节数组并计算哈希值。
            byte[] data = md5.ComputeHash(Encoding.UTF8.GetBytes(content));
            //创建一个StringBuilder对象，用来收集字节数组中的每一个字节，然后创建一个字符串。
            StringBuilder sb = new StringBuilder();
            //遍历字节数组，将每一个字节转换为十六进制字符串后，追加到StringBuilder实例的结尾
            for (int i = 0; i < data.Length; i++)
            {
                sb.Append(data[i].ToString("X2"));
            }
            //返回一个十六进制字符串
            content = sb.ToString();
            if (content.Length < 32)
                Debug.LogError("32位 加密错误！！长度不对");
            return content;
        }

        public static string RemoveDoubleQuotationMarks(string str)
        {
            char[] temp = str.ToCharArray();
            string result = string.Empty;
            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i] != '"')
                {
                    result += temp[i];
                }
            }
            return result.Trim();
        }

        //遍历地址中所有文件
        public static void GetAllFile(string path, Action<FileInfo> callBack)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo direction = new DirectoryInfo(path);
                FileInfo[] files = direction.GetFiles("*", SearchOption.AllDirectories);
                for (int i = 0; i < files.Length; i++)
                {
                    UnityEngine.Debug.Log("-----" + files[i].Name);
                    callBack(files[i]);
                }

                //DirectoryInfo TheFolder = new DirectoryInfo(path);
                ////遍历文件
                //foreach (FileInfo NextFile in TheFolder.GetFiles())
                //{

                //}
                ////遍历文件夹
                //foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                //{

                //}
            }
        }

        //判断文件夹中是否存在文件
        public static bool HasFile(string path, string data)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                //遍历文件
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    if (NextFile.Name.ToLower() == data)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //遍历文件夹
        public static void FindMenu(string path, Action<DirectoryInfo> callBack)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    callBack?.Invoke(NextFolder);
                }
            }
        }

        private static string mbasePath = "";
        /// <summary>
        /// 所有的资源按照100个一个文件夹分组
        /// </summary>
        private static int index = 0;
        public static void AssetFenZu(string path)
        {
            if (Directory.Exists(path))
            {
                index = 0;
                string dic = path + "/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                if (!Directory.Exists(dic))
                {
                    Directory.CreateDirectory(dic);
                }
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                //遍历文件夹,得到所有文件
                foreach (FileInfo NextFolder in TheFolder.GetFiles())
                {
                    if (index == 100)
                    {
                        dic = path + "/" + DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss-fff");
                        if (!Directory.Exists(dic))
                        {
                            Directory.CreateDirectory(dic);
                        }
                        index = 0;
                    }
                    else
                    {
                        WDebug.Log(dic + "/" + NextFolder.Name);
                        NextFolder.MoveTo(dic + "/" + NextFolder.Name);
                        index++;
                    }
                }
            }
        }

        //移动所有文件到一个文件夹下
        public static void MoveAllFileToDic(string path)
        {
            mbasePath = path;
            MoveAllFileToDicFunc(path);
        }

        static void MoveAllFileToDicFunc(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo TheFolder = new DirectoryInfo(path);
                //遍历文件夹
                foreach (DirectoryInfo NextFolder in TheFolder.GetDirectories())
                {
                    MoveAllFileToDicFunc(NextFolder.FullName);
                }
                foreach (FileInfo NextFile in TheFolder.GetFiles())
                {
                    string temp = mbasePath + @"\" + NextFile.Name;
                    if (Path.GetDirectoryName(NextFile.FullName) != mbasePath)
                    {
                        try
                        {
                            //NextFile.MoveTo(ChangeName(temp));
                        }
                        catch (Exception e)
                        {
                            WDebug.LogError("无法移动 : " + temp);
                        }
                    }
                    else
                    {
                        Debug.Log("根目录下文件不移动" + NextFile.Name);
                    }
                }
            }
        }

        //新建文件夹，移动文件
        public static void CreateDicMoveFile(string path)
        {
            int count = 0;
            DirectoryInfo TheFolder = new DirectoryInfo(path);
            //遍历文件
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                count++;
            }
            if (count == 0) return;
            string res = "文件夹" + path + "下有：" + count + "文件想新建文件夹并且移动，是否继续！";
            foreach (FileInfo NextFile in TheFolder.GetFiles())
            {
                string tempDic = Path.GetDirectoryName(NextFile.FullName) + @"\" + Path.GetFileNameWithoutExtension(NextFile.FullName);
                try
                {
                    if (!Directory.Exists(tempDic))
                    {
                        Directory.CreateDirectory(tempDic);
                    }
                    //NextFile.MoveTo(ChangeName(tempDic + @"\" + NextFile.Name));
                }
                catch (Exception e)
                {
                    Debug.Log(tempDic);
                }
            }
        }

        //去掉所有特殊字符，得到字符串
        public static string ziFu = "[]【】 .()n";
        public static string GetNoTeShu(string data)
        {
            bool lobo = true;
            string result = "";
            char[] allZifu = ziFu.ToCharArray();
            foreach (char res in data.ToCharArray())
            {
                lobo = true;
                foreach (var item in allZifu)
                {
                    if (res == item)
                    {
                        lobo = false;
                        break;
                    }
                }
                if (lobo)
                {
                    result += res;
                }
            }
            return result;
        }

        //英文星期转化为中文星期
        static string[] weekdaysCn = { "星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六" };

        public static string WeekToCn(int data)
        {
            if (data >= 0 && data <= 6)
            {
                return weekdaysCn[data];
            }
            else
            {
                Debug.LogError("DF_NowTime_V01 WeekToCn 转换为中文星期，内容" + data + "不符合规范");
                return "";
            }
        }

        public static string GetTimeCn()
        {
            DateTime NowTime = DateTime.Now.ToLocalTime();
            int lodata = Int32.Parse(DateTime.Now.DayOfWeek.ToString(("d")));
            return NowTime.ToString("yyyy 年 MM 月 dd 日 HH: mm:ss ") + WeekToCn(lodata);
        }

        static string[] weekdaysEn = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };

        public static string WeekToEn(int data)
        {
            if (data >= 0 && data <= 6)
            {
                return weekdaysEn[data];
            }
            else
            {
                Debug.LogError("DF_NowTime_V01 WeekToEn 转换为英文星期，内容" + data + "不符合规范");
                return "";
            }
        }

        public static string GetTimeEn()
        {
            DateTime NowTime = DateTime.Now.ToLocalTime();
            int lodata = Int32.Parse(DateTime.Now.DayOfWeek.ToString(("d")));
            return NowTime.ToString("yyyy / MM / dd  HH: mm:ss ") + WeekToEn(lodata);
        }

        //路径\替换为/
        public static string PathToNormal(string path)
        {
            string loName = "";
            char[] loPath = path.ToCharArray();
            foreach (char item in loPath)
            {
                if (item == '\\')
                {
                    loName += '/';
                }
                else
                {
                    loName += item;
                }
            }
            return loName;
        }
        //-------------
        public static void PauseTime(Action callBack)
        {
            Time.timeScale = 0;
            callBack?.Invoke();
        }

        public static void PlayTime(Action callBack)
        {
            Time.timeScale = 1;
            callBack?.Invoke();
        }

        /// <summary>
        /// 绑定EventTrigger事件
        /// </summary>
        /// <param name="eventTrigger"></param>
        /// <param name="eventTriggerType"></param>
        /// <param name="unityAction"></param>
        public static void AddEventTriggerFun(EventTrigger eventTrigger, EventTriggerType eventTriggerType, UnityEngine.Events.UnityAction<BaseEventData> unityAction)
        {
            // 定义所要绑定的事件类型
            EventTrigger.Entry entry = new EventTrigger.Entry();
            // 设置事件类型
            entry.eventID = eventTriggerType;
            // 初始化回调函数
            entry.callback = new EventTrigger.TriggerEvent();
            // 定义回调函数
            UnityEngine.Events.UnityAction<BaseEventData> callBack = new UnityEngine.Events.UnityAction<BaseEventData>(unityAction);
            // 绑定回调函数
            entry.callback.AddListener(callBack);
            eventTrigger.triggers.Add(entry);
        }
    }
}

