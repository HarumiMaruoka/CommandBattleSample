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
    // �X�L����I������
    private void SkillSelection() { }
    // �o�g���R���g���[���[�ɑI�������X�L����������
    private void SendSelectedSkillToBattleController() { }
    // �^�[�Q�b�g�I���ɑJ�ڂ���B
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
