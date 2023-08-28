using System;
using System.Collections.Generic;
using static UnityEngine.GraphicsBuffer;

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
        _targetType = (SelectableTargetType)int.Parse(splitedStr[1]);
        _targetingType = splitedStr[2].ToTargetingType();
        _effectTime = float.Parse(splitedStr[3]);
        _selectCount = int.Parse(splitedStr[4]);
    }

    private readonly string _name;
    private readonly SelectableTargetType _targetType;
    private readonly TargetingType _targetingType;

    private readonly TargetSelecter _targetSelecter = new TargetSelecter();
    protected readonly float _effectTime; // このEffectの再生時間

    // 範囲攻撃の際は半径を表す。
    // 複数選択やランダム選択攻撃に関しては攻撃対象の数を表現する。
    // それ以外の値では利用されない。
    private readonly int _selectCount;

    public string Name => _name;
    public float EffectTime => _effectTime;
    public SelectableTargetType TargetType => _targetType;
    public TargetingType TargetingType => _targetingType;
    public TargetSelecter TargetSelecter => _targetSelecter;
    public int SelectCount => _selectCount;
    public event Action OnEffectComplete;

    public event Action<Actor, List<Actor>> OnEffectStart;
    public event Action<Actor, List<Actor>> OnEffectUpdate;
    public event Action<Actor, List<Actor>> OnEffectEnd;

    // 効果を実装する
    // ToDo: とりあえず、攻撃、全体攻撃、防御を実装しよう
    public virtual void EffectStart(Actor user, List<Actor> targets)
    {
        // 共通処理をここに記載する。
        OnEffectStart?.Invoke(user, targets);
    }
    public virtual void EffectUpdate(Actor user, List<Actor> targets)
    {
        // 共通処理をここに記載する。
        OnEffectUpdate?.Invoke(user, targets);
    }
    public virtual void EffectEnd(Actor user, List<Actor> targets)
    {
        // 共通処理をここに記載する。
        OnEffectEnd?.Invoke(user, targets);
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

    public List<Actor> GetSelectables() // このスキルが選択可能なキャラの抽出。
    {
        List<Actor> result = new List<Actor>();

        if (_skill.TargetType.HasFlag(SelectableTargetType.Myself))
        { result.Add(_myself); }
        if (_skill.TargetType.HasFlag(SelectableTargetType.Ally))
        { result.AddRange(_allies); }
        if (_skill.TargetType.HasFlag(SelectableTargetType.Enemy))
        { result.AddRange(_enemies); }

        result.RemoveAll(actor =>
        // 選択対象が「生存しているアクター」であり、かつアクターが死亡場合、アクターを除外する
        (_skill.TargetType.HasFlag(SelectableTargetType.Alive) && actor.IsDead) ||
        // 選択対象が「死亡しているアクター」であり、かつアクターが生きている場合、アクターを除外する
        (_skill.TargetType.HasFlag(SelectableTargetType.Death) && !actor.IsDead) ||
        // もし選択対象が「状態異常を持つアクター」であり、かつアクターが状態異常を持っていない場合、アクターを除外する
        (_skill.TargetType.HasFlag(SelectableTargetType.AbnormalStatus) && actor.StatusEffect.CurrentStatus != StatusEffectType.None));

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
                {
                    Random random = new Random();

                    for (int i = 0; i < _skill.SelectCount; i++)
                    {
                        int randomIndex = random.Next(Selectables.Count); // ランダムなインデックスを生成
                        result.Add(Selectables[randomIndex]); // ランダムなアクターを選択結果に追加
                    }
                    break;
                }
            case TargetingType.RandomUnique: // ランダム（重複不可）
                {
                    Random random = new Random();
                    int numToSelect = Math.Min(_skill.SelectCount, Selectables.Count); // 選択するアクターの個数を制限

                    List<Actor> availableActors = new List<Actor>(Selectables); // 選択可能なアクターのリストをコピー
                    for (int i = 0; i < numToSelect; i++)
                    {
                        int randomIndex = random.Next(availableActors.Count); // ランダムなインデックスを生成
                        result.Add(availableActors[randomIndex]); // ランダムなアクターを選択結果に追加

                        availableActors.RemoveAt(randomIndex); // 選択したアクターを選択可能リストから削除
                    }
                    break;
                }
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
[Flags]
public enum SelectableTargetType
{
    // 自分
    Myself = 1,
    // 味方
    Ally = 2,
    // 敵
    Enemy = 4,
    // 生きているキャラ
    Alive = 8,
    // 死亡しているキャラ
    Death = 16,
    // 状態異常のキャラ
    AbnormalStatus = 32,
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

