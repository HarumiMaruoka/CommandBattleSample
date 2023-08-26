using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class StateMachineController : MonoBehaviour
{
    [SerializeField]
    private StateMachineRunner _runner;
    [SerializeField]
    private Text _stateName; // 現在のステート確認する用。（テスト用）
    [SerializeField, StateMachinePropety]
    private string _stepName;
    [SerializeField, StateMachinePropety]
    private string _backName;

    private readonly BattleSystem _battleSystem = new BattleSystem();
    public BattleSystem BattleSystem => _battleSystem;

    void Update()
    {
        _stateName.text = _runner.StateMachine.CurrentState.Name;
    }
    public async void StepTrigger()
    {
        _runner.StateMachine.SetValue(_stepName, true);
        await UniTask.Yield();
        _runner.StateMachine.SetValue(_stepName, false);
    }
    public async void BackTrigger()
    {
        _runner.StateMachine.SetValue(_backName, true);
        await UniTask.Yield();
        _runner.StateMachine.SetValue(_backName, false);
    }
}