using Org.BouncyCastle.Asn1;
using SK.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using WisdomTree.Common.Function;
using ZXKFramework;
using static System.Net.Mime.MediaTypeNames;

public static class StartGame
{
    public static List<string> Objstring=new List<string>();
}

public class ResultWnd : UIBase
{
    Button btn;
    Transform transforms;
    public override string GroupName => "ResultWnd";

    public override string Name => "ResultWnd";

    public override void Init(IUIManager uictrl)
    {
        base.Init(uictrl);
        btn = transform.FindFirst<Button>("Btn");
        transforms = transform.FindFirst<Transform>("Content");
        
    }
    public override void Show()
    {
        StartInit();
        btn.AddListener(() =>
        {
            Result();
            Game.Instance.uiManager.CloseUI<ResultWnd>();
        });
    }
    private void StartInit()
    {
        for (int i = 0; i < transforms.childCount; i++)
        {
            transforms.GetChild(i).Find("Text").GetComponent<UnityEngine.UI.Text>().text = transforms.GetChild(i).name + "已浏览";
            transforms.GetChild(i).Find("Image").SetActive(false);
        }
        Debug.Log(StartGame.Objstring.Count);
        for (int i = 0; i < StartGame.Objstring.Count; i++)
        {
            transforms.Find(StartGame.Objstring[i]).Find("Image").SetActive(true);
        }
    }
    private void Result()
    {
        double zhi = 0;
        zhi = StartGame.Objstring.Count * 12.5f;
        Debug.Log(zhi);
        Communication.Log = true;
        Communication.UploadReport(zhi, "实验总结内容", url => Communication.OpenWebReport(url),
            new WisdomTree.Common.Function.Model("浏览结果", new Content("浏览结果：", StringTxt())));
    }
    private string StringTxt()
    {
        string txt = "";
        for (int i = 0; i < StartGame.Objstring.Count; i++)
        {
            txt += StartGame.Objstring[i] + "已浏览\n";
        }
        return txt;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
