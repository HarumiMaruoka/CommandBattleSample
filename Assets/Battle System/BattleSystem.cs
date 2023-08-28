using System;
using System.Collections.Generic;
using System.Linq;

// 戦闘システムを提供する。（システムのみ。演出等は含まない。）
public class BattleSystem
{
    // 味方リスト
    private List<AllyDataStore.Ally> _allyList;
    // 敵リスト
    private List<EnemyDataStore.Enemy> _enemyList;
    // 戦闘に参加している全てのキャラ
    private List<BattleActorData> _battleActor;

    public IReadOnlyList<BattleActorData> BattleActor => _battleActor; // 戦闘に参加している全てのキャラ
    public Actor ActiveActor { get; private set; } = null; // 行動キャラ
    public Skill SelectedSkill { get; private set; } = null; // 選択されたスキル
    public List<Actor> SelectedTargets { get; set; } = null; // スキル効果対象キャラ

    public void BattleInitialize() // 戦闘開始時に呼び出す。
    {
        // 味方コレクション初期化
        _allyList = new List<AllyDataStore.Ally>(GameDataStore.Instance.AllyDataStore.ActiveAllies);
        // 敵コレクション初期化
        _enemyList = new List<EnemyDataStore.Enemy>(GameManager.Instance.EnemyManager.GetRandomEnemyGroup().Enemies);

        // 戦闘に参加している全てのキャラのコレクション初期化
        _battleActor = new List<BattleActorData>();
        _battleActor.AddRange(_allyList.Select(ally =>
            CreateBattleActor(ally.Myself, _allyList.Except(new[] { ally }).Select(a => a.Myself), _enemyList.Select(e => e.Myself))));
        _battleActor.AddRange(_enemyList.Select(enemy =>
            CreateBattleActor(enemy.Myself, _enemyList.Except(new[] { enemy }).Select(e => e.Myself), _allyList.Select(a => a.Myself))));
    }

    public void TurnStart() // ターン開始時に一度だけ呼び出す。
    {
        // 行動キャラの選定（ActionCountが最も低いActorの抽出。）
        _battleActor = BattleActor.OrderBy(a => a.Count).ToList();
        ActiveActor = BattleActor.First().Actor;
        // 全キャラのカウントタイムの更新
        var minCount = BattleActor.First().Count;
        foreach (var actor in BattleActor) actor.UpdateCountBy(-minCount);
    }

    public void StartSkillSelectMode() // スキル選択モード開始時に一度だけ呼び出す。
    {
        SelectedSkill = null;
    }

    public bool TryGetSelectedSkill(out Actor user, out Skill selected) // 選択されたスキル取得を試みる。
    {
        user = null; selected = null;

        // 選択されたオブジェクトが1つでもあるとき、0番目を選択されたスキルとして保存する。
        var selectedList = ActiveActor.SkillData.SkillSelector.SelectedObjects;
        if (selectedList.Count > 0)
        {
            SelectedSkill = selectedList[0];
        }

        if (SelectedSkill == null) return false;

        user = ActiveActor; selected = SelectedSkill;
        return true;
    }

    public void StartTargetSelectMode() // ターゲット選択モード開始時に一度だけ呼び出す。
    {
        SelectedTargets = null;
        SelectedSkill.TargetSelecter.OnCompleted += OnSelectedTarget;
    }

    public bool TryGetSelectedTarget(out Skill useSkill, out List<Actor> selecteds) // 選択されたターゲット取得を試みる。
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

        var initialCount = myself.TotalAttributeStatus.Speed; // 初期カウントの設定。
        _myself = myself; _baseCount = initialCount; _allies = allies; _enemies = enemies;
    }

    private readonly Actor _myself;
    private readonly List<Actor> _allies;
    private readonly List<Actor> _enemies;

    public Actor Actor => _myself; // 自分自身を表現する
    public List<Actor> Allies => _allies; // 味方を表現する
    public List<Actor> Enemies => _enemies; // 敵を表現する

    private int _baseCount;
    public int Count => _baseCount;

    public void UpdateCountBy(int count)
    {
        _baseCount += count;
    }
}