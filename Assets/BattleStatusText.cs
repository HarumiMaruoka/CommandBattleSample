// 日本語対応
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleStatusText : MonoBehaviour
{
    [SerializeField]
    private Text _view;

    private readonly List<string> _stringList = new List<string>();

    public void AddText(string message)
    {
        _stringList.Add(message);
        _view.text = message;
    }
    public void ClearText()
    {
        _stringList.Clear();
        _view.text = null;
    }
}
