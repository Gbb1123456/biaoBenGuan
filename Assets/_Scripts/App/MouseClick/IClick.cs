using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IClick
{
    void SetCallBack(Action<GameObject> callBack);
}
