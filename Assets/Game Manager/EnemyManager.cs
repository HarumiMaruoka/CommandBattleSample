// 日本語対応

public class EnemyManager
{
    public EnemyDataStore.EnemyGroup GetRandomEnemyGroup()
    {
        // プレイヤーが現在いるフィールドの種類を取得する。
        FieldType currentFieldType = GameManager.Instance.FieldManager.CurrentFieldType;
        // 引数に渡されたFieldTypeに生息するエネミーグループリストを取得する。
        EnemyDataStore.FieldEnemyData fieldEnemyData = GameDataStore.Instance.EnemyDataStore.FieldTypeToEnemyData[currentFieldType];

        var totalRate = 0;
        foreach (var e in fieldEnemyData.EnemyGroups)
        {
            totalRate += e.Value.SpawnRate;
        }

        var randomRate = UnityEngine.Random.Range(0, totalRate + 1);
        var calculationRate = 0f; // 計算用

        foreach (var item in fieldEnemyData.EnemyGroups)
        {
            var old = calculationRate;
            calculationRate += item.Value.SpawnRate;

            if (randomRate > old && randomRate <= calculationRate)
            {
                return item.Value;
            }
        }
        return default;
    }
}