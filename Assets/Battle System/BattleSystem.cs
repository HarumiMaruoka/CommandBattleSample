using System;
using System.Collections.Generic;
using System.Linq;

// �퓬�V�X�e����񋟂���B�i�V�X�e���̂݁B���o���͊܂܂Ȃ��B�j
public class BattleSystem
{
    public List<Actor> _allys { get; set; } = null;
    public List<Actor> _enemies { get; set; } = null;
    public List<BattleActorData> BattleActor { get; set; } = null; // �퓬�ɎQ�����Ă���S�ẴL����
    public Actor ActiveActor { get; set; } = null; // �s���L����
    public Skill SelectedSkill { get; set; } = null; // �I�����ꂽ�X�L��
    public List<Actor> SelectedActors { get; set; } = null; // �X�L�����ʑΏۃL����

    public void BattleInitialize() // �퓬�J�n���ɌĂяo���B
    {
        // �����A�G�̏���
        // �J�E���g�̏����ݒ�
        throw new NotImplementedException();
    }

    public void TurnStart() // �^�[���J�n���Ɉ�x�����Ăяo���B
    {
        // �s���L�����̑I��iActionCount���ł��ႢActor�̒��o�B�j
        BattleActor = BattleActor.OrderBy(a => a.Count).ToList();
        ActiveActor = BattleActor.First().Actor;
        // �S�L�����̃J�E���g�^�C���̍X�V
        var minCount = BattleActor.First().Count;
        foreach (var actor in BattleActor) actor.UpdateCountBy(-minCount);
    }

    public void StartSkillSelectMode() // �X�L���I�����[�h�J�n���Ɉ�x�����Ăяo���B
    {
        SelectedSkill = null;
    }

    public bool TryGetSelectedSkill(out Actor user, out Skill selected) // �I�����ꂽ�X�L���擾�����݂�B
    {
        user = null; selected = null;
        if (SelectedSkill == null) return false;

        user = ActiveActor; selected = SelectedSkill;
        return true;
    }

    public void EndSkillSelectMode() // �X�L���I�����[�h�I�����Ɉ�x�����Ăяo���B
    {
        //throw new NotImplementedException();
    }

    public void StartTargetSelectMode() // �^�[�Q�b�g�I�����[�h�J�n���Ɉ�x�����Ăяo���B
    {
        SelectedActors = null;
    }

    public bool TryGetSelectedTarget(out Skill useSkill, out List<Actor> selecteds) // �I�����ꂽ�^�[�Q�b�g�擾�����݂�B
    {
        useSkill = null; selecteds = null;
        if (SelectedActors == null) return false;

        useSkill = SelectedSkill; selecteds = SelectedActors;
        return true;
    }

    public void EndTargetSelectMode() // �^�[�Q�b�g�I�����[�h�I�����Ɉ�x�����Ăяo���B
    {
        //throw new NotImplementedException();
    }

    public void TurnEnd() // �^�[���I�����Ɉ�x�����Ăяo���B
    {
        // �s�����I�������A�N�^�[�̃J�E���g�Đݒ�B

    }

    public void ResolveRevivalActor() // �A�N�^�[�̕���������
    {
        throw new NotImplementedException();
    }

    public void ResolveDeathActor() // �A�N�^�[�̎��S������
    {
        throw new NotImplementedException();
    }
}

public struct BattleActorData
{
    public BattleActorData(Actor myself, int initialCount, List<Actor> allies, List<Actor> enemies)
    {
        if (myself == null || allies == null || enemies == null)
            throw new ArgumentNullException();
        _myself = myself; _baseCount = initialCount; _allies = allies; _enemies = enemies;
    }

    private readonly Actor _myself;
    private readonly List<Actor> _allies;
    private readonly List<Actor> _enemies;

    public Actor Actor => _myself; // �������g��\������
    public List<Actor> Allies => _allies; // ������\������
    public List<Actor> Enemies => _enemies; // �G��\������

    private int _baseCount;
    public int Count => _baseCount;

    public void UpdateCountBy(int count)
    {
        _baseCount += count;
    }
}