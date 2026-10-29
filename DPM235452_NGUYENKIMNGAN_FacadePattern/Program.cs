using System;

namespace DPM235452_NGUYENKIMNGAN_FacadePattern
{
    // Subsystem 1: Hệ thống con 1
    public class Subsystem1
    {
        public string Operation1() => "Subsystem 1: Sẵn sàng!\n";
        public string OperationN() => "Subsystem 1: Đang xử lý...\n";
    }

    // Subsystem 2: Hệ thống con 2
    public class Subsystem2
    {
        public string Operation1() => "Subsystem 2: Chuẩn bị thực thi!\n";
        public string OperationZ() => "Subsystem 2: Hoàn tất tác vụ!\n";
    }

    // Facade: Cung cấp giao diện đơn giản hóa cho các subsystem phức tạp
    public class Facade
    {
        protected Subsystem1 _subsystem1;
        protected Subsystem2 _subsystem2;

        public Facade(Subsystem1 subsystem1, Subsystem2 subsystem2)
        {
            _subsystem1 = subsystem1;
            _subsystem2 = subsystem2;
        }

        public string Operation()
        {
            string result = "Facade khởi tạo các subsystem:\n";
            result += _subsystem1.Operation1();
            result += _subsystem2.Operation1();
            result += "Facade chỉ đạo các subsystem thực hiện hành động:\n";
            result += _subsystem1.OperationN();
            result += _subsystem2.OperationZ();
            return result;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 5: FACADE PATTERN (LÝ THUYẾT) ===");

            Subsystem1 s1 = new Subsystem1();
            Subsystem2 s2 = new Subsystem2();
            Facade facade = new Facade(s1, s2);

            Console.WriteLine(facade.Operation());

            Console.ReadLine();
        }
    }
}