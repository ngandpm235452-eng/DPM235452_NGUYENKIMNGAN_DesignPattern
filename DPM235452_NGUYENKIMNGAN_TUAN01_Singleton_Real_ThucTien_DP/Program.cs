using System;

public class AppConfigManager
{
    private static readonly Lazy<AppConfigManager> _instance =
        new Lazy<AppConfigManager>(() => new AppConfigManager());

    public string ConnectionString { get; set; }
    public string AppVersion { get; set; }

    private AppConfigManager()
    {
        ConnectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;";
        AppVersion = "Phiên bản 1.0.0";
    }

    public static AppConfigManager Instance => _instance.Value;

    public void PrintConfig() =>
        Console.WriteLine($"[Cấu hình] {AppVersion} | Kết nối DB: {ConnectionString}");
}

class Program
{
    static void Main()
    {
        AppConfigManager config1 = AppConfigManager.Instance;
        Console.WriteLine("--- Lần gọi 1 ---");
        config1.PrintConfig();

        AppConfigManager config2 = AppConfigManager.Instance;
        Console.WriteLine("\n--- Lần gọi 2 ---");
        config2.PrintConfig();

        Console.WriteLine($"\nKiểm tra đối tượng duy nhất: {ReferenceEquals(config1, config2)}");
    }
}