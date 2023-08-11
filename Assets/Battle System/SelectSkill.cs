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
    // スキルを選択する
    private void SkillSelection() { }
    // バトルコントローラーに選択したスキルを教える
    private void SendSelectedSkillToBattleController() { }
    // ターゲット選択に遷移する。
    private void Step()
    {
        if (Input.GetKeyDown(KeyCode.Return)) _controller.StepTrigger();
    }
}
