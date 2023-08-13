using System;

[Serializable, Flags]
public enum StatusEffectType
{
    /// <summary> ���� </summary>
    None = 0,
    /// <summary> �S�� </summary>
    All = -1,
    /// <summary> �� </summary>
    Poison = 1,
    /// <summary> �ғ� </summary>
    SeverePoison = 2,
    /// <summary> �₯�� </summary>
    Burn = 4,
    /// <summary> ��� </summary>
    Paralysis = 8,
    /// <summary> ���� </summary>
    Sleep = 16,
    /// <summary> ���� </summary>
    Confusion = 32,
}
