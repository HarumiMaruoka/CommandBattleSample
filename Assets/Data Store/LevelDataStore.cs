// 日本語対応
using System.Collections.Generic;

public class LevelStatusDataStore
{
    private readonly Dictionary<LevelData, ActorAttributeStatus> _statusData = new Dictionary<LevelData, ActorAttributeStatus>();
    public IReadOnlyDictionary<LevelData, ActorAttributeStatus> StatusData => _statusData;

    public void Initialize(List<string[]> csv)
    {
        for (int i = 0; i < csv.Count; i++)
        {
            int id = int.Parse(csv[i][0]);
            int level = int.Parse(csv[i][1]);
            LevelData data = new LevelData(id, level);

            ActorAttributeStatus actorAttributeStatus = new ActorAttributeStatus(
                int.Parse(csv[i][2]),   // 最大体力
                int.Parse(csv[i][3]),   // 最大MP
                int.Parse(csv[i][4]),   // 攻撃力
                int.Parse(csv[i][5]),   // 防御力
                int.Parse(csv[i][6]),   // 魔法力
                int.Parse(csv[i][7]),   // 魔法防御力
                int.Parse(csv[i][8]),   // 速さ
                int.Parse(csv[i][9]),   // 命中率
                int.Parse(csv[i][10]),  // 回避率
                int.Parse(csv[i][11])); // クリティカル率
        }
    }
}
public struct LevelData
{
    public LevelData(int id, int level) : this()
    {
        _id = id;
        _level = level;
    }

    private readonly int _id;
    private readonly int _level;

    public int Id => _id;
    public int Level => _level;
}