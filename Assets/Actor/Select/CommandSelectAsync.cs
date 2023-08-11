using Cysharp.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;

public class CommandSelectAsync<T>
{
    private bool _isRunning = false;
    private int _currentSelectIndex = 0;
    private readonly List<T> _currentSelectedObjs = new List<T>();

    public bool IsRunning => _isRunning;
    public int CurrentSelectIndex => _currentSelectIndex;
    public IReadOnlyList<T> CurrentSelectedObjs => _currentSelectedObjs;

    public T GetCurrentedSelectObj
    {
        get
        {
            if (!_isRunning) throw new ArgumentException("�ғ����Ă��܂���B");

            return _currentSelectedObjs[_currentSelectIndex];
        }
    }

    public async UniTask<List<T>> SelectAsync(IReadOnlyList<T> selectables, Func<bool> isNextInput, Func<bool> isPreviousInput,
        Func<bool> isDeterminationInput, Func<bool> isCancelInput, int selectCount, bool isDuplicatable,
        CancellationToken cancellationToken)
    {
        if (_isRunning)
        {
            throw new ArgumentException("���Ɏ��s���ł��B");
        }

        // �������L�����`�F�b�N����B
        // �����ȏꍇ�A�G���[���b�Z�[�W���o�͂���null��Ԃ��B
        if (selectables == null || selectables.Count == 0 ||
            isNextInput == null || isPreviousInput == null ||
            isDeterminationInput == null || isCancelInput == null ||
            selectCount <= 0)
        {
            throw new ArgumentException("�����Ȉ������܂܂�Ă��܂��B");
        }

        _currentSelectedObjs.Clear();
        _currentSelectIndex = 0;
        _isRunning = true;

        try
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (isNextInput())
                {
                    _currentSelectIndex++;
                    if (_currentSelectIndex >= selectables.Count) _currentSelectIndex = 0;
                }
                if (isPreviousInput())
                {
                    _currentSelectIndex--;
                    if (_currentSelectIndex < 0) _currentSelectIndex = selectables.Count - 1;
                }
                if (isDeterminationInput())
                {
                    T select = selectables[_currentSelectIndex];
                    if (isDuplicatable) // �d�����đI���\
                    {
                        _currentSelectedObjs.Add(select);
                    }
                    else if (!_currentSelectedObjs.Contains(select)) // �d���s�̏ꍇ�A���Ɋ܂܂�Ă��邩�ǂ����`�F�b�N
                    {
                        _currentSelectedObjs.Add(select);
                    }
                }

                if (isCancelInput())
                {
                    _isRunning = false;
                    return null;
                }
                if (_currentSelectedObjs.Count == selectCount)
                {
                    _isRunning = false;
                    return _currentSelectedObjs;
                }

                await UniTask.Yield();
            }
        }
        catch (OperationCanceledException)
        {
            _isRunning = false;
            throw new OperationCanceledException("�ҋ@���L�����Z������܂����B");
        }

        _isRunning = false;
        return null;
    }
}
