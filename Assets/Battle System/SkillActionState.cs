using System.Collections.Generic;
using UnityEngine;

public class SkillActionState : StateBehavior
{
    private StateMachineController _controller;

    private float _effectTime = 0f; // �X�L���̍Đ�����
    private float _timer = 0f; // �X�L�����Đ����n�߂Ă���̌o�ߎ���

    public override void Start()
    {
        _controller = _stateMachine.Runner.GetComponent<StateMachineController>();
    }

    private Actor _skillUser = null;
    private List<Actor> _skillTargets = null;

    public override void Enter()
    {
        // �I�����ꂽ�X�L�����Đ�����B
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
    // ���S����ɑJ�ڂ���
    private void Step()
    {
        _controller.StepTrigger();
    }
}
