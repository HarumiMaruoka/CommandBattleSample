using System;

public class Actor
{
    public Actor(int id, string name)
    {
        _id = id;
        _name = name;

        _actorLevelStatus = new ActorLevelStatus(100000);
        _resourceStatus = new ActorResourceStatus();
        _actorSkill = new ActorSkillData(this);
        _statusEffect = new ActorStatusEffect();
    }

    public void Initialize()
    {
        _actorSkill.Initialize();
    }

    private readonly int _id;
    private readonly string _name;

    private readonly ActorLevelStatus _actorLevelStatus;
    private readonly ActorResourceStatus _resourceStatus;
    private readonly ActorSkillData _actorSkill;
    private readonly ActorStatusEffect _statusEffect;

    public int ID => _id;
    public string Name => _name;
    public ActorLevelStatus LevelStatus => _actorLevelStatus;
    public ActorResourceStatus ResourceStatus => _resourceStatus;
    public ActorSkillData SkillData => _actorSkill;
    public ActorStatusEffect StatusEffect => _statusEffect;

    public ActorAttributeStatus LevelAttributeStatus => 
        GameDataStore.Instance.LevelStatusDataStore.StatusData[new LevelData(_id, LevelStatus.Level)];
    public ActorAttributeStatus TotalAttributeStatus => LevelAttributeStatus;
    public AttackData AttackData => new AttackData(TotalAttributeStatus.Attack);

    public bool IsDead { get => _resourceStatus.IsDead; }
}