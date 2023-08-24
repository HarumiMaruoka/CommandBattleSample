// 日本語対応
using System.Collections.Generic;

// 全体攻撃: 敵全体にダメージ
public class AllEnemyAttack : Skill
{
    public AllEnemyAttack(string name, SelectableTargetType targetType, TargetingType targetingType,
        float effectTime = 0, int selectCount = 1, bool isOpened = false)
        : base(name, targetType, targetingType, effectTime, selectCount, isOpened)
    { }
    public AllEnemyAttack(string[] splitedStr) : base(splitedStr)
    { }

    private DamageEffect[] _damageEffect = null;

    public override void EffectStart(Actor user, List<Actor> targets)
    {
        base.EffectStart(user, targets); // 先に呼び出す。この下に独自の処理を書く。

        // ここに処理を実装する。
        _damageEffect = new DamageEffect[targets.Count];
        for (int i = 0; i < _damageEffect.Length; i++)
        {
            _damageEffect[i] = new DamageEffect(-1);
        }
    }

    public override void EffectUpdate(Actor user, List<Actor> targets)
    {
        // ここに処理を実装する。

        for (int i = 0; i < targets.Count; i++)
        {
            _damageEffect[i].Play(user.AttackData, targets[0], _effectTimer);
        }

        base.EffectUpdate(user, targets); // 後に呼び出す。この上に独自の処理を書く。
    }

    public override void EffectEnd(Actor user, List<Actor> targets)
    {
        // ここに処理を実装する。

        base.EffectEnd(user, targets); // 後に呼び出す。この上に独自の処理を書く。
    }
}
