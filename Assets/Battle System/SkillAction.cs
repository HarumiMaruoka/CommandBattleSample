using UnityEngine;

public class SkillAction : StateBehavior
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
    // ‰‰o‚ğÄ¶‚·‚é
    private void PlayEffect() { }
    // €–S”»’è‚É‘JˆÚ‚·‚é
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
