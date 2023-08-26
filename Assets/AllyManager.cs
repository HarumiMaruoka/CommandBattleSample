// 日本語対応

public class AllyManager
{
    private readonly static AllyManager _instance = new AllyManager();
    public static AllyManager Instance => _instance;
    private AllyManager() { }

    // 全ての味方データ

    // 前線に配置されている味方データ

    public class AllyData
    {
        public void Initialize()
        {

        }

        private bool _isOpened; // 解放されているかどうか。
        private readonly Actor _myself;
    }
}
