// 日本語対応
using System;

public static class EnumConverter
{
    public static SelectableTargetType ToSelectableTargetType(this string str)
    {
        return str switch
        {
            "SelfOnly" => SelectableTargetType.SelfOnly,
            "AllyIncludingSelf" => SelectableTargetType.AllyIncludingSelf,
            "AllyExcludingSelf" => SelectableTargetType.AllyExcludingSelf,
            "EnemyOnly" => SelectableTargetType.EnemyOnly,
            "AllIncludingSelf" => SelectableTargetType.AllIncludingSelf,
            "AllExcludingSelf" => SelectableTargetType.AllExcludingSelf,
            _ => throw new ArgumentException($"変換に失敗しました。 {str}"),
        };
    }
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
}
