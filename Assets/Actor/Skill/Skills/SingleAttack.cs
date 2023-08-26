// 日本語対応
using System.Collections.Generic;

// 通常攻撃、ひとりを攻撃する。味方も選択可能
public class SingleAttack : Skill
{
    private DamageEffect _damageEffect = new DamageEffect(-1f);

    public SingleAttack(string name, SelectableTargetType targetType, TargetingType targetingType,
        float effectTime = 0, int selectCount = 1)
        : base(name, targetType, targetingType, effectTime, selectCount)
    { }
    public SingleAttack(string[] splitedStr) : base(splitedStr) { }

    public override void EffectStart(Actor user, List<Actor> targets)
    {
        base.EffectStart(user, targets); // 先に呼び出す。この下に独自の処理を書く。

        // ここに処理を実装する。

    }

    public override void EffectUpdate(Actor user, List<Actor> targets)
    {
        // ここに処理を実装する。

        // targetはtargetsの0番目、OnPlayは今のところnullで。
        _damageEffect.Play(user.AttackData, targets[0], _effectTimer);

        base.EffectUpdate(user, targets); // 後に呼び出す。この上に独自の処理を書く。
    }

    public override void EffectEnd(Actor user, List<Actor> targets)
    {
        // ここに処理を実装する。

        base.EffectEnd(user, targets); // 後に呼び出す。この上に独自の処理を書く。
    }
}
