using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Networking;

namespace ZXKFramework
{
    public interface IHttp
    {
        IEnumerator Get(string url, Action<string, DownloadHandler> callBack, int timeOut = 3);
        IEnumerator Post(string url, WWWForm form, Action<string, DownloadHandler> callBack, string Header = null, string HeaderValue = null, int timeOut = 3);

        //上传
        IEnumerator Upload(string url, byte[] myData, Action<bool, string> callBack);
        IEnumerator Upload(string url, string data, Action<bool, string> callBack);

        //协程：下载文件
        IEnumerator DownloadFile(string url, string filePath, Action<bool, string> callBack, bool _isStop);
        IEnumerator LoadHotFixAssembly(string filename, string uri);
    }
}
