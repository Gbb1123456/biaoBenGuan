using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ZXKFramework
{
    public class TextItem
    {
        StreamWriter writer;
        private string path;

        public void SetTextPath(string path)
        {
            this.path = path;
        }

        public void SetTextPathAndClean(string path)
        {
            SetTextPath(path);
            Clean();
        }

        public void Clean()
        {
            if (path.IsNotNull()) return;
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
                file.Refresh();
            }
        }

        public void Write(string message)
        {
            Write(path, message);
        }

        //新建文本并且无限添加内容
        void Write(string path, string message)
        {
            string dirPath = Path.GetDirectoryName(path);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            FileInfo file = new FileInfo(path);
            if (!file.Exists)
                writer = file.CreateText();
            else
                writer = file.AppendText();
            writer.WriteLine(message);
            writer.Flush();
            writer.Dispose();
            writer.Close();
        }

        //读取所有内容
        public string Read()
        {
            if (path.IsNotNull()) return "";
            return File.ReadAllText(path, Encoding.UTF8);
        }

        //得到每行字符串
        public string[] ReadAllLines()
        {
            if (path.IsNotNull()) return null;
            return File.ReadAllLines(path);
        }
    }
}