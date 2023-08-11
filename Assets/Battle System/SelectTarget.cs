using UnityEngine;

public class SelectTarget : StateBehavior
{
    private StateMachineController _controller;

    public override void Start()
    {
        _controller = _stateMachine.Runner.GetComponent<StateMachineController>();
    }
    public override void Update()
    {
        Step();
        Back();
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
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
