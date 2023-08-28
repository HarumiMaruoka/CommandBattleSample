using System;
using System.Collections.Generic;
using System.Linq;

// �퓬�V�X�e����񋟂���B�i�V�X�e���̂݁B���o���͊܂܂Ȃ��B�j
public class BattleSystem
{
    // �������X�g
    private List<AllyDataStore.Ally> _allyList;
    // �G���X�g
    private List<EnemyDataStore.Enemy> _enemyList;
    // �퓬�ɎQ�����Ă���S�ẴL����
    private List<BattleActorData> _battleActor;

    public IReadOnlyList<BattleActorData> BattleActor => _battleActor; // �퓬�ɎQ�����Ă���S�ẴL����
    public Actor ActiveActor { get; private set; } = null; // �s���L����
    public Skill SelectedSkill { get; private set; } = null; // �I�����ꂽ�X�L��
    public List<Actor> SelectedTargets { get; set; } = null; // �X�L�����ʑΏۃL����

    public void BattleInitialize() // �퓬�J�n���ɌĂяo���B
    {
        // �����R���N�V����������
        _allyList = new List<AllyDataStore.Ally>(GameDataStore.Instance.AllyDataStore.ActiveAllies);
        // �G�R���N�V����������
        _enemyList = new List<EnemyDataStore.Enemy>(GameManager.Instance.EnemyManager.GetRandomEnemyGroup().Enemies);

        // �퓬�ɎQ�����Ă���S�ẴL�����̃R���N�V����������
        _battleActor = new List<BattleActorData>();
        _battleActor.AddRange(_allyList.Select(ally =>
            CreateBattleActor(ally.Myself, _allyList.Except(new[] { ally }).Select(a => a.Myself), _enemyList.Select(e => e.Myself))));
        _battleActor.AddRange(_enemyList.Select(enemy =>
            CreateBattleActor(enemy.Myself, _enemyList.Except(new[] { enemy }).Select(e => e.Myself), _allyList.Select(a => a.Myself))));
    }

    public void TurnStart() // �^�[���J�n���Ɉ�x�����Ăяo���B
    {
        // �s���L�����̑I��iActionCount���ł��ႢActor�̒��o�B�j
        _battleActor = BattleActor.OrderBy(a => a.Count).ToList();
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

        // �I�����ꂽ�I�u�W�F�N�g��1�ł�����Ƃ��A0�Ԗڂ�I�����ꂽ�X�L���Ƃ��ĕۑ�����B
        var selectedList = ActiveActor.SkillData.SkillSelector.SelectedObjects;
        if (selectedList.Count > 0)
        {
            SelectedSkill = selectedList[0];
        }

        if (SelectedSkill == null) return false;

        user = ActiveActor; selected = SelectedSkill;
        return true;
    }

    public void StartTargetSelectMode() // �^�[�Q�b�g�I�����[�h�J�n���Ɉ�x�����Ăяo���B
    {
        SelectedTargets = null;
        SelectedSkill.TargetSelecter.OnCompleted += OnSelectedTarget;
    }

    public bool TryGetSelectedTarget(out Skill useSkill, out List<Actor> selecteds) // �I�����ꂽ�^�[�Q�b�g�擾�����݂�B
    {
        useSkill = null; selecteds = null;
        if (SelectedTargets == null) return false;

        useSkill = SelectedSkill; selecteds = SelectedTargets;
        return true;
    }

    public void EndTargetSelectMode()
    {
        SelectedSkill.TargetSelecter.OnCompleted -= OnSelectedTarget;
    }

    private BattleActorData CreateBattleActor(Actor actor, IEnumerable<Actor> allies, IEnumerable<Actor> enemies)
    {
        return new BattleActorData(actor, allies.ToList(), enemies.ToList());
    }

    private void OnSelectedTarget(List<Actor> selected)
    {
        SelectedTargets = selected;
    }
}

public struct BattleActorData
{
    public BattleActorData(Actor myself, List<Actor> allies, List<Actor> enemies)
    {
        if (myself == null || allies == null || enemies == null)
            throw new ArgumentNullException();

        var initialCount = myself.TotalAttributeStatus.Speed; // �����J�E���g�̐ݒ�B
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