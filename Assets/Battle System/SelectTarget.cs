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
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
