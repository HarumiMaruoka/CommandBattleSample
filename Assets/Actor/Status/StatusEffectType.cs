using System;

[Serializable, Flags]
public enum StatusEffectType
{
    /// <summary> ñ≥Çµ </summary>
    None = 0,
    /// <summary> ëSÇƒ </summary>
    All = -1,
    /// <summary> ì≈ </summary>
    Poison = 1,
    /// <summary> ñ“ì≈ </summary>
    SeverePoison = 2,
    /// <summary> Ç‚ÇØÇ« </summary>
    Burn = 4,
    /// <summary> ñÉ·É </summary>
    Paralysis = 8,
    /// <summary> ñ∞ÇË </summary>
    Sleep = 16,
    /// <summary> ç¨óê </summary>
    Confusion = 32,
}
