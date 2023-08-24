// 日本語対応
using System;
using System.Collections.Generic;

public class SkillDataStore
{
    private readonly Dictionary<int, Skill> _idToSkill = new Dictionary<int, Skill>();
    private readonly Dictionary<string, Skill> _stringToSkill = new Dictionary<string, Skill>();

    public IReadOnlyDictionary<int, Skill> IDToSkill => _idToSkill;
    public IReadOnlyDictionary<string, Skill> StringToSkill => _stringToSkill;

    public void Initialize(List<string[]> csvStr)
    {
        for (int i = 0; i < csvStr.Count; i++)
        {
            var skill = GetSkill(i, csvStr[i]);

            _idToSkill.Add(i, skill);
            _stringToSkill.Add(skill.Name, skill);
        }
    }

    private Skill GetSkill(int skillID, string[] lineData)
    {
        switch (skillID)
        {
            case 0: return new SingleAttack(lineData);
            case 1: return new AllEnemyAttack(lineData);
            default: throw new ArgumentException($"skillIDが無効です。skillID: {skillID}");
        }
    }
}