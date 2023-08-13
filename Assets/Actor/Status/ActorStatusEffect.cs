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

    public void UpdateTurn(StateBehavior currentState)
    {
        // �X�e�[�g���ł̍X�V�^�C�~���O ���� Acotr���ŏ�Ԃł���� if�u���b�N�����s����B
        //if (currentState is xxx && _currentStatus.HasFlag(StatusEffect.Poison))
        //{
        // �T���v��
        //}

        // �ȉ� �����悤�ȏ���������
    }
}