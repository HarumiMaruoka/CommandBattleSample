using System;
using System.Collections.Generic;
using System.Linq;

// 戦闘システムを提供する。（システムのみ。演出等は含まない。）
public class BattleSystem
{
    public List<Actor> _allys { get; set; } = null;
    public List<Actor> _enemies { get; set; } = null;
    public List<BattleActorData> BattleActor { get; set; } = null; // 戦闘に参加している全てのキャラ
    public Actor ActiveActor { get; set; } = null; // 行動キャラ
    public Skill SelectedSkill { get; set; } = null; // 選択されたスキル
    public List<Actor> SelectedActors { get; set; } = null; // スキル効果対象キャラ

    public void BattleInitialize() // 戦闘開始時に呼び出す。
    {
        // 味方、敵の準備
        // カウントの初期設定
        throw new NotImplementedException();
    }

    public void TurnStart() // ターン開始時に一度だけ呼び出す。
    {
        // 行動キャラの選定（ActionCountが最も低いActorの抽出。）
        BattleActor = BattleActor.OrderBy(a => a.Count).ToList();
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
        if (SelectedSkill == null) return false;

        user = ActiveActor; selected = SelectedSkill;
        return true;
    }

    public void EndSkillSelectMode() // スキル選択モード終了時に一度だけ呼び出す。
    {
        //throw new NotImplementedException();
    }

    public void StartTargetSelectMode() // ターゲット選択モード開始時に一度だけ呼び出す。
    {
        SelectedActors = null;
    }

    public bool TryGetSelectedTarget(out Skill useSkill, out List<Actor> selecteds) // 選択されたターゲット取得を試みる。
    {
        useSkill = null; selecteds = null;
        if (SelectedActors == null) return false;

        useSkill = SelectedSkill; selecteds = SelectedActors;
        return true;
    }

    public void EndTargetSelectMode() // ターゲット選択モード終了時に一度だけ呼び出す。
    {
        //throw new NotImplementedException();
    }

    public void TurnEnd() // ターン終了時に一度だけ呼び出す。
    {
        // 行動が終了したアクターのカウント再設定。

    }

    public void ResolveRevivalActor() // アクターの復活を処理
    {
        throw new NotImplementedException();
    }

    public void ResolveDeathActor() // アクターの死亡を処理
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