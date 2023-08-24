// 日本語対応

using UnityEngine;

public class ContinueJudgment : StateBehavior
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
    // Turn Endへ
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
    // Battle Exitへ
    private void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) _controller.BackTrigger();
    }
}
