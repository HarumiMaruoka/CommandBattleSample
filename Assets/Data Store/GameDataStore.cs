// 日本語対応

// ゲームに必要なデータストアのシングルトン
public class GameDataStore
{
    private readonly static GameDataStore _instance = new GameDataStore();
    public static GameDataStore Instance => _instance;
    private GameDataStore() { }

    private readonly SkillDataStore _skillDataStore = new SkillDataStore();
    private readonly ActorDataStore _actorDataStore = new ActorDataStore();
    private readonly SkillUnlockDataStore _skillUnlockDataStore = new SkillUnlockDataStore();
    private readonly LevelStatusDataStore _levelStatusDataStore = new LevelStatusDataStore();
    private readonly EnemyDataStore _enemyDataStore = new EnemyDataStore();

    public SkillDataStore SkillDataStore => _skillDataStore;
    public ActorDataStore ActorDataStore => _actorDataStore;
    public SkillUnlockDataStore SkillUnlockDataStore => _skillUnlockDataStore;
    public LevelStatusDataStore LevelStatusDataStore => _levelStatusDataStore;
    public EnemyDataStore EnemyDataStore => _enemyDataStore;
}
