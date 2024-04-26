using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

public class JieShaoWnd : UIBase
{
    public override string GroupName => "JieShaoWnd";

    public override string Name => "JieShaoWnd";

    Text topTxt;
    Image tuPian_Img;
    Text txt;
    Button exit_Btn;

    public override void Init(IUIManager uictrl)
    {
        base.Init(uictrl);
        topTxt = transform.FindFirst<Text>("topTxt");
        tuPian_Img = transform.FindFirst<Image>("tuPian_Img");
        txt = transform.FindFirst<Text>("txt");
        exit_Btn = transform.FindFirst<Button>("exit_Btn");

        exit_Btn.onClick.AddListener(() =>
        {
            GameManager.Instance.transform.FindFirst("PlayerControllerFPS").GetComponent<FirstPersonController>().enabled = true;
            Game.Instance.uiManager.CloseUI<JieShaoWnd>();
            //Game.Instance.sound.StopBGM();
        });
    }

    public override void ShowData(params object[] obj)
    {
        base.ShowData(obj);

        topTxt.text = obj[0].ToString();
        tuPian_Img.sprite = obj[1] as Sprite;
        txt.text = obj[2].ToString();
    }

    public override void Show()
    {
        base.Show();
    }
}
