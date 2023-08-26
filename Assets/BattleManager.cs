// 日本語対応
using System.Collections.Generic;

public class BattleManager
{
    private readonly static BattleManager _instance = new BattleManager();
    public static BattleManager Instance => _instance;
    private BattleManager() { }

    // 味方リスト
    private List<Actor> _allyList = new List<Actor>();
    // 敵リスト
    private List<Actor> _enemyList = new List<Actor>();
}
