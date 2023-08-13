public class SkillData
{
    public SkillData(string name, SkillLogic skill, bool isOpened = false)
    {
        if (skill == null) throw new System.ArgumentNullException("Skillがnullです。");
        _name = name; _skill = skill; IsOpened = isOpened;
    }

    private readonly string _name;
    private readonly SkillLogic _skill;

    public bool IsOpened { get; set; } // 使用可能かどうか表現する値
    public string Name => _name;
    public SkillLogic Skill => _skill;
}
