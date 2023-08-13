/// <summary>
/// 攻撃力や防御力等のステータスを管理するクラス。
/// </summary>
public class ActorAttributeStatus
{
    /// <summary> 攻撃力 </summary>
    public int Attack { get; set; }
    /// <summary> 防御力 </summary>
    public int Defense { get; set; }
    /// <summary> 魔法力 </summary>
    public int MagicPower { get; set; }
    /// <summary> 魔法防御力 </summary>
    public int MagicDefense { get; set; }
    /// <summary> 速さ </summary>
    public int Speed { get; set; }
    /// <summary> 命中率 </summary>
    public int Accuracy { get; set; }
    /// <summary> 回避率 </summary>
    public int Evasion { get; set; }
    /// <summary> クリティカル率 </summary>
    public int CriticalRate { get; set; }
}