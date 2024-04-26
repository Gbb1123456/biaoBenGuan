using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

public class GameModel : Model
{
    public override string Name => "gameModel";

    public ExcelData excel;

    public GameObject lookModel;

    public void Init()
    {
        excel = new ExcelData();
        excel.Init();
    }
}
