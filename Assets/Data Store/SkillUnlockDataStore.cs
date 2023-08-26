// 日本語対応
using System.Collections.Generic;

public class SkillUnlockDataStore
{
    public void Initialize(List<string[]> csv)
    {
        for (int i = 0; i < csv.Count; i++)
        {
            var data = new CharacterSkillUnlockData(csv[i]);

            AddToDictionary(_characterIDToSkillUnlockData, data.CharacterID, data);
            AddToDictionary(_skillIDToSkillUnlockData, data.SkillID, data);
            AddToDictionary(_unlockLevelToSkillUnlockData, data.UnlockLevel, data);
        }
    }
    private void AddToDictionary<TKey, TValue>(Dictionary<TKey, List<TValue>> dictionary, TKey key, TValue value)
    {
        if (dictionary.ContainsKey(key))
        {
            dictionary[key].Add(value);
        }
        else
        {
            dictionary.Add(key, new List<TValue>() { value });
        }
    }

    private readonly Dictionary<int, List<CharacterSkillUnlockData>> _characterIDToSkillUnlockData = new Dictionary<int, List<CharacterSkillUnlockData>>();
    private readonly Dictionary<int, List<CharacterSkillUnlockData>> _skillIDToSkillUnlockData = new Dictionary<int, List<CharacterSkillUnlockData>>();
    private readonly Dictionary<int, List<CharacterSkillUnlockData>> _unlockLevelToSkillUnlockData = new Dictionary<int, List<CharacterSkillUnlockData>>();

    public IReadOnlyDictionary<int, List<CharacterSkillUnlockData>> CharacterIDToSkillUnlockData => _characterIDToSkillUnlockData;
    public IReadOnlyDictionary<int, List<CharacterSkillUnlockData>> SkillIDToSkillUnlockData => _skillIDToSkillUnlockData;
    public IReadOnlyDictionary<int, List<CharacterSkillUnlockData>> UnlockLevelToSkillUnlockData => _unlockLevelToSkillUnlockData;
}
// 特定の味方キャラがどのスキルをどのレベルで使用可能になるか表現するデータ。
public struct CharacterSkillUnlockData
{
    public CharacterSkillUnlockData(string[] splitedStr)
    {
        _characterID = int.Parse(splitedStr[0]);
        _skillID = int.Parse(splitedStr[1]);
        _unlockLevel = int.Parse(splitedStr[2]);
    }

    private readonly int _characterID;
    private readonly int _skillID;
    private readonly int _unlockLevel;

    public int CharacterID => _characterID;
    public int SkillID => _skillID;
    public int UnlockLevel => _unlockLevel;
}