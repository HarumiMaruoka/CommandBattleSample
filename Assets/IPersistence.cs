// 日本語対応

/// <summary>
/// 保存や復元の実装を強制するインターフェース
/// </summary>
public interface IPersistence
{
    void Save();
    void Load();
}
