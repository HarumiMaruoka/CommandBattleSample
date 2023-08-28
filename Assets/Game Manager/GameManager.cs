// 日本語対応

public class GameManager
{
    private readonly static GameManager _instance = new GameManager();
    public static GameManager Instance => _instance;
    private GameManager() { }

    private readonly EnemyManager _enemyManager = new EnemyManager();
    private readonly FieldManager _fieldManager = new FieldManager();

    public EnemyManager EnemyManager => _enemyManager;
    public FieldManager FieldManager => _fieldManager;
}
