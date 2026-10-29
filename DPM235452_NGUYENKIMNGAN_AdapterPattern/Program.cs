using System;

namespace DPM235452_NGUYENKIMNGAN_AdapterPattern
{
    // Target interface mà Client mong muốn sử dụng
    public interface ITarget
    {
        string GetRequest();
    }

    // Adaptee: Lớp chứa giao diện không tương thích
    public class Adaptee
    {
        public string GetSpecificRequest()
        {
            return "Dữ liệu đặc thù từ Adaptee";
        }
    }

    // Adapter: Lớp chuyển đổi tương thích
    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }

        public string GetRequest()
        {
            return $"[Adapter đã chuyển đổi] -> {_adaptee.GetSpecificRequest()}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Adaptee adaptee = new Adaptee();
            ITarget target = new Adapter(adaptee);

            Console.WriteLine("=== MẪU 1: ADAPTER PATTERN (LÝ THUYẾT) ===");
            Console.WriteLine(target.GetRequest());

            Console.ReadLine();
        }
    }
}