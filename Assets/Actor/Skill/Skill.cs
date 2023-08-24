using System;
using System.Collections.Generic;

public abstract class Skill
{
    public Skill(string name, SelectableTargetType targetType, TargetingType targetingType, float effectTime = 0f, int selectCount = 1)
    {
        _name = name;
        _targetType = targetType;
        _targetingType = targetingType;
        _effectTime = effectTime;
        _selectCount = selectCount;
    }

    public Skill(string[] splitedStr)
    {
        _name = splitedStr[0];
        _targetType = splitedStr[1].ToSelectableTargetType();
        _targetingType = splitedStr[2].ToTargetingType();
        _effectTime = float.Parse(splitedStr[3]);
        _selectCount = int.Parse(splitedStr[4]);
    }

    private readonly string _name;
    private readonly SelectableTargetType _targetType;
    private readonly TargetingType _targetingType;

    // 範囲攻撃の際は半径を表す。
    // 複数選択やランダム選択攻撃に関しては攻撃対象の数を表現する。
    // それ以外の値では利用されない。
    private readonly int _selectCount;

    public string Name => _name;
    public SelectableTargetType TargetType => _targetType;
    public TargetingType TargetingType => _targetingType;
    public int SelectCount => _selectCount;

    public event Action OnEffectComplete;

    protected readonly float _effectTime; // このEffectの再生時間
    protected float _effectTimer = 0f; // Effectを再生開始してからの経過時間

    // 効果を実装する
    // ToDo: とりあえず、攻撃、全体攻撃、防御を実装しよう
    public virtual void EffectStart(Actor user, List<Actor> targets)
    {
        // 共通処理をここに記載する。
        _effectTimer = 0f;
    }
    public virtual void EffectUpdate(Actor user, List<Actor> targets)
    {
        // 共通処理をここに記載する。
        _effectTimer += UnityEngine.Time.deltaTime;
        if (_effectTimer > _effectTime) EffectEnd(user, targets);
    }
    public virtual void EffectEnd(Actor user, List<Actor> targets)
    {
        // 共通処理をここに記載する。

        OnEffectComplete?.Invoke();
    }
}
public class TargetSelecter : CommandSelectObserver<Actor>
{
    private Skill _skill; // スキル

    private Actor _myself; // 自分
    private List<Actor> _allies; // 味方
    private List<Actor> _enemies; // 敵

    private List<Actor> _selectedActor = new List<Actor>(); // 選択したActor

    // 完了時に呼び出す。引数に選択済みActorリストが渡される。
    public event Action<List<Actor>> OnCompleted;

    public TargetSelecter()
    {
        base.OnSelected += OnActorSelected;
    }

    /// <summary>
    /// 選択モード開始時に呼び出す。
    /// </summary>
    /// <param name="skill"> スキル </param>
    /// <param name="myself"> スキル使用者 </param>
    /// <param name="allies"> 味方 </param>
    /// <param name="enemies"> 敵 </param>
    public void Start(Skill skill, Actor myself, List<Actor> allies, List<Actor> enemies)
    {
        _selectedActor.Clear(); _skill = skill; _myself = myself;
        _allies = allies; _enemies = enemies;
    }

    public List<Actor> GetSelectables()
    {
        List<Actor> result = new List<Actor>();

        switch (_skill.TargetType)
        {
            case SelectableTargetType.SelfOnly:
                result.Add(_myself);
                break;
            case SelectableTargetType.AllyIncludingSelf:
                result.Add(_myself);
                result.AddRange(_allies);
                break;
            case SelectableTargetType.AllyExcludingSelf:
                result.AddRange(_allies);
                break;
            case SelectableTargetType.EnemyOnly:
                result.AddRange(_enemies);
                break;
            case SelectableTargetType.AllIncludingSelf:
                result.Add(_myself);
                result.AddRange(_allies);
                result.AddRange(_enemies);
                break;
            case SelectableTargetType.AllExcludingSelf:
                result.AddRange(_allies);
                result.AddRange(_enemies);
                break;
        }

        return result;
    }

