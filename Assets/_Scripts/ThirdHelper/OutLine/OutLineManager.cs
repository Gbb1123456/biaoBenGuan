
using QuickOutline;
using System.Collections.Generic;
using UnityEngine;
using ZXKFramework;

public class OutLineManager
{
    private Outline outLine = null;
    Outline _nowOutLine = null;
    private Dictionary<string, Outline> allOutLine = new Dictionary<string, Outline>();

    public void Init(Transform trsParent, Outline _outline)
    {
        this.outLine = _outline;
        if (outLine.IsNull()) return;
        foreach (Transform trs in trsParent)
        {
            if (!allOutLine.ContainsKey(trs.name))
            {
                allOutLine.Add(trs.name, SetOutLine(trs.GetOrAddComponent<Outline>()));
            }
            else
            {
                Debug.LogError("OutLineHelpr has " + trs.name);
            }
        }
    }

    Outline SetOutLine(Outline loOutLine)
    {
        if (outLine != null)
        {
            loOutLine.OutlineMode = outLine.OutlineMode;
            loOutLine.OutlineColor = outLine.OutlineColor;
            loOutLine.OutlineWidth = outLine.OutlineWidth;
        }
        return loOutLine;
    }

    public Outline OpenHightLight(string goName)
    {
        if (allOutLine.TryGetValue(goName, out Outline loOutLine))
        {
            if (loOutLine != null && !loOutLine.enabled)
            {
                loOutLine.enabled = true;
                _nowOutLine = loOutLine;
                return _nowOutLine;
            }
        }
        return null;
    }

    public Outline OpenHightLightAndCloseOther(string goName)
    {
        CloseAllOutLine();
        return OpenHightLight(goName);
    }

    public Outline OpenHightLoop(string goName)
    {
        if (allOutLine.TryGetValue(goName, out Outline loOutLine))
        {
            if (loOutLine != null)
            {
                loOutLine.enabled = !loOutLine.enabled;
                _nowOutLine = loOutLine;
                return _nowOutLine;
            }
        }
        return null;
    }

    public void CloseNowOutLine()
    {
        if (_nowOutLine != null && _nowOutLine.enabled)
        {
            _nowOutLine.enabled = false;
            _nowOutLine = null;
        }
    }

    public void CloseOutLine(string goName)
    {
        if (allOutLine.TryGetValue(goName, out Outline loOutLine))
        {
            if (loOutLine != null && loOutLine.enabled)
            {
                loOutLine.enabled = false;
            }
        }
    }

    public void CloseAllOutLine()
    {
        foreach (var loOutLine in allOutLine.Values)
        {
            if (loOutLine.enabled)
                loOutLine.enabled = false;
        }
    }
}
