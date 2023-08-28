using UnityEngine;

public class SkillSelectState : StateBehavior
{
    private StateMachineController _controller;

    public override void Start()
    {
        _controller = _stateMachine.Runner.GetComponent<StateMachineController>();
    }
    public override void Enter()
    {
        _controller.BattleSystem.StartSkillSelectMode();
        _controller.BattleSystem.ActiveActor.SkillData.SkillSelector.Clear();
    }
    public override void Update()
    {
        if (_controller.BattleSystem.TryGetSelectedSkill(out Actor user, out Skill selected))
        {
            Step();
        }
    }
    // ターゲット選択に遷移する
    private void Step()
    {
        _controller.StepTrigger();
    }
}
