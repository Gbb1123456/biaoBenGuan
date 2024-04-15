
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

namespace ZXKFramework
{
    public class Http : IHttp
    {
        public IEnumerator Get(string url, Action<string, DownloadHandler> callBack, int timeOut = 3)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                webRequest.timeout = timeOut * 1000;
                yield return webRequest.SendWebRequest();
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    callBack?.Invoke(webRequest.error, null);
                }
                else
                {
                    callBack?.Invoke("Get请求成功 " + url, webRequest.downloadHandler);
                }
            };
        }

        public IEnumerator Post(string url, WWWForm form, Action<string, DownloadHandler> callBack, string Header = null, string HeaderValue = null, int timeOut = 3)
        {
            //请求链接，并将form对象发送到远程服务器
            using (UnityWebRequest webRequest = UnityWebRequest.Post(url, form))
            {
                if (!string.IsNullOrEmpty(Header) && !string.IsNullOrEmpty(HeaderValue))
                {
                    webRequest.SetRequestHeader(Header, HeaderValue);
                }
                webRequest.timeout = timeOut * 1000;
                yield return webRequest.SendWebRequest();
                if (webRequest.isHttpError || webRequest.isNetworkError)
                {
                    callBack?.Invoke(webRequest.error, null);
                }
                else
                {
                    callBack?.Invoke("Post请求成功 " + url, webRequest.downloadHandler);
                }
            };
        }

        //上传
        public IEnumerator Upload(string url, byte[] myData, Action<bool, string> callBack)
        {
            //byte[] myData = System.Text.Encoding.UTF8.GetBytes("Chinar的测试数据");
            using (UnityWebRequest uwr = UnityWebRequest.Put(url, myData))
            {
                yield return uwr.SendWebRequest();
                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    callBack?.Invoke(false, uwr.error);
                }
                else
                {
                    callBack?.Invoke(true, "上传成功 " + url);
                }
            }
        }

        public IEnumerator Upload(string url, string data, Action<bool, string> callBack)
        {
            byte[] myData = System.Text.Encoding.UTF8.GetBytes(data);
            using (UnityWebRequest uwr = UnityWebRequest.Put(url, myData))
            {
                yield return uwr.SendWebRequest();
                if (uwr.isNetworkError || uwr.isHttpError)
                {
                    callBack?.Invoke(false, uwr.error);
                }
                else
                {
                    callBack?.Invoke(true, "上传成功 " + url);
                }
            }
        }

        /// <summary>
        /// 协程：下载文件
        /// </summary>
        /// <param name="url">请求的Web地址</param>
        /// <param name="filePath">文件保存路径</param>
        /// <param name="callBack">下载完成的回调函数</param>
        /// <returns></returns>
        public IEnumerator DownloadFile(string url, string filePath, Action<bool, string> callBack, bool _isStop)
        {
            using (UnityWebRequest huwr = UnityWebRequest.Head(url))
            {
                ; //Head方法可以获取到文件的全部长度
                yield return huwr.SendWebRequest();
                if (huwr.isNetworkError || huwr.isHttpError) //如果出错
                {
                    callBack?.Invoke(false, huwr.error);
                }
                else
                {
                    long totalLength = long.Parse(huwr.GetResponseHeader("Content-Length")); //首先拿到文件的全部长度
                    string dirPath = Path.GetDirectoryName(filePath);
                    if (!Directory.Exists(dirPath)) //判断路径是否存在
                    {
                        Directory.CreateDirectory(dirPath);
                    }
                    //创建一个文件流，指定路径为filePath,模式为打开或创建，访问为写入
                    using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        long nowFileLength = fs.Length; //当前文件长度
                        Debug.Log(fs.Length);
                        if (nowFileLength < totalLength)
                        {
                            Debug.Log("还没下载完成");
                            fs.Seek(nowFileLength, SeekOrigin.Begin);       //从头开始索引，长度为当前文件长度
                            UnityWebRequest uwr = UnityWebRequest.Get(url); //创建UnityWebRequest对象，将Url传入
                            uwr.SetRequestHeader("Range", "bytes=" + nowFileLength + "-" + totalLength);
                            uwr.SendWebRequest();                      //开始请求
                            if (uwr.isNetworkError || uwr.isHttpError) //如果出错
                            {
                                callBack?.Invoke(false, uwr.error);
                            }
                            else
                            {
                                long index = 0;     //从该索引处继续下载
                                while (!uwr.isDone) //只要下载没有完成，一直执行此循环
                                {
                                    if (_isStop) break;
                                    yield return null;
                                    byte[] data = uwr.downloadHandler.data;
                                    if (data != null)
                                    {
                                        long length = data.Length - index;
                                        fs.Write(data, (int)index, (int)length); //写入文件
                                        index += length;
                                        nowFileLength += length;
                                        //ProgressBar.value = (float)nowFileLength / totalLength;
                                        //SliderValue.text = Math.Floor((float)nowFileLength / totalLength * 100) + "%";
                                        if (nowFileLength >= totalLength) //如果下载完成了
                                        {
                                            //ProgressBar.value = 1; //改变Slider的值
                                            //SliderValue.text = 100 + "%";
                                            callBack?.Invoke(true, "下载成功 url:" + url + " filePath:" + filePath);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public IEnumerator LoadHotFixAssembly(string filename, string uri)
        {
            using (UnityWebRequest request = UnityWebRequest.Get(uri))
            {
                yield return request.SendWebRequest();
                CreatFile(Application.persistentDataPath, filename, request.downloadHandler.data);
            }
        }

        private void CreatFile(string savePath, string name, byte[] content)
        {
            string filePath = savePath + "/" + name;
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            Stream sw;
            FileInfo fileInfo = new FileInfo(filePath);
            sw = fileInfo.Create();
            sw.Write(content, 0, content.Length);
            sw.Close();
            sw.Dispose();
            Debug.Log("保存文件路径:" + filePath);
        }
    }
}