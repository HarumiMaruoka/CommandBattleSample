// 日本語対応
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [SerializeField]
    private TextAsset _textAsset;

    private void Start()
    {
        // スキルデータを読み込み
        GameDataStore.Instance.SkillData.Initialize(CsvReader.ParseCsv(_textAsset.text));
    }
}
