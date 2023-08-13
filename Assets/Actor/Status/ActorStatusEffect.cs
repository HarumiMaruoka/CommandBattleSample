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

    public void UpdateTurn(StateBehavior currentState)
    {
        // ステートが毒の更新タイミング かつ Acotrが毒状態であれば ifブロックを実行する。
        //if (currentState is xxx && _currentStatus.HasFlag(StatusEffect.Poison))
        //{
        // サンプル
        //}

        // 以下 似たような処理が続く
    }
}