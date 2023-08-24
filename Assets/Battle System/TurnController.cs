// 日本語対応
using System;
using System.Collections.Generic;

public class TurnController
{
    // 次に行動するActorのindexが0、末尾がCount - 1;
    private readonly List<Actor> _actionOrderList = new List<Actor>();
    // 行動可能Actorリスト
    private IReadOnlyList<Actor> _actionActors = null;


    public void Initilized(IReadOnlyList<Actor> actionActors)
    {
        _actionActors = actionActors;
        _actionOrderList.Clear();

        // 最初のカウントを設定する
    }

    // メモ: 行動順 FF10とペルソナ を合わせた感じにしたい
    // カウントタイムバトル
}

// ターンのデータ。比較可能。
// 比較はどっちが早いか表現する。
public struct ActorTurnData : IComparable<ActorTurnData>
{
    public ActorTurnData(Actor actor)
    {
        if (actor == null) throw new ArgumentException("actorがnullです。");
        _actor = actor;
        _count = 0;
    }

    private readonly Actor _actor;
    public Actor Actor => _actor;

    public int _count;
    public int Count { get => _count; set => _count = value; }

    

    public int CompareTo(ActorTurnData other)
    {
        throw new NotImplementedException();
    }

    public static int CalculateCount(Actor actorStatus, Skill skill)
    {
        throw new NotImplementedException();
    }
}