using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BtnClick : MonoBehaviour
{
    Button btn;

    public bool isLookModel;

    private void Start()
    {
        btn = GetComponent<Button>();
        if (isLookModel)
        {
            btn.onClick.AddListener(() =>
            {
                GameManager.Instance.ShowGameModel(gameObject);
            });
        }
        else
        {
            btn.onClick.AddListener(() =>
            {
                GameManager.Instance.ShowJieShaoWnd(gameObject);
            });
        }
        
    }
}
