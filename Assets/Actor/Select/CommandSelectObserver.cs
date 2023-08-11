using System;
using System.Collections.Generic;

public class CommandSelectObserver<T> : CommandSelectObservable<T> where T : class
{
    private IReadOnlyList<T> _selectables = null; // �I���\�I�u�W�F�N�g���X�g
    private int _hoverIndex; // �J�[�\�����̃C���f�b�N�X
    private bool _isDuplicatable = false; // �d�����đI���\���ǂ�����\������
    private bool _isEnabled = true;
    private readonly List<T> _selectedObjects = new List<T>(); // �I���ς݃I�u�W�F�N�g���X�g

    public T HoverObject // �J�[�\�����̃I�u�W�F�N�g
    {
        get
        {
            if (IsInIndex()) return _selectables[_hoverIndex];
            else throw new ArgumentException($"�͈͊O���w�肳��܂����B\nIndex: {_hoverIndex}");
        }
    }

    public IReadOnlyList<T> Selectables => _selectables;
    public int HoverIndex => _hoverIndex;
    public bool IsDuplicatable => _isDuplicatable;
    public bool IsEnable => _isEnabled;
    public IReadOnlyList<T> SelectedObjects => _selectedObjects;

    public CommandSelectObserver()
    {
        NextInput += OnNextSelect;
        PreviousInput += OnPreviousSelect;
        DeterminationInput += OnDetermination;
        CancelInput += Clear;
        BackInput += Back;
    }

    /// <summary> �R���X�g���N�^�ł��ǂ����A���̃I�u�W�F�N�g���ė��p���鎖��z�肵�ď������֐��ŏ���������B </summary>
    /// <param name="selectables"> �I���\�I�u�W�F�N�g���X�g </param>
    public void Initialize(IReadOnlyList<T> selectables)
    {
        _selectables = selectables;
    }

    public void Enable()
    { _isEnabled = true; }
    public void Disable()
    { _isEnabled = false; }

    private bool IsInIndex()
    {
        return
            _selectables != null && _selectables.Count != 0 &&
            _hoverIndex >= 0 && _hoverIndex < _selectables.Count;
    }
    private T OnDetermination()
    {
        if (!_isEnabled) return null;

        var select = HoverObject;
        if (_isDuplicatable) // �d�����đI���\
        {
            _selectedObjects.Add(select);
            return select;
        }
        else if (!_selectedObjects.Contains(select)) // �d���s�̏ꍇ�A���Ɋ܂܂�Ă��邩�ǂ����`�F�b�N
        {
            _selectedObjects.Add(select);
            return select;
        }

        return null;
    }
    private OnChangedSelectedData OnNextSelect()
    {
        var selectData = new OnChangedSelectedData();
        if (!_isEnabled) return selectData;

        selectData.oldObj = _selectables[_hoverIndex];
        _hoverIndex++;
        if (_hoverIndex >= _selectables.Count) _hoverIndex = 0;

        selectData.newObj = _selectables[_hoverIndex];

        return selectData;
    }
    private OnChangedSelectedData OnPreviousSelect()
    {
        var selectData = new OnChangedSelectedData();
        if (!_isEnabled) return selectData;

        selectData.oldObj = _selectables[_hoverIndex];
        _hoverIndex--;
        if (_hoverIndex < 0) _hoverIndex = _selectables.Count - 1;

        selectData.newObj = _selectables[_hoverIndex];

        return selectData;
    }
    private void Clear(Action<T> actionTaken)
    {
        if (!_isEnabled) return;

        foreach (var selected in _selectedObjects)
        {
            actionTaken(selected);
        }
        _selectedObjects.Clear();
    }
    private T Back()
    {
        if (!_isEnabled) throw new ArgumentException("Enabled��false�ł��B");

        if (_selectedObjects.Count != 0)
        {
            var returnItem = _selectedObjects[_selectedObjects.Count - 1];
            _selectedObjects.RemoveAt(_selectedObjects.Count - 1);
            return returnItem;
        }
        else
            throw new ArgumentException("�I���ς݃I�u�W�F�N�g�͈������܂���B");
    }
    public bool IsSelected(T target)
    {
        return _selectedObjects.Contains(target);
    }
}

public class CommandSelectObservable<T> where T : class
{
    public event Func<OnChangedSelectedData> NextInput;
    public event Func<OnChangedSelectedData> PreviousInput;
    public event Func<T> DeterminationInput;
    public event Action<Action<T>> CancelInput;
    public event Func<T> BackInput;

    public OnChangedSelectedData ExecuteNext()
    { return NextInput.Invoke(); }
    public OnChangedSelectedData ExecutePrevious()
    { return PreviousInput.Invoke(); }
    public T ExecuteDetermination()
    { return DeterminationInput?.Invoke(); }
    public void ExecuteCancel(Action<T> actionTaken) // Action taken �Ƃ� �u���u���e�v�̒���(Google�|��)
    {
        CancelInput?.Invoke(actionTaken);
    }
    public T ExecuteBack()
    { return BackInput?.Invoke(); }

    public struct OnChangedSelectedData
    {
        public OnChangedSelectedData(T oldObject = null, T newObject = null)
        {
            oldObj = oldObject; newObj = newObject;
        }

        public T oldObj;
        public T newObj;
    }
}
