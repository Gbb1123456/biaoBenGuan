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

    public override void Init(IUIManager uictrl)
    {
        base.Init(uictrl);

        exit_Btn = transform.FindFirst<Button>("exit_Btn");

        maxScreen_Btn = transform.FindFirst<Button>("maxScreen_Btn");
    }


}
