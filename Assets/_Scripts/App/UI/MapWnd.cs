using SK.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using ZXKFramework;

public class MapWnd : UIBase
{
    public override string GroupName => "MapWnd";

    public override string Name => "MapWnd";

    RectTransform targetParentRect;
    Transform targetParent;
    BiaoBenGuan biaoben;
    // Start is called before the first frame update
    public override void Init(IUIManager uictrl)
    {
        base.Init(uictrl);
        targetParentRect = transform.FindFirst<RectTransform>("RawImage");
        targetParent = transform.FindFirst<Transform>("位置");
        //biaoben = GameObject.Find("start").GetComponent<BiaoBenGuan>(); 
    }
    void Update()
    {
        targetParent.localPosition = GetScreenPosition(GameObject.Find("start").GetComponent<BiaoBenGuan>().PlayerControllerFPS);
        targetParent.eulerAngles = new Vector3(0, 0, -GameObject.Find("start").GetComponent<BiaoBenGuan>().PlayerControllerFPS.transform.eulerAngles.y+180);
    }
    public Vector3 GetScreenPosition(GameObject target)
    {
        RectTransform canvasRtm = targetParentRect.GetComponent<RectTransform>();
        float width = canvasRtm.sizeDelta.x;
        float height = canvasRtm.sizeDelta.y;
        //Debug.Log(target.transform.position);
        Vector3 pos = GameObject.Find("start").GetComponent<BiaoBenGuan>().Camera.WorldToScreenPoint(target.transform.position);
        pos.x *= width / Screen.width;
        pos.y *= height / Screen.height;
        pos.x -= width * 0.5f;
        pos.y -= height * 0.5f;
        return pos;
    }
}
