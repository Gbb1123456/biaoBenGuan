using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

public class HelpWnd : UIBase
{
    public override string GroupName => "HelpWnd";

    public override string Name => "HelpWnd";

    Button yes_Btn;

    public override void Init(IUIManager uictrl)
    {
        base.Init(uictrl);

        yes_Btn = transform.FindFirst<Button>("yes_Btn");
        yes_Btn.onClick.AddListener(() =>
        {
            Game.Instance.uiManager.CloseUI<HelpWnd>();
        });
    }

}
