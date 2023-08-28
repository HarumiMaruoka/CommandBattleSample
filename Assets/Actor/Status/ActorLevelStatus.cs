// 日本語対応
using System;
using System.Collections.Generic;

public class ActorLevelStatus
{
    public ActorLevelStatus(long maxExp)
    {
        _maxExp = maxExp;
    }

    private readonly long _maxExp;

    public Action<int> OnLevelChanged; // レベルが上昇したときに実行されるアクション

    private readonly List<LevelData> _levelDataList = new List<LevelData>(); // 必ずソートして使う。
    public IReadOnlyList<LevelData> LevelDataList => _levelDataList;

    private long _exp;
    public long Exp => _exp;

    private readonly Dictionary<long, int> _expToLevelCache = new Dictionary<long, int>(); // ToDo: メモリ使用量が増加する可能性があるため、適切なタイミングでキャッシュをクリアする。

    public void AddExperience(int addExp)
    {
        int oldLevel = Level; // レベルアップ前のレベルを取得

        _exp += addExp; // 経験値上昇
        if (_exp > _maxExp) _exp = _maxExp;

        int newLevel = Level;

        if (oldLevel != newLevel) // レベルが上昇したらイベントを発火
        {
            OnLevelChanged?.Invoke(newLevel);
        }
    }

    public int Level
    {
        get
        {
            if (_expToLevelCache.TryGetValue(_exp, out int cachedLevel))
            {
                return cachedLevel;
            }

            for (int i = _levelDataList.Count - 1; i >= 0; i--)
            {
                if (_exp >= _levelDataList[i].Exp)
                {
                    _expToLevelCache[_exp] = _levelDataList[i].Level;
                    return _levelDataList[i].Level;
                }
            }
            return 0;
        }
    }

    public struct LevelData : IComparable<LevelData>
    {
        public LevelData(int level, long exp)
        {
            _level = level;
            _exp = exp;
        }

        public readonly int _level;
        public readonly long _exp; // このレベルに必要な最低限の経験値

        public int Level => _level;
        public long Exp => _exp;

        public int CompareTo(LevelData other)
        {
            return _level.CompareTo(other._level);
        }
    }

    public ActorLevelStatus Clone()
    {
        ActorLevelStatus clonedStatus = new ActorLevelStatus(_maxExp);

        // イベントのコピーはしない
        clonedStatus.OnLevelChanged = null;

        foreach (var levelData in _levelDataList)
        {
            clonedStatus._levelDataList.Add(new LevelData(levelData.Level, levelData.Exp));
        }

        clonedStatus._exp = _exp;

        foreach (var kvp in _expToLevelCache)
        {
            clonedStatus._expToLevelCache[kvp.Key] = kvp.Value;
        }

        return clonedStatus;
    }
}
