using System;
using UnityEngine;
using UnityEngine.UI;

public class TestSelectable : MonoBehaviour
{
    [SerializeField]
    private Image _image = null;
    [SerializeField]
    private Image _flameImage = null;
    [SerializeField]
    private Color _onSelectedColor = Color.red;
    [SerializeField]
    private Color _unSelectedColor = Color.white;

    private TestSelectableStatus _status = TestSelectableStatus.None;

    private void Start()
    {
        OnChangedStatus();
    }

    public void AddStatus(TestSelectableStatus status)
    {
        _status |= status;
        OnChangedStatus();
    }
    public void RemoveStatus(TestSelectableStatus status)
    {
        _status &= ~status;
        OnChangedStatus();
    }

    private void OnChangedStatus()
    {
        if (_status.HasFlag(TestSelectableStatus.Hover))
        {
            _flameImage.gameObject.SetActive(true);
        }
        else
        {
            _flameImage.gameObject.SetActive(false);
        }

        if (_status.HasFlag(TestSelectableStatus.Selected))
        {
            _image.color = _onSelectedColor;
        }
        else
        {
            _image.color = _unSelectedColor;
        }
    }
}
[Serializable, Flags]
public enum TestSelectableStatus
{
    None = 0,
    Hover = 1,
    Selected = 2,
}
