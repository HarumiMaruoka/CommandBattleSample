/// <summary>
/// Actor�̗̑͂�MP���A���\�[�X�Ɋւ���X�e�[�^�X���Ǘ�����N���X�B
/// </summary>
public class ActorResourceStatus
{
    private int _maxHp;
    private int _maxMp;
    private int _hp;
    private int _mp;

    public int Hp => _hp;
    public int Mp => _mp;

    public int MaxHp => _maxHp;
    public int MaxMp => _maxMp;

    public void Damage(int value)
    {
        _hp -= value;
    }
    public void Heal(int value)
    {
        _hp += value;
        if (_hp > _maxHp) _hp = _maxHp;
    }
}
