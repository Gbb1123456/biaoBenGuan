using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;
using UnityEngine.UI;
using SK.Framework;

public class BtnClick : MonoBehaviour
{
    Button btn;

    public bool isLookModel;

    private void Start()
    {
        btn = GetComponent<Button>();
        btn.AddListener(() =>
        {
            GameManager.Instance.transform.FindFirst("PlayerControllerFPS").GetComponent<FirstPersonController>().enabled = false;
            if (isLookModel)
            {
                GameManager.Instance.ShowGameModel(gameObject);
                StartGame.Objstring.TryAdd(gameObject.name);
            }
            else
            {
                GameManager.Instance.ShowJieShaoWnd(gameObject);
            }
        });
        
        
    }
}
