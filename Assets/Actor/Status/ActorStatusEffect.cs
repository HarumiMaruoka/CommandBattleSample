using System;
/// <summary>
/// Actor�̏�Ԉُ���Ǘ�����N���X
/// </summary>
public class ActorStatusEffect
{
    private StatusEffectType _currentStatus;
    public StatusEffectType CurrentStatus => _currentStatus;

    public void AddStatusEffect(StatusEffectType status)
    {
        _currentStatus |= status;
    }
    public void RemoveStatusEffect(StatusEffectType status)
    {
        _currentStatus &= ~status;
    }

    public void Execute(StateBehavior currentState)
    {
        // �X�e�[�g���ł̍X�V�^�C�~���O ���� Acotr���ŏ�Ԃł���� if�u���b�N�����s����B
        //if (currentState is xxx && _currentStatus.HasFlag(StatusEffect.Poison))
        //{
        // �T���v��
        //}

        // �ȉ� �����悤�ȏ���������
    }
}

[Serializable, Flags]
public enum StatusEffectType
{
    /// <summary> ���� </summary>
    None = 0,
    /// <summary> �S�� </summary>
    All = -1,
    /// <summary> �� </summary>
    Poison = 1,
    /// <summary> �ғ� </summary>
    SeverePoison = 2,
    /// <summary> �₯�� </summary>
    Burn = 4,
    /// <summary> ��� </summary>
    Paralysis = 8,
    /// <summary> ���� </summary>
    Sleep = 16,
    /// <summary> ���� </summary>
    Confusion = 32,
}
