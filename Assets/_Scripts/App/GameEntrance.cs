using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

public class GameEntrance : MonoBehaviour
{
    GameModel gameModel;
    // Start is called before the first frame update
    void Start()
    {
        MVC.RegisterModel(new GameModel());
        Game.Instance.uiManager.ShowUI<StartWnd>();
        gameModel = MVC.GetModel<GameModel>();
        gameModel.Init();
        //Debug.Log(gameModel.excel.GetLanguageDataid(1).Chinese);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
