using UnityEngine;

public class BattleEnd : StateBehavior
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
    // ���o���Đ�����
    private void PlayEffect() { }
    // �t�B�[���h���[�h�ɑJ�ڂ���
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
