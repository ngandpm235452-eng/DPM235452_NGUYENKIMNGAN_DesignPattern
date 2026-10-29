using System;

namespace DPM235452_NGUYENKIMNGAN_BridgePattern
{
    // Interface Implementation chứa các thao tác cơ bản
    public interface IImplementation
    {
        string OperationImplementation();
    }

    public class ConcreteImplementationA : IImplementation
    {
        public string OperationImplementation() => "Kết quả xử lý từ Implementation A";
    }

    public class ConcreteImplementationB : IImplementation
    {
        public string OperationImplementation() => "Kết quả xử lý từ Implementation B";
    }

    // Abstraction định nghĩa giao diện điều khiển cấp cao
    public class Abstraction
    {
        protected IImplementation _implementation;

        public Abstraction(IImplementation implementation)
        {
            _implementation = implementation;
        }

        public virtual string Operation()
        {
            return $"Abstraction: Chạy cùng với -> {_implementation.OperationImplementation()}";
        }
    }

    // Refined Abstraction mở rộng tính năng
    public class ExtendedAbstraction : Abstraction
    {
        public ExtendedAbstraction(IImplementation implementation) : base(implementation) { }

        public override string Operation()
        {
            return $"ExtendedAbstraction: [Mở rộng] -> {_implementation.OperationImplementation()}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            Console.WriteLine("=== MẪU 2: BRIDGE PATTERN (LÝ THUYẾT) ===");

            IImplementation implA = new ConcreteImplementationA();
            Abstraction abs1 = new Abstraction(implA);
            Console.WriteLine(abs1.Operation());

            IImplementation implB = new ConcreteImplementationB();
            Abstraction abs2 = new ExtendedAbstraction(implB);
            Console.WriteLine(abs2.Operation());

            Console.ReadLine();
        }
    }
}