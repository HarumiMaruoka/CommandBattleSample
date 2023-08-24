// 日本語対応
using UnityEngine;

// 死亡処理
public class DeathProcess : StateBehavior
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
    // 継続判定ステートへ
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
