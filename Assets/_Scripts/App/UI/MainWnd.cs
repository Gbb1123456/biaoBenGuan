using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

public class MainWnd : UIBase
{
    public override string GroupName => "MainWnd";

    public override string Name => "MainWnd";

    Button exit_Btn;
    Button maxScreen_Btn;
    Button help_Btn;
    Button tiJiao_Btn;

    public override void Init(IUIManager uictrl)
    {
        base.Init(uictrl);

        exit_Btn = transform.FindFirst<Button>("exit_Btn");

        maxScreen_Btn = transform.FindFirst<Button>("maxScreen_Btn");

        help_Btn = transform.FindFirst<Button>("help_Btn");

        tiJiao_Btn = transform.FindFirst<Button>("tiJiao_Btn");

        help_Btn.onClick.AddListener(() =>
        {
            Game.Instance.uiManager.ShowUI<HelpWnd>();
        });
        tiJiao_Btn.AddListener(() =>
        {
            Game.Instance.uiManager.ShowUI<ResultWnd>();
        });
    }



}
