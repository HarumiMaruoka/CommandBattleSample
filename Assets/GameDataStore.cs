// 日本語対応

// ゲームに必要なデータストアのシングルトン
public class GameDataStore
{
    private readonly static GameDataStore _instance = new GameDataStore();
    public static GameDataStore Instance => _instance;
    private GameDataStore() { }

    private readonly SkillDataStore _skillData = new SkillDataStore();
    private readonly AllyCharacterDataStore _allyCharacterData = new AllyCharacterDataStore();
    private readonly SkillUnlockDataStore _skillUnlockDataStore = new SkillUnlockDataStore();

    public SkillDataStore SkillData => _skillData;
    public AllyCharacterDataStore AllyCharacterData => _allyCharacterData;
    public SkillUnlockDataStore SkillUnlockDataStore => _skillUnlockDataStore;
}
