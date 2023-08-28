// 日本語対応
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SampleActorView : MonoBehaviour
{
    [SerializeField]
    private Image _actorImage;
    [SerializeField]
    private Image _hoverImage;

    [SerializeField]
    private Text _nameView;
    [SerializeField]
    private Text _currentHpText;
    [SerializeField]
    private Text _selectedCountView;

    [SerializeField]
    private Color _aliveColor = Color.white;
    [SerializeField]
    private Color _activeColor = Color.red;
    [SerializeField]
    private Color _deadColor = Color.black;

    private BattleSystem _battleSystem = null;
    private Actor _myself = null;
    private ActorStatus _status = ActorStatus.Alive;

    public event Action<ActorStatus> OnStatusChanged;
    private int SelectedCount => _battleSystem.SelectedTargets.Count(item => item == _myself);

    private bool TryGetHoveredActors(out List<Actor> hoverActors)
    {
        hoverActors = null;

        try
        {
            hoverActors = _battleSystem.ActiveActor.SkillData.SkillSelector.HoverObject.TargetSelecter.GetHoverActors();
        }
        catch (Exception ex)
        {
            Debug.LogError(ex.Message);
            return false;
        }

        return hoverActors != null; // hoverActorsが問題なく取得できたら trueを返す。
    }
    private bool IsHovered
    {
        get
        {
            if (TryGetHoveredActors(out List<Actor> result)) return result.Contains(_myself);
            else return false;
        }
    }

    private void Start()
    {
        OnStatusChanged += StatusUpdate;
    }
    private void Update()
    {
        _hoverImage.gameObject.SetActive(IsHovered); // ホバー中ならホバー用Imageを有効にする。そうでないときは無効。
        _selectedCountView.text = SelectedCount == 0 ? null : SelectedCount.ToString(); // 一回以上ターゲットされているときカウントを表示する。

        _currentHpText.text = _myself.ResourceStatus.Hp.ToString();

        var status = ActorStatus.Alive;
        if (_myself.IsDead) status = ActorStatus.Dead;
        else if (_battleSystem.ActiveActor == _myself) status = ActorStatus.Active;

        Status = status;
    }

    private void Initialize(BattleSystem battleSystem, Actor actor, ActorStatus initialStatus)
    {
        _battleSystem = battleSystem; _myself = actor; _status = initialStatus;
        _nameView.text = _myself.Name;
    }

    public ActorStatus Status
    {
        get => _status;

        set
        {
            if (_status != value)
            {
                _status = value;
                OnStatusChanged?.Invoke(_status);
            }
        }
    }

    private void StatusUpdate(ActorStatus newStatus)
    {
        switch (newStatus)
        {
            case ActorStatus.Alive: _actorImage.color = _aliveColor; break;
            case ActorStatus.Active: _actorImage.color = _activeColor; break;
            case ActorStatus.Dead: _actorImage.color = _deadColor; break;
        }
    }

    public enum ActorStatus
    {
        Alive, // 生存中
        Active, // 行動中
        Dead, // 死亡
    }
}
