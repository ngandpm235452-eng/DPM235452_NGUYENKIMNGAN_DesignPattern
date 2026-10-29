using System;
using System.Collections.Generic;

namespace DPM235452_NGUYENKIMNGAN_VisitorPattern
{
    // Component Interface
    public interface IComponent
    {
        void Accept(IVisitor visitor);
    }

    // Concrete Component A
    public class ConcreteComponentA : IComponent
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentA(this);
        }

        public string ExclusiveMethodOfConcreteComponentA()
        {
            return "Phương thức đặc thù ComponentA";
        }
    }

    // Concrete Component B
    public class ConcreteComponentB : IComponent
    {
        public void Accept(IVisitor visitor)
        {
            visitor.VisitConcreteComponentB(this);
        }

        public string SpecialMethodOfConcreteComponentB()
        {
            return "Phương thức đặc thù ComponentB";
        }
    }

    // Visitor Interface
    public interface IVisitor
    {
        void VisitConcreteComponentA(ConcreteComponentA element);
        void VisitConcreteComponentB(ConcreteComponentB element);
    }

    // Concrete Visitor 1
    public class ConcreteVisitor1 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine($"Visitor1 thực thi trên {element.GetType().Name} -> {element.ExclusiveMethodOfConcreteComponentA()}");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine($"Visitor1 thực thi trên {element.GetType().Name} -> {element.SpecialMethodOfConcreteComponentB()}");
        }
    }

    // Concrete Visitor 2
    public class ConcreteVisitor2 : IVisitor
    {
        public void VisitConcreteComponentA(ConcreteComponentA element)
        {
            Console.WriteLine($"Visitor2 thực thi trên {element.GetType().Name} -> {element.ExclusiveMethodOfConcreteComponentA()}");
        }

        public void VisitConcreteComponentB(ConcreteComponentB element)
        {
            Console.WriteLine($"Visitor2 thực thi trên {element.GetType().Name} -> {element.SpecialMethodOfConcreteComponentB()}");
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== MẪU 10: VISITOR PATTERN (LÝ THUYẾT) ===");

            List<IComponent> components = new List<IComponent>
            {
                new ConcreteComponentA(),
                new ConcreteComponentB()
            };

            Console.WriteLine("--- Thực thi với Visitor 1 ---");
            var visitor1 = new ConcreteVisitor1();
            foreach (var component in components)
            {
                component.Accept(visitor1);
            }

            Console.WriteLine("\n--- Thực thi với Visitor 2 ---");
            var visitor2 = new ConcreteVisitor2();
            foreach (var component in components)
            {
                component.Accept(visitor2);
            }

            Console.ReadLine();
        }
    }
}