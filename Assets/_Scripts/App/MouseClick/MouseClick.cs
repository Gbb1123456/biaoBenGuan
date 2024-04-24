using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour, IClick
{
    Action<GameObject> callBack;
    public void SetCallBack(Action<GameObject> callBack)
    {
        this.callBack = callBack;
    }

    void OnMouseDown()
    {
        callBack?.Invoke(gameObject);
    }
}