    /// <summary> カーソル中のActorを取得する。 </summary>
    /// <param name="actor"></param>
    /// <returns></returns>
    public List<Actor> GetHoverActors()
    {
        List<Actor> result = new List<Actor>();

        switch (_skill.TargetingType)
        {
            case TargetingType.SingleTarget: // 単一
                result.Add(HoverObject);
                break;
            case TargetingType.AllTargets: // 全体
                result.AddRange(GetSelectables());
                break;
            case TargetingType.AreaOfEffect: // 範囲
                for (int i = -_skill.SelectCount; i <= _skill.SelectCount; i++)
                {
                    if (IsInIndex(i + HoverIndex))
                        result.Add(Selectables[i + HoverIndex]);
                }
                break;
            case TargetingType.MultipleOverlapping: // 複数（重複可能）
                result.Add(HoverObject);
                break;
            case TargetingType.MultipleUnique: // 複数（重複不可）
                if (!_selectedActor.Contains(HoverObject)) result.Add(HoverObject);
                break;
            case TargetingType.RandomOverlapping: // ランダム（重複可能）
                throw new NotImplementedException(); // 未定義
            case TargetingType.RandomUnique: // ランダム（重複不可）
                throw new NotImplementedException(); // 未定義
        }

        return result;
    }

    private void OnActorSelected(Actor selected)
    {
        // 決定ボタン押したときに実行される
        _selectedActor.AddRange(GetHoverActors());

        // 複数選択以外の場合 次のステップへ進む。
        if (_skill.TargetingType != TargetingType.MultipleOverlapping &&
            _skill.TargetingType != TargetingType.MultipleUnique)
        {
            OnCompleted?.Invoke(new List<Actor>(_selectedActor)); //一応リストのコピーを渡す。
        }
        // 複数選択の場合 指定数選択したら次のステップへ進む。
        else if (_selectedActor.Count == _skill.SelectCount)
        {
            OnCompleted?.Invoke(new List<Actor>(_selectedActor)); //一応リストのコピーを渡す。
        }
    }
}

/// <summary>
/// 誰を選択できるか（選択可能な対象の種類）
/// </summary>
public enum SelectableTargetType
{
    /// <summary> 自分のみ </summary>
    SelfOnly,
    /// <summary> 自分を含む味方のみ </summary>
    AllyIncludingSelf,
    /// <summary> 自分を除く味方のみ </summary>
    AllyExcludingSelf,
    /// <summary> 敵のみ </summary>
    EnemyOnly,
    /// <summary> 自分を含む全体（味方および敵） </summary>
    AllIncludingSelf,
    /// <summary> 自分を除く全体（味方および敵） </summary>
    AllExcludingSelf,
}
/// <summary>
/// どのように選択するか（対象の選択方法の種類）
/// </summary>
public enum TargetingType
{
    // 直接選択
    /// <summary> 単一ターゲット </summary>
    SingleTarget,
    /// <summary> 全体ターゲット </summary>
    AllTargets,
    /// <summary> 範囲ターゲット </summary>
    AreaOfEffect,
    /// <summary> 複数ターゲット（重複化） </summary>
    MultipleOverlapping,
    /// <summary> 複数ターゲット（重複不可） </summary>
    MultipleUnique,

    // 非直接選択
    /// <summary> ランダムターゲット（重複化） </summary>
    RandomOverlapping,
    /// <summary> ランダムターゲット（重複不可） </summary>
    RandomUnique
}

public struct DamageEffect
{
    private readonly float _executeTime; // 実行タイミング(時間)
    private bool _isExecuted; // 実行済みかどうか表現する値。

    /// <summary>  </summary>
    /// <param name="executeTime"> 実行時間。（Effectを再生し始めてからの経過時間。） </param>
    public DamageEffect(float executeTime)
    { _executeTime = executeTime; _isExecuted = false; }

    /// <summary> 実行関数。指定時間経過後に一度だけ実行される。 </summary>
    /// <param name="attackData"> 攻撃データ </param>
    /// <param name="victim"> 被災者 </param>
    /// <param name="elapsedTime"> ダメージ演出を開始してからの経過時間 </param>
    /// <param name="onPlay"> 実行時に呼び出される。引数にはダメージ量が渡される。 </param>
    public void Play(AttackData attackData, Actor victim, float elapsedTime, Action<int> onPlay = null)
    {
        if (!_isExecuted && elapsedTime > _executeTime)
        {
            _isExecuted = true;
            var damageValue = DamageCalculater.DamageCalculate(attackData, victim);
            onPlay?.Invoke(damageValue);
        }
    }
}

