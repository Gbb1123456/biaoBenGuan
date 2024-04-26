using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

public class GameManager : MonoSingleton<GameManager>
{
    GameModel gameModel;

    int id;
    bool isa = false;

    [SerializeField]
    public List<GameObject> allLookModel;

    [SerializeField]
    public PlayerMovement playerMove;

    [SerializeField]
    public LookWithMouse playerRot;

    // Start is called before the first frame update
    void Start()
    {
        gameModel = MVC.GetModel<GameModel>();

        //GameObject go = transform.FindFirst("ClickGame");

        //for (int i = 0; i < go.transform.childCount; i++)
        //{
        //    SetMouseClickCallBack(go.transform.GetChild(i).name, ShowJieShaoWnd);
        //}
    }

    // Update is called once per frame
    //void Update()
    //{

    //    if (Input.GetKeyDown(KeyCode.A))
    //    {
    //        isa = true;
    //    }

    //    if (isa)
    //    {
    //        id++;
    //        MainData main = gameModel.excel.GetMainDataid(id);
    //        Sprite sp = Resources.Load<Sprite>(main.SpritePath);
    //        if (string.IsNullOrEmpty(main.TopTxt) || string.IsNullOrEmpty(main.Txt) || sp == null)
    //        {
    //            Debug.LogError("上面三个判断有空，无法赋值，检查表中名字为：" + id);
    //        }
    //        else
    //        {
    //            Game.Instance.uiManager.ShowUI<JieShaoWnd>(null, main.TopTxt, sp, main.Txt);
    //        }

    //        isa = false;
    //    }

    //}

    public void SetMouseClickCallBack(string name, Action<GameObject> callBack)
    {
        GameObject go = transform.FindFirst(name);
        if (go.TryGetComponent(out IClick click))
        {
            click.SetCallBack(callBack);
        }
    }

    /// <summary>
    /// 显示介绍界面
    /// </summary>
    /// <param name="go"></param>
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
            if (!string.IsNullOrEmpty(main.SoundPath))
            {
                Game.Instance.sound.PlayBGM(main.SoundPath, false);
                return;
            }
            Game.Instance.uiManager.ShowUI<JieShaoWnd>(null, main.TopTxt, sp, main.Txt);
        }
    }


    public void ShowGameModel(GameObject go)
    {
        playerMove.enabled = false;
        playerRot.enabled = false;
        for (int i = 0; i < allLookModel.Count; i++)
        {
            allLookModel[i].SetActive(false);
        }
        transform.FindFirst("Look_Canvas").SetActive(true);

        transform.FindFirst("Look_Canvas").FindFirst<Text>("biaoBenName_Txt").text = go.name;

        GameObject g = transform.FindFirst("Model").FindFirst(go.name);
        g.transform.localScale = Vector3.one;
        g.SetActive(true);
        gameModel.lookModel = g;
    }
}
