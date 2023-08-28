// 日本語対応
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private TextAsset _skillDataCsv;
    [SerializeField]
    private TextAsset _skillUnlockDataStoreCsv;
    [SerializeField]
    private TextAsset _actorDataCsv;
    [SerializeField]
    private TextAsset _levelDataCsv;
    [SerializeField]
    private TextAsset _enemyDataCsv;

    private void Start()
    {
        // データ読み込み。逐次的処理をしている所があるので順番を崩さないように！
        GameDataStore.Instance.SkillDataStore.Initialize(CsvReader.ParseCsv(_skillDataCsv.text));
        GameDataStore.Instance.SkillUnlockDataStore.Initialize(CsvReader.ParseCsv(_skillUnlockDataStoreCsv.text));
        GameDataStore.Instance.ActorDataStore.Initialize(CsvReader.ParseCsv(_actorDataCsv.text));
        GameDataStore.Instance.LevelStatusDataStore.Initialize(CsvReader.ParseCsv(_levelDataCsv.text));
        GameDataStore.Instance.EnemyDataStore.Initialize(CsvReader.ParseCsv(_enemyDataCsv.text));
    }
}
