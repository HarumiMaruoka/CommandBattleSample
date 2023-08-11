using System.Collections.Generic;
using UnityEngine;

public class CommandSelectRunner : MonoBehaviour
{
    [SerializeField]
    private int _generateCount = 3;
    [SerializeField]
    private TestSelectable _testSelectablePrefab = null;

    private CommandSelectObserver<TestSelectable> _commandSelectObserver = new CommandSelectObserver<TestSelectable>();

    void Start()
    {
        var list = new List<TestSelectable>();
        for (int i = 0; i < _generateCount; i++)
        {
            list.Add(Instantiate(_testSelectablePrefab, this.transform));
        }
        _commandSelectObserver.Initialize(list);
        _commandSelectObserver.HoverObject.AddStatus(TestSelectableStatus.Hover);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            var changeData = _commandSelectObserver.ExecuteNext();
            changeData.oldObj?.RemoveStatus(TestSelectableStatus.Hover);
            changeData.newObj?.AddStatus(TestSelectableStatus.Hover);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            var changeData = _commandSelectObserver.ExecutePrevious();
            changeData.oldObj?.RemoveStatus(TestSelectableStatus.Hover);
            changeData.newObj?.AddStatus(TestSelectableStatus.Hover);
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            var item = _commandSelectObserver.ExecuteDetermination();
            item?.AddStatus(TestSelectableStatus.Selected);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            _commandSelectObserver.ExecuteCancel(selected => selected.RemoveStatus(TestSelectableStatus.Selected));
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            var item = _commandSelectObserver.ExecuteBack();

            if (!_commandSelectObserver.IsSelected(item))
                item?.RemoveStatus(TestSelectableStatus.Selected);
        }
    }
}
