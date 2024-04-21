﻿using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class BiaoDian : MonoBehaviour
{
    /// <summary>
    /// Text文本组件
    /// </summary>
    private Text txt;

    /// <summary>
    /// 用于匹配标点符号（正则表达式）
    /// </summary>
    private readonly string strRegex = @"(\！|\？|\，|\。|\,|\.|\《|\》|\）|\：|\”|\’|\、|\；|\+|\-)";

    /// <summary>
    /// 用于存储text组件中的内容
    /// </summary>
    private System.Text.StringBuilder MExplainText = null;

    /// <summary>
    /// 用于存储text生成器中的内容
    /// </summary>
    private IList<UILineInfo> MExpalinTextLine;

    private void Awake()
    {
        txt = GetComponent<Text>();

        OnTextChange();
        txt.RegisterDirtyLayoutCallback(OnTextChange);
    }

    public void OnTextChange()
    {
        StartCoroutine(MClearUpExplainMode(txt, txt.text));
    }

    /// <summary>
    /// 整理文字。确保首字母不出现标点
    /// </summary>
    /// <param name="_component">text组件</param>
    /// <param name="_text">需要填入text中的内容</param>
    /// <returns></returns>
    IEnumerator MClearUpExplainMode(Text _component, string _text)
    {
        _component.text = _text;

        // 如果直接执行下边方法的话，那么_component.cachedTextGenerator.lines将会获取的是之前text中的内容，而不是_text的内容，所以需要等待一下
        yield return new WaitForEndOfFrame();

        // 获取Text生成器中的内容（每行的索引、线高度、直线上Y的高度、这一行和下一行的距离）
        MExpalinTextLine = _component.cachedTextGenerator.lines;

        // 需要改变的字符序号
        int mChangeIndex = -1;

        // 获得文本内容
        MExplainText = new System.Text.StringBuilder(_component.text);

        for (int i = 1; i < MExpalinTextLine.Count; i++)
        {
            // 到最后一行了返回
            if (_component.text.Length <= MExpalinTextLine[i].startCharIdx)
                break;

            // 正则表达式判断首位是否有标点
            bool _b = Regex.IsMatch(_component.text[MExpalinTextLine[i].startCharIdx].ToString(), strRegex);

            if (_b)
            {
                mChangeIndex = GetInsertPos(_component, MExpalinTextLine[i].startCharIdx - 1, MExpalinTextLine[i - 1].startCharIdx);
                //在有标点的位置插入\n换行
                if (mChangeIndex > 0)
                    MExplainText.Insert(mChangeIndex, "\n");
            }
        }

        _component.text = MExplainText.ToString();
    }

    private int GetInsertPos(Text _component, int startCharIdx, int lastLineStartIdx)
    {
        bool _b = Regex.IsMatch(_component.text[startCharIdx].ToString(), strRegex);
        if (_b)
        {
            startCharIdx = _GetInsertPos(_component, startCharIdx - 1);
            if (startCharIdx <= lastLineStartIdx)
                startCharIdx = 0;
        }

        return startCharIdx;
    }

    private int _GetInsertPos(Text _component, int startCharIdx)
    {
        if (startCharIdx == 0)
            return 0;
        bool _b = Regex.IsMatch(_component.text[startCharIdx].ToString(), strRegex);
        if (_b)
        {
            return _GetInsertPos(_component, startCharIdx - 1);
        }

        return startCharIdx;
    }
}
