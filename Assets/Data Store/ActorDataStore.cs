// 日本語対応
using System.Collections.Generic;

public class ActorDataStore
{
    private readonly List<Actor> _allActorList = new List<Actor>();
    private readonly Dictionary<int, Actor> _idToActor = new Dictionary<int, Actor>();
    private readonly Dictionary<string, Actor> _nameToActor = new Dictionary<string, Actor>();

    public IReadOnlyList<Actor> AllActorList => _allActorList;
    public IReadOnlyDictionary<int, Actor> IDToActor => _idToActor;
    public IReadOnlyDictionary<string, Actor> NameToActor => _nameToActor;

    public void Initialize(List<string[]> csvData)
    {
        for (int i = 0; i < csvData.Count; i++)
        {
            int id = int.Parse(csvData[i][0]); // ActorIDの取得
            string name = csvData[i][1];       // 名前の取得

            // Actorの生成
            var actor = new Actor(id, name);

            // 各コレクションへ登録
            _allActorList.Add(actor);
            _idToActor.Add(id, actor);
            _nameToActor.Add(name, actor);
        }

        foreach (var actor in _allActorList)
        {
            actor.Initialize();
        }
    }
}