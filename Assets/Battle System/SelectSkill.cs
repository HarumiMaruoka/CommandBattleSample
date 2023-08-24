using UnityEngine;

public class SelectSkill : StateBehavior
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
    // ターゲット選択に遷移する
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
