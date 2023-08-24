public static class DamageCalculater
{
    public static int DamageCalculate(AttackData attackData, Actor victim)
    {
        var damageValue = attackData.AttackPower;

        victim.ResourceStatus.TryDamage(damageValue);
        return damageValue;
    }
}
public struct AttackData
{
    public AttackData(int attackPower)
    {
        _attackPower = attackPower;
    }

    private readonly int _attackPower;

    public int AttackPower => _attackPower;
}
