using System;
using System.Collections.Generic;

public static class BattleSystem
{
    private readonly static Stack<List<Actor>> _actorListPool = new Stack<List<Actor>>();

    /// <summary> オブジェクトプールから List<Actor> を取得する。 </summary>
    public static List<Actor> GetActorList()
    {
        if (_actorListPool.Count != 0)
        {
            var list = _actorListPool.Pop();
            list.Clear();
            return list;
        }
        else
        {
            return new List<Actor>();
        }
    }
    /// <summary> オブジェクトプールに List<Actor> を返却する。 </summary>
    public static void ReleaseActorList(List<Actor> list)
    {
        if (list == null) return;
        _actorListPool.Push(list);
    }

    public static int DamageCalculate(AttackData attackData, Actor actor)
    {
        throw new NotImplementedException();
    }
}
