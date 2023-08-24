using System;
/// <summary>
/// Actorの状態異常を管理するクラス
/// </summary>
public class ActorStatusEffect
{
    private StatusEffectType _currentStatus;
    public StatusEffectType CurrentStatus => _currentStatus;

    public void AddStatusEffect(StatusEffectType status)
    {
        _currentStatus |= status;
    }
    public void RemoveStatusEffect(StatusEffectType status)
    {
        _currentStatus &= ~status;
    }

    public void Execute(StateBehavior currentState)
    {
        // ステートが毒の更新タイミング かつ Acotrが毒状態であれば ifブロックを実行する。
        //if (currentState is xxx && _currentStatus.HasFlag(StatusEffect.Poison))
        //{
        // サンプル
        //}

        // 以下 似たような処理が続く
    }
}

[Serializable, Flags]
public enum StatusEffectType
{
    /// <summary> 無し </summary>
    None = 0,
    /// <summary> 全て </summary>
    All = -1,
    /// <summary> 毒 </summary>
    Poison = 1,
    /// <summary> 猛毒 </summary>
    SeverePoison = 2,
    /// <summary> やけど </summary>
    Burn = 4,
    /// <summary> 麻痺 </summary>
    Paralysis = 8,
    /// <summary> 眠り </summary>
    Sleep = 16,
    /// <summary> 混乱 </summary>
    Confusion = 32,
}
