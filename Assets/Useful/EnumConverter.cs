// 日本語対応
using System;

public static class EnumConverter
{
    public static TargetingType ToTargetingType(this string str)
    {
        return str switch
        {
            "SingleTarget" => TargetingType.SingleTarget,
            "AllTargets" => TargetingType.AllTargets,
            "AreaOfEffect" => TargetingType.AreaOfEffect,
            "MultipleOverlapping" => TargetingType.MultipleOverlapping,
            "MultipleUnique" => TargetingType.MultipleUnique,
            "RandomOverlapping" => TargetingType.RandomOverlapping,
            "RandomUnique" => TargetingType.RandomUnique,
            _ => throw new ArgumentException($"変換に失敗しました。 {str}"),
        };
    }
    public static FieldType ToFieldType(this string str)
    {
        return str switch
        {
            "Forest" => FieldType.Forest,
            "Ocean" => FieldType.Ocean,
            "Volcano" => FieldType.Volcano,
            _ => throw new ArgumentException($"変換に失敗しました。 {str}"),
        };
    }
}
