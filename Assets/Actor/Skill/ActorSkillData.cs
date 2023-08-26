// 日本語対応
using System;
using System.Collections.Generic;

public class ActorSkillData
{
    public ActorSkillData(Actor owner)
    {
        if (owner == null) throw new System.ArgumentNullException("ownerがnullです。");
        _owner = owner;
    }
    public void Initialize()
    {
        // 最初から使用可能なスキル初期化。

        // このActorが習得可能な全てのスキルとSkillUnlockDataの初期化。
        var skills = new List<Skill>();

        _unlockData = GameDataStore.Instance.SkillUnlockDataStore.CharacterIDToSkillUnlockData[_owner.ID];

        for (int i = 0; i < _unlockData.Count; i++)
        {
            skills.Add(GameDataStore.Instance.SkillDataStore.IDToSkill[_unlockData[i].SkillID]);
        }

        _myAllSkill = skills;
    }

    private readonly Actor _owner;
    // このActorが習得可能な全てのスキルリスト
    private List<Skill> _myAllSkill = null;
    // スキルが使用可能か表現するデータ
    public IReadOnlyList<CharacterSkillUnlockData> _unlockData;
    // スキル選択用のクラス
    private SkillSelector _skillSelector = new SkillSelector();
    // 戦闘の際、利用可能なスキル。
    private List<Skill> _activeSkills = new List<Skill>();

    public Actor Owner => _owner;
    public IReadOnlyList<Skill> AllSkill => _myAllSkill;
    public SkillSelector SkillSelector => _skillSelector;
}

public class SkillSelector : CommandSelectObserver<Skill>
{
    public SkillSelector()
    {
        Disable();
    }

    private bool _isActive;

    public void Activate(IReadOnlyList<Skill> slectableSkills) // 起動させる
    {
        // 起動時のセットアップ処理を書く
        _isActive = true;
        base.Enable(); // 基底クラスの起動
        base.Initialize(slectableSkills); // 選択可能オブジェクトの割り当て
        base.ExecuteCancel(); // 選択済みオブジェクトをクリアする
    }

    public bool TryGetHoverSkill(out Skill skill)
    {
        skill = null;
        if (!_isActive)
        { UnityEngine.Debug.Log("非アクティブです"); return false; }

        skill = HoverObject;
        return true;
    }

    public bool TryGetSelectSkill(out Skill skill)
    {
        skill = null;
        if (!_isActive)
        { UnityEngine.Debug.Log("非アクティブです"); return false; }

        if (base.SelectedObjects.Count == 0)
        {
            return false;
        }
        else if (base.SelectedObjects.Count == 1)
        {
            if (base.SelectedObjects[0] != null)
            {
                skill = base.SelectedObjects[0];
                return true;
            }

            UnityEngine.Debug.Log("選択したオブジェクト（SelectedObjects[0]）がnullです。");
            return false;
        }

        throw new ArgumentException("未定義のエラーです。");
    }
    public void Deactivate() // 停止させる
    {
        // 停止時の処理を書く
        _isActive = false;
        base.Disable();
    }
}