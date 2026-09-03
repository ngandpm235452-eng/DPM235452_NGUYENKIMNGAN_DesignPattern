using System;

public class Singleton
{
    private static Singleton _instance;
    private static readonly object _lock = new object();

    private Singleton() { }

    public static Singleton Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new Singleton();
                }
                return _instance;
            }
        }
    }

    public void Log(string message) => Console.WriteLine($"[Hệ thống]: {message}");
}

class Program
{
    static void Main()
    {
        Singleton s1 = Singleton.Instance;
        Singleton s2 = Singleton.Instance;

        Console.WriteLine($"Biến s1 và s2 có cùng một vùng nhớ không? {ReferenceEquals(s1, s2)}");
        s1.Log("Khởi tạo Singleton thành công!");
    }
}