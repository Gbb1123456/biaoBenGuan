using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

public class GameManager : MonoSingleton<GameManager>
{
    GameModel gameModel;
    // Start is called before the first frame update
    void Start()
    {
        gameModel = MVC.GetModel<GameModel>();
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ShowJieShaoWnd(GameObject go)
    {
        MainData main = gameModel.excel.GetMainDataName(go.name);
        Sprite sp = Resources.Load<Sprite>(main.SpritePath);
        if (string.IsNullOrEmpty(main.TopTxt) || string.IsNullOrEmpty(main.Txt) || sp == null)
        {
            Debug.Log("上面三个判断有空，无法赋值，检查表中名字为：" + go.name);
        }
        else
        {
            Game.Instance.uiManager.ShowUI<JieShaoWnd>(null, main.TopTxt, sp, main.Txt);
        }
    }
}
