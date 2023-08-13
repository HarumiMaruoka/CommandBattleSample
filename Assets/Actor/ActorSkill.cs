using System.Collections.Generic;

/// <summary> Actorが持つスキルの情報 </summary>
public class ActorSkill
{
    public ActorSkill(Actor owner)
    {
        if (owner == null) throw new System.ArgumentNullException("ownerがnullです。");
        _owner = owner;
    }
    public void LoadCsv(/* 読み込まれたデータ。 */)
    {

    }

    private readonly Actor _owner;
    // このActorが習得可能な全てのスキルリスト
    private readonly List<SkillData> _myAllSkill = new List<SkillData>();
    // このアクターが利用可能なスキルリスト
    private readonly List<SkillData> _usableSkill = new List<SkillData>();

    public Actor Owner => _owner;
    public IReadOnlyList<SkillData> AllSkill => _myAllSkill;
    public IReadOnlyList<SkillData> UsableSkill => _usableSkill;
}
