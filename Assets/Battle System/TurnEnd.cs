using UnityEngine;

public class TurnEnd : StateBehavior
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
    // ターン開始に戻る
    private void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) _controller.BackTrigger();
    }
    // 戦闘終了に遷移する
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
