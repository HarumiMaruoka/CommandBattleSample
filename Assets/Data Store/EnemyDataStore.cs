// 日本語対応
using System.Collections.Generic;

public class EnemyDataStore
{
    private readonly Dictionary<FieldType, FieldEnemyData> _fieldTypeToEnemyData = new Dictionary<FieldType, FieldEnemyData>();

    public void Initialize(List<string[]> csv)
    {
        for (int i = 0; i < csv.Count; i++)
        {
            FieldType fieldType = csv[i][0].ToFieldType(); // どこに生息しているか
            int groupID = int.Parse(csv[i][1]); // どのグループに所属しているか（番号）
            int actorID = int.Parse(csv[i][2]); // ActorID
            int spawnRate = int.Parse(csv[i][3]); // 出現しやすさ（この値が大きいほど出現しやすい。）

            var usableSkills = new List<int>();

            for (int j = 4; j < csv[i].Length; j++)
            {
                usableSkills.Add(int.Parse(csv[i][j])); // このエネミーが使用可能なSkillID。
            }

            if (!_fieldTypeToEnemyData.ContainsKey(fieldType)) // フィールドタイプが無ければ追加する。
            {
                _fieldTypeToEnemyData.Add(fieldType, new FieldEnemyData(new Dictionary<int, EnemyGroup>()));
            }
            if (!_fieldTypeToEnemyData[fieldType].EnemyGroups.ContainsKey(groupID)) // グループIDが無ければ追加する。このグループの出現しやすさも設定する。
            {
                _fieldTypeToEnemyData[fieldType].EnemyGroups.Add(groupID, new EnemyGroup(new List<Enemy>(), spawnRate));
            }
            _fieldTypeToEnemyData[fieldType].EnemyGroups[groupID].Enemies.Add(new Enemy(actorID, usableSkills)); // actorIDの追加。
        }
    }

    public struct FieldEnemyData
    {
        public FieldEnemyData(Dictionary<int, EnemyGroup> enemyGroups)
        {
            _enemyGroups = enemyGroups;
        }

        private readonly Dictionary<int, EnemyGroup> _enemyGroups;

        public Dictionary<int, EnemyGroup> EnemyGroups => _enemyGroups;
    }
    public struct EnemyGroup
    {
        public EnemyGroup(List<Enemy> enemyIDs, int spawnRate)
        {
            _enemies = enemyIDs;
            _spawnRate = spawnRate;
        }

        private readonly List<Enemy> _enemies; // 要素に敵のIDが入ってる。
        private readonly int _spawnRate; // 出現しやすさ。

        public List<Enemy> Enemies => _enemies;
        public int SpawnRate => _spawnRate;
    }
    public struct Enemy
    {
        public Enemy(int id, List<int> usableSkills)
        {
            _id = id; _usableSkillIDs = usableSkills;
        }

        private readonly int _id;
        private readonly List<int> _usableSkillIDs;

        public int Id => _id;
        public List<int> UsableSkillIDs => _usableSkillIDs;
    }
}
public enum FieldType
{
    Forest,  // 森
    Ocean,   // 海
    Volcano, // 火山
}