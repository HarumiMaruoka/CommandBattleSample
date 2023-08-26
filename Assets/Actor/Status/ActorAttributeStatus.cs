/// <summary>
/// 攻撃力や防御力等のステータスを表現する構造体。
/// </summary>
public struct ActorAttributeStatus
{
    public ActorAttributeStatus(int maxHP, int maxMP, int attack, int defense, int magicPower, int magicDefense, int speed, int accuracy, int evasion, int criticalRate)
    {
        _maxHP = maxHP;
        _maxMP = maxMP;
        _attack = attack;
        _defense = defense;
        _magicPower = magicPower;
        _magicDefense = magicDefense;
        _speed = speed;
        _accuracy = accuracy;
        _evasion = evasion;
        _criticalRate = criticalRate;
    }

    /// <summary> 最大体力 </summary>
    public readonly int _maxHP;
    /// <summary> 最大MP </summary>
    public readonly int _maxMP;
    /// <summary> 攻撃力 </summary>
    public readonly int _attack;
    /// <summary> 防御力 </summary>
    public readonly int _defense;
    /// <summary> 魔法力 </summary>
    public readonly int _magicPower;
    /// <summary> 魔法防御力 </summary>
    public readonly int _magicDefense;
    /// <summary> 速さ </summary>
    public readonly int _speed;
    /// <summary> 命中率 </summary>
    public readonly int _accuracy;
    /// <summary> 回避率 </summary>
    public readonly int _evasion;
    /// <summary> クリティカル率 </summary>
    public readonly int _criticalRate;

    /// <summary> 最大体力 </summary>
    public int MaxHP => _maxHP;
    /// <summary> 最大MP </summary>
    public int MaxMP => _maxMP;
    /// <summary> 攻撃力 </summary>
    public int Attack => _attack;
    /// <summary> 防御力 </summary>
    public int Defense => _defense;
    /// <summary> 魔法力 </summary>
    public int MagicPower => _magicPower;
    /// <summary> 魔法防御力 </summary>
    public int MagicDefense => _magicDefense;
    /// <summary> 速さ </summary>
    public int Speed => _speed;
    /// <summary> 命中率 </summary>
    public int Accuracy => _accuracy;
    /// <summary> 回避率 </summary>
    public int Evasion => _evasion;
    /// <summary> クリティカル率 </summary>
    public int CriticalRate => _criticalRate;

    public static ActorAttributeStatus operator +(ActorAttributeStatus left, ActorAttributeStatus right)
    {
        return new ActorAttributeStatus(
            left._maxHP + right._maxHP,
            left._maxMP + right._maxMP,
            left._attack + right._attack,
            left._defense + right._defense,
            left._magicPower + right._magicPower,
            left._magicDefense + right._magicDefense,
            left._speed + right._speed,
            left._accuracy + right._accuracy,
            left._evasion + right._evasion,
            left._criticalRate + right._criticalRate
        );
    }
}