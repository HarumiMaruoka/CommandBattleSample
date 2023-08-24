using UnityEngine;

public class BattleStart : StateBehavior
{
    private StateMachineController _controller;

    public override void Start()
    {
        _controller = _stateMachine.Runner.GetComponent<StateMachineController>();
    }
    public override void Update()
    {
        Step();
    }
    // ターン開始に遷移する
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
