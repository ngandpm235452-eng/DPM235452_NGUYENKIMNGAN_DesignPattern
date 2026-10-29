using System;

namespace DPM235452_NGUYENKIMNGAN_DecoratorPattern
{
    // Component: Định nghĩa interface chung cho các đối tượng có thể thêm trách nhiệm động
    public abstract class Component
    {
        public abstract string Operation();
    }

    // ConcreteComponent: Đối tượng gốc ban đầu chưa gắn Decorator
    public class ConcreteComponent : Component
    {
        public override string Operation() => "Thành phần gốc (ConcreteComponent)";
    }

    // Decorator: Lớp trừu tượng duy trì tham chiếu đến đối tượng Component
    public abstract class Decorator : Component
    {
        protected Component _component;

        public Decorator(Component component)
        {
            _component = component;
        }

        public override string Operation()
        {
            if (_component != null)
            {
                return _component.Operation();
            }
            return string.Empty;
        }
    }

    // ConcreteDecoratorA: Thêm tính năng A vào đối tượng
    public class ConcreteDecoratorA : Decorator
    {
        public ConcreteDecoratorA(Component component) : base(component) { }

        public override string Operation()
        {
            return $"ConcreteDecoratorA({base.Operation()})";
        }
    }

    // ConcreteDecoratorB: Thêm tính năng B vào đối tượng
    public class ConcreteDecoratorB : Decorator
    {
        public ConcreteDecoratorB(Component component) : base(component) { }

        public override string Operation()
        {
            return $"ConcreteDecoratorB({base.Operation()})";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 4: DECORATOR PATTERN (LÝ THUYẾT) ===");

            // 1. Đối tượng gốc
            Component simple = new ConcreteComponent();
            Console.WriteLine($"[Gốc]: {simple.Operation()}");

            // 2. Bọc thêm DecoratorA
            Component decoratorA = new ConcreteDecoratorA(simple);
            Console.WriteLine($"[Bọc A]: {decoratorA.Operation()}");

            // 3. Bọc tiếp DecoratorB lên DecoratorA (lồng nhau)
            Component decoratorB = new ConcreteDecoratorB(decoratorA);
            Console.WriteLine($"[Bọc A + B]: {decoratorB.Operation()}");

            Console.ReadLine();
        }
    }
}