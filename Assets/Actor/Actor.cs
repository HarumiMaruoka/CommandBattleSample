public class Actor
{
    public Actor(int id, string name)
    {
        _id = id;
        _name = name;

        _resourceStatus = new ActorResourceStatus();
        _attributeStatus = new ActorAttributeStatus();
        _actorSkill = new ActorSkillData(this);
        _statusEffect = new ActorStatusEffect();
    }

    private int _id;
    private readonly string _name;

    private readonly ActorResourceStatus _resourceStatus;
    private readonly ActorAttributeStatus _attributeStatus;
    private readonly ActorSkillData _actorSkill;
    private readonly ActorStatusEffect _statusEffect;

    public int ID => _id;
    public string Name => _name;
    public ActorResourceStatus ResourceStatus => _resourceStatus;
    public ActorAttributeStatus Status => _attributeStatus;
    public ActorSkillData ActorSkill => _actorSkill;
    public ActorStatusEffect StatusEffect => _statusEffect;

    public AttackData AttackData => new AttackData(Status.Attack);
}