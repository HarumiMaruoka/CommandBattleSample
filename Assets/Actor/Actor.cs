public class Actor
{
    public Actor(string name)
    {
        _name = name;

        _resourceStatus = new ActorResourceStatus();
        _attributeStatus = new ActorAttributeStatus();
        _actorSkill = new ActorSkill(this);
        _statusEffect = new ActorStatusEffect();
    }

    private readonly string _name;

    private readonly ActorResourceStatus _resourceStatus;
    private readonly ActorAttributeStatus _attributeStatus;
    private readonly ActorSkill _actorSkill;
    private readonly ActorStatusEffect _statusEffect;

    public string Name => _name;
    public ActorResourceStatus ResourceStatus => _resourceStatus;
    public ActorAttributeStatus Status => _attributeStatus;
    public ActorSkill ActorSkill => _actorSkill;
    public ActorStatusEffect StatusEffect => _statusEffect;

    public bool IsDead => _resourceStatus.Hp <= 0; // ‘Ì—Í‚ª0ˆÈ‰º‚ÌŽž‚±‚Ìactor‚ÍŽ€–S‚µ‚Ä‚¢‚éŽ–‚ð•\Œ»‚·‚éB
}