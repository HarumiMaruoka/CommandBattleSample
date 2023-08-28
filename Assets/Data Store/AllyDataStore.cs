// 日本語対応
using System.Collections.Generic;
using System;

public class AllyDataStore
{
    // 全ての味方データ
    private readonly Dictionary<AllyType, Ally> _allAllies = new Dictionary<AllyType, Ally>();
    // 前線に配置されている味方データ
    private readonly HashSet<Ally> _activeAllies = new HashSet<Ally>();

    public Dictionary<AllyType, Ally> AllAllies => _allAllies;
    public HashSet<Ally> ActiveAllies => _activeAllies;

    public struct Ally
    {
        public Ally(Actor myself)
        {
            if (myself == null) throw new NullReferenceException(nameof(myself));
            _myself = myself;
        }
        private readonly Actor _myself;
        public Actor Myself => _myself;

        private bool IsOpened => throw new NotImplementedException(); // この味方キャラが利用可能かどうか表現する。
    }

    public enum AllyType
    {
        ErichGamma, // エリック・ガンマ
        RichardHelm, // リチャード・ヘルム
        RalphEJohnson, // ラルフ・ジョンソン
        JohnMatthewVlissides, // ジョン・ブリシディース
    }
}
