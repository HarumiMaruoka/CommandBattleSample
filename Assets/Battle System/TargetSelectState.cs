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
    // �X�L����I������
    private void TargetSelection() { }
    // �o�g���R���g���[���[�ɑI�������^�[�Q�b�g��������
    private void SendSelectedTargetToBattleController() { }
    // �X�L���I���ɖ߂�
    private void Back()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) _controller.BackTrigger();
    }
    // �X�L���A�N�V�����ɑJ�ڂ���
    private void Step()
    {
        _controller.StepTrigger();
    }
}
