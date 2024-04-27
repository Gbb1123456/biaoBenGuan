using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

public class StartWnd : UIBase
{
    public override string GroupName => "StartWnd";

    public override string Name => "StartWnd";

    Button start_Btn;

    public override void Init(IUIManager uictrl)
    {
        base.Init(uictrl);

        start_Btn = transform.FindFirst<Button>("start_Btn");

        start_Btn.onClick.AddListener(() =>
        {
            Game.Instance.uiManager.CloseUI<StartWnd>();
            
            Game.Instance.uiManager.ShowUI<HelpWnd>();
            Game.Instance.uiManager.ShowUI<MainWnd>();
            Game.Instance.uiManager.ShowUI<MapWnd>();
        });
    }

    public override void Show()
    {
        base.Show();
    }
}
