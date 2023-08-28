using System.Collections.Generic;
using UnityEngine;

public class SkillActionState : StateBehavior
{
    private StateMachineController _controller;

    private float _effectTime = 0f; // スキルの再生時間
    private float _timer = 0f; // スキルを再生し始めてからの経過時間

    public override void Start()
    {
        _controller = _stateMachine.Runner.GetComponent<StateMachineController>();
    }

    private Actor _skillUser = null;
    private List<Actor> _skillTargets = null;

    public override void Enter()
    {
        // 選択されたスキルを再生する。
        _timer = 0f;
        _effectTime = _controller.BattleSystem.SelectedSkill.EffectTime;

        _skillUser = _controller.BattleSystem.ActiveActor;
        _skillTargets = _controller.BattleSystem.SelectedTargets;

        _controller.BattleSystem.SelectedSkill.EffectStart(_skillUser, _skillTargets);
    }
    public override void Update()
    {
        _controller.BattleSystem.SelectedSkill.EffectUpdate(_skillUser, _skillTargets);
        if (_timer >= _effectTime)
        {
            Step();
        }
        _timer += Time.deltaTime;
    }
    public override void Exit()
    {
        _controller.BattleSystem.SelectedSkill.EffectEnd(_skillUser, _skillTargets);
    }
    // 死亡判定に遷移する
    private void Step()
    {
        _controller.StepTrigger();
    }
}
