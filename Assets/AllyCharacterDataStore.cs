// 日本語対応
using System.Collections.Generic;

public class AllyCharacterDataStore
{
    public void Initialize()
    {

    }

    private readonly Dictionary<AllyCharactor, Actor> _allyActors = new Dictionary<AllyCharactor, Actor>(); // 全ての味方キャラクター
    private readonly List<Actor> _fightableActors = new List<Actor>(); // 前線で戦うキャラクターリスト

    public IReadOnlyDictionary<AllyCharactor, Actor> AllyActors => _allyActors;
    public List<Actor> FightableActors => _fightableActors;



    
}
public enum AllyCharactor
{
    JonKing,
    HugoBurnham,
    SaraLee,
    DavidPajo,
}
