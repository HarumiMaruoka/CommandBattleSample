/// <summary>
/// Actorの体力やMP等、リソースに関するステータスを管理するクラス。
/// </summary>
public class ActorResourceStatus
{
    private int _maxHp;
    private int _maxMp;
    private int _hp;
    private int _mp;

    public int Hp { get => _hp; set => _hp = value; }
    public int Mp { get => _mp; set => _mp = value; }

    public bool IsDead => _hp <= 0; // 体力が0以下の時このactorは死亡している事を表現する。

    public bool TryDamage(int value)
    {
        if (IsDead) return false;

        _hp -= value;

        if (_hp > _maxHp) _hp = _maxHp;
        else if (_hp <= 0) _hp = 0;
        return true;
    }
    public bool TryHeal(int value)
    {
        if (IsDead) return false;

        _hp += value;

        if (_hp > _maxHp) _hp = _maxHp;
        else if (_hp <= 0) _hp = 0;
        return true;
    }
    public bool TryRevive(ReviveMode mode)
    {
        if (!IsDead) return false;

        var value = 0;
        if (mode == ReviveMode.Leave1) value = 1;
        else if (mode == ReviveMode.Leave100Percent) value = _maxHp;
        else
        {
            var percent = (int)mode;
            value = (int)((percent / 100f) * _maxHp);
        }

        _hp = value;
        if (_mp > _maxMp) _mp = _maxMp;
        else if (_mp <= 0) _mp = 0;
        return true;
    }

    public bool TryIncreaseMP(int value)
    {
        if (IsDead) return false;

        _mp += value;

        if (_mp > _maxMp) _mp = _maxMp;
        else if (_mp <= 0) _mp = 0;
        return true;
    }
    /// <summary> MPを消費する。消費量に対し、現在の所有量が足りなければfalseを返す。 </summary>
    /// <param name="value"> 消費量 </param>
    /// <returns> 消費に成功したときtrueを返す。 </returns>
    public bool TryDecreaseMP(int value)
    {
        if (IsDead) return false;
        if (_mp - value < 0) return false;

        _mp -= value;

        if (_mp > _maxMp) _mp = _maxMp;
        else if (_mp <= 0) _mp = 0;
        return true;
    }
}

public enum ReviveMode
{
    // 1で復活, 1割 2割 ... 10割 で復活
    Leave1,
    Leave10Percent = 10,
    Leave20Percent = 20,
    Leave30Percent = 30,
    Leave40Percent = 40,
    Leave50Percent = 50,
    Leave60Percent = 60,
    Leave70Percent = 70,
    Leave80Percent = 80,
    Leave90Percent = 90,
    Leave100Percent = 100,
}