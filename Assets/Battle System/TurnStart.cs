using UnityEngine;

public class TurnStart : StateBehavior
{
    private StateMachineController _controller;

    public override void Start()
    {
        _controller = _stateMachine.Runner.GetComponent<StateMachineController>();
    }
    public override void Enter()
    {
        _controller.BattleSystem.TurnStart();
    }
    public override void Update()
    {
        Step();
    }

    // �X�L���I���ɑJ�ڂ���
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
