using System.Collections.Generic;

/// <summary> Actor�����X�L���̏�� </summary>
public class ActorSkill
{
    public ActorSkill(Actor owner)
    {
        if (owner == null) throw new System.ArgumentNullException("owner��null�ł��B");
        _owner = owner;
    }
    public void LoadCsv(/* �ǂݍ��܂ꂽ�f�[�^�B */)
    {

    }

    private readonly Actor _owner;
    // ����Actor���K���\�ȑS�ẴX�L�����X�g
    private readonly List<SkillData> _myAllSkill = new List<SkillData>();
    // ���̃A�N�^�[�����p�\�ȃX�L�����X�g
    private readonly List<SkillData> _usableSkill = new List<SkillData>();

    public Actor Owner => _owner;
    public IReadOnlyList<SkillData> AllSkill => _myAllSkill;
    public IReadOnlyList<SkillData> UsableSkill => _usableSkill;
}
