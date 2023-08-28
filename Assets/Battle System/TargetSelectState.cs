using UnityEngine;

public class TargetSelectState : StateBehavior
{
    private StateMachineController _controller;

    public override void Start()
    {
        _controller = _stateMachine.Runner.GetComponent<StateMachineController>();
    }
    public override void Enter()
    {
        _controller.BattleSystem.StartTargetSelectMode();
    }
    public override void Update()
    {
        if (_controller.BattleSystem.TryGetSelectedTarget(out var useSkill, out var selecteds))
            Step();
        Back();
    }
    public override void Exit()
    {
        _controller.BattleSystem.EndTargetSelectMode();
    }
    // スキルを選択する
    private void TargetSelection() { }
    // バトルコントローラーに選択したターゲットを教える
    private void SendSelectedTargetToBattleController() { }
    // スキル選択に戻る
    private void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) _controller.BackTrigger();
    }
    // スキルアクションに遷移する
    private void Step()
    {
        _controller.StepTrigger();
    }
}
